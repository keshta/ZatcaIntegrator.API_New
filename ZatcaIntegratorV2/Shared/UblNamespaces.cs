using System;
using System.Xml;
using System.Xml.Serialization;

namespace ZatcaIntegratorV2.Shared
{
    public static class UblNamespaces
    {
        public const string Invoice = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
        public const string Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public const string Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public const string Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";

        public static XmlSerializerNamespaces Namespaces => new XmlSerializerNamespaces(new[]
        {
            new XmlQualifiedName("cac", Cac),
            new XmlQualifiedName("cbc", Cbc),
            new XmlQualifiedName("ext", Ext)
        });
    }
}
