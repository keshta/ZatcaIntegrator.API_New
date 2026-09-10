using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Model;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace ZatcaIntegratorV2.Shared
{
    public static class Extensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static int GetEnumValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string GetEnumValueAsString(this Enum value)
        {
            var val = Convert.ToInt32(value);
            return val.ToString();
        }

        public static string ToSerialNo(this string value)
        {
            //1-ProviderName|2-Version|3-DeviceSN
            string serialNo = $"1-{value}|2-V2.0.0.6|3-{Guid.NewGuid()}";
            return serialNo;
        }


        public static bool IsBase64Utf8(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            // Base64 strings must have a length that is a multiple of 4
            if (input.Length % 4 != 0)
                return false;

            try
            {
                // Try to decode Base64
                byte[] bytes = Convert.FromBase64String(input);

                // Try to decode the bytes as UTF-8
                Encoding.UTF8.GetString(bytes);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToSha256(this string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convert to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2")); // lowercase hex
                return sb.ToString();
            }
        }

        public static string ToSha256HexBase64(this string input)
        {
            // Convert the input text to a byte array
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the SHA256 hash
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convert each byte of the hash to a hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                string hexString = sb.ToString();

                // Convert the hex string to a Base64 string
                byte[] hexBytes = Encoding.UTF8.GetBytes(hexString);
                return Convert.ToBase64String(hexBytes);
            }
        }


        // Encode string to Base64 UTF-8
        public static string ToEncodeBase64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.IsBase64Utf8())
                return value;

            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }


        // Decode Base64 UTF-8 to string
        public static string ToDecodeBase64(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return string.Empty;

            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string FromBase64Utf8(this long? base64)
        {
            if (!base64.HasValue)
                return string.Empty;

            var bytes = Convert.FromBase64String(base64.ToString());
            return Encoding.UTF8.GetString(bytes);
        }

        public static XmlDocument NormalizeXml(this XmlDocument doc)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, new UTF8Encoding(false)); // false = بدون BOM
            doc.Save(writer);
            ms.Position = 0;

            var normalizedDoc = new XmlDocument();
            normalizedDoc.PreserveWhitespace = true;
            normalizedDoc.Load(ms);
            return normalizedDoc;
        }

        public static XmlDocument ToXmlDocumentNormalize(this string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
                return null;

            try
            {
                var doc = new XmlDocument
                {
                    PreserveWhitespace = true
                };
                doc.LoadXml(xmlString);

                // Ensure UTF-8 without BOM
                using var ms = new MemoryStream();
                using var writer = new StreamWriter(ms, new UTF8Encoding(false)); // false = no BOM
                doc.Save(writer);
                ms.Position = 0;
                var normalizedDoc = new XmlDocument();
                normalizedDoc.Load(ms);

                return normalizedDoc.NormalizeXml();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static XmlDocument ToXmlDocument(this string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
                //XML string is null or empty
                return null;

            try
            {
                XmlDocument doc = new XmlDocument
                {
                    PreserveWhitespace = true // It is important for electronic invoices to change the Hash
                };
                doc.LoadXml(xmlString);
                return doc;
            }
            catch (XmlException ex)
            {
                //Failed to parse XML string into XmlDocument
                return null;
            }
        }

        public static XmlDocument ToXmlDocument2(this string xmlTxt)
        {
            if (string.IsNullOrEmpty(xmlTxt))
                return null;

            // 1. Convert the Base64 string to raw bytes
            byte[] bytes = Convert.FromBase64String(xmlTxt);

            // 2. Detect encoding from BOM (Byte Order Mark) or XML declaration
            Encoding encoding = Encoding.UTF8; // default assumption

            if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            {
                encoding = Encoding.UTF8; // UTF-8 with BOM
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE)
            {
                encoding = Encoding.Unicode; // UTF-16LE
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF)
            {
                encoding = Encoding.BigEndianUnicode; // UTF-16BE
            }

            // 3. Convert bytes to string using the detected encoding
            string xmlString = encoding.GetString(bytes);

            // 4. Load XML into XmlDocument, preserving whitespace (important for invoice hashes)
            XmlDocument doc = new XmlDocument { PreserveWhitespace = true };
            doc.LoadXml(xmlString);

            return doc;
        }


        public static string SerializeInvoiceMap(this InvoiceModel invoice)
        {
            try
            {
                // Create XML serializer
                var serializer = new XmlSerializer(typeof(InvoiceModel));

                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false // include XML declaration
                };

                // Define namespaces explicitly (including default namespace)
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"); // default namespace
                ns.Add("cbc", UblNamespaces.Cbc);
                ns.Add("cac", UblNamespaces.Cac);
                ns.Add("ext", UblNamespaces.Ext);

                // Serialize the invoice into a MemoryStream
                using var ms = new MemoryStream();
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(writer, invoice, ns);
                }

                // Convert bytes to string
                ms.Position = 0;
                string xml;
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    xml = reader.ReadToEnd();
                }

                // Load XML into XmlDocument
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                // Namespace manager including default namespace
                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("inv", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"); // default namespace
                nsmgr.AddNamespace("cbc", UblNamespaces.Cbc);
                nsmgr.AddNamespace("cac", UblNamespaces.Cac);

                // Move schemeID from PartyIdentification element to its child ID element
                var partyNodes = doc.SelectNodes("//cac:PartyIdentification", nsmgr);
                if (partyNodes != null)
                {
                    foreach (XmlElement party in partyNodes)
                    {
                        string schemeID = party.GetAttribute("schemeID");
                        if (!string.IsNullOrEmpty(schemeID))
                            party.RemoveAttribute("schemeID");

                        var idNode = party.SelectSingleNode("cbc:ID", nsmgr) as XmlElement;
                        if (idNode != null && !string.IsNullOrEmpty(schemeID))
                        {
                            idNode.SetAttribute("schemeID", schemeID);
                        }
                    }
                }

                return doc.OuterXml;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string SerializeInvoiceMap2(this InvoiceModel invoice)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(InvoiceModel));

                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false // include declaration
                    //OmitXmlDeclaration = true
                };

                var ns = new XmlSerializerNamespaces();
                ns.Add("cbc", UblNamespaces.Cbc);
                ns.Add("cac", UblNamespaces.Cac);
                ns.Add("ext", UblNamespaces.Ext);

                // Serialize the invoice to a memory stream
                using var ms = new MemoryStream();
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(writer, invoice, ns);
                }

                // Convert bytes to string
                string xml;
                ms.Position = 0;
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    xml = reader.ReadToEnd();
                }

                // Load XML into XmlDocument
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("cac", UblNamespaces.Cac);
                nsmgr.AddNamespace("cbc", UblNamespaces.Cbc);

                // Move schemeID from parent PartyIdentification to child ID
                var partyNodes = doc.SelectNodes("//cac:PartyIdentification", nsmgr);
                if (partyNodes != null)
                {
                    foreach (XmlElement party in partyNodes)
                    {
                        string schemeID = party.GetAttribute("schemeID");
                        if (!string.IsNullOrEmpty(schemeID))
                            party.RemoveAttribute("schemeID");

                        var idNode = party.SelectSingleNode("cbc:ID", nsmgr) as XmlElement;
                        if (idNode != null && !string.IsNullOrEmpty(schemeID))
                        {
                            idNode.SetAttribute("schemeID", schemeID);
                        }
                    }
                }

                // Return final XML
                return doc.OuterXml;
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string SerializeInvoice(this InvoiceModel invoice)
        {
            try
            {
                // Create an XML serializer for the InvoiceModel
                var serializer = new XmlSerializer(typeof(InvoiceModel));

                // Configure XML output settings
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,                      // Pretty-print the XML
                    Encoding = new UTF8Encoding(false), // UTF-8 without BOM
                    OmitXmlDeclaration = false          // Include <?xml ... ?> declaration
                };

                // Define the XML namespaces for serialization
                var ns = new XmlSerializerNamespaces();
                ns.Add("cbc", UblNamespaces.Cbc);
                ns.Add("cac", UblNamespaces.Cac);
                ns.Add("ext", UblNamespaces.Ext);

                // Serialize the invoice into a memory stream
                using var ms = new MemoryStream();
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    // Pass the namespaces to the serializer
                    serializer.Serialize(writer, invoice, ns);
                }

                // Convert the UTF-8 bytes to a string and return
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                // Print the exception to console (for debugging)
                //Console.WriteLine(ex);
                return string.Empty; // Return empty string if serialization fails
            }
        }


        public static string SerializeInvoice2(this InvoiceModel invoice)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceModel));
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8,
                OmitXmlDeclaration = false
            };
            using StringWriter stringWriter = new StringWriter();
            using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
            serializer.Serialize(xmlWriter, invoice, UblNamespaces.Namespaces);
            return stringWriter.ToString();
        }

        public static string ToDateInvoice(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToTimeInvoice(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

        public static decimal ToTwoDecimal(this decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal ToTwoDecimal(this decimal? value)
        {
            return Math.Round(value??0, 2, MidpointRounding.AwayFromZero);
        }

        public static EnvironmentType ToEnvironmentType(this ZatcaEnvironmentType environment)
        {
            var env = environment switch
            {
                ZatcaEnvironmentType.Production => EnvironmentType.Production,
                ZatcaEnvironmentType.Simulation => EnvironmentType.Simulation,
                ZatcaEnvironmentType.NonProduction => EnvironmentType.NonProduction,
                _ => throw new ArgumentOutOfRangeException(nameof(environment), environment, null)
            };
            return env;
        }

    }
}
