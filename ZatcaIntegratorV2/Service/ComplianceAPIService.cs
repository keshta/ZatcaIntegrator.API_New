using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using ZATCA.EInvoice.SDK;
using ZATCA.EInvoice.SDK.Contracts;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.XmlInvoice;

namespace ZatcaIntegratorV2.Service
{
    public class ComplianceAPIService : IComplianceAPIService
    {
        IEInvoiceHashGenerator _eInvoiceHashGenerator = new EInvoiceHashGenerator();
        IEInvoiceSigner _eInvoiceSigner = new EInvoiceSigner();
        IEInvoiceQRGenerator _eInvoiceQRGenerator = new EInvoiceQRGenerator();
        IXmlInvoiceSimplified _xmlInvoiceSimplified = new XmlInvoiceSimplified();
        IXmlInvoiceStandard _xmlInvoiceStandard = new XmlInvoiceStandard();

        public async Task<ComplianceResultDto> GetComplianceCSIDAsync(string csr, string otp, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            
            string url = environment switch
            {
                ZatcaEnvironmentType.Production => "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/compliance",
                ZatcaEnvironmentType.Simulation => "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/compliance",
                ZatcaEnvironmentType.NonProduction => "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance",
                _ => throw new ArgumentOutOfRangeException(nameof(environment), "Invalid environment type")
            };


            var requestBody = new { csr };
            var result = new ComplianceResultDto();
            var errorList = new ComplianceErrorListDto();

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(5);

                // Required headers
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("OTP", otp);
                client.DefaultRequestHeaders.Add("Accept-Version", "V2");

                var json = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest: // 400
                            errorList = JsonSerializer.Deserialize<ComplianceErrorListDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            ) ?? new ComplianceErrorListDto();
                            break;

                        case HttpStatusCode.NotAcceptable: // 406
                            var singleError = JsonSerializer.Deserialize<ComplianceErrorDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            );
                            if (singleError != null)
                                errorList.Errors.Add(singleError);
                            break;

                        case HttpStatusCode.InternalServerError: // 500
                            errorList.Errors.Add(new ComplianceErrorDto { Code = "", Message = responseContent });
                            break;

                        default:
                            errorList.Errors.Add(new ComplianceErrorDto { Code = response.StatusCode.ToString(), Message = responseContent });
                            break;
                    }

                }

                else
                {
                    // Success: deserialize response
                    result.Response = JsonSerializer.Deserialize<ComplianceResponseDto>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
            }
            catch (Exception ex)
            {
                // Log full exception
                errorList.Errors.Add(new ComplianceErrorDto
                {
                    Code = "Exception",
                    Message = ex.Message
                });
            }

            result.Errors = errorList.Errors;
            return result;
        }


        //public async Task<ComplianceResultDto> SignAllDocumentAsync(ComplianceResponseDto response, string privateKey, Dictionary<InvoiceTypeData, InvoiceDto> invoices, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        public async Task<ComplianceResultDto> SignAllDocumentAsync(ComplianceResponseDto response, string privateKey, AccountCustomerOrSupplierDto supplier, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            //string projectRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string baseSimplifiedPath = Path.Combine(projectRoot, "Files", "SigningDevice", "Simplified");
            //string baseStandardPath = Path.Combine(projectRoot, "Files", "SigningDevice", "Standard");

            //string simplifiedCreditPath = Path.Combine(baseSimplifiedPath, "Simplified_Credit.xml");
            //string simplifiedDebitPath = Path.Combine(baseSimplifiedPath, "Simplified_Debit.xml");
            //string simplifiedInvoicePath = Path.Combine(baseSimplifiedPath, "Simplified_Invoice.xml");

            //string standardCreditPath = Path.Combine(baseStandardPath, "Standard_Credit.xml");
            //string standardDebitPath = Path.Combine(baseStandardPath, "Standard_Debit.xml");
            //string standardInvoicePath = Path.Combine(baseStandardPath, "Standard_Invoice.xml");

            //var simplifiedCredit = invoices.ContainsKey(InvoiceTypeData.SimplifiedCredit) ? invoices[InvoiceTypeData.SimplifiedCredit] : null;
            //var simplifiedCreditXml = await _xmlInvoiceSimplified.GenerateXmlCreditAsync(simplifiedCredit);

            //var simplifiedDebit = invoices.ContainsKey(InvoiceTypeData.SimplifiedDebit) ? invoices[InvoiceTypeData.SimplifiedDebit] : null;
            //var simplifiedDebitXml = await _xmlInvoiceSimplified.GenerateXmlDebitAsync(simplifiedDebit);

            //var simplifiedInvoice = invoices.ContainsKey(InvoiceTypeData.SimplifiedInvoice) ? invoices[InvoiceTypeData.SimplifiedInvoice] : null;
            //var simplifiedInvoiceXml = await _xmlInvoiceSimplified.GenerateXmlInvoiceAsync(simplifiedInvoice);

            //var standardCredit = invoices.ContainsKey(InvoiceTypeData.StandardCredit) ? invoices[InvoiceTypeData.StandardCredit] : null;
            //var standardCreditXml = await _xmlInvoiceStandard.GenerateXmlCreditAsync(standardCredit);

            //var standardDebit = invoices.ContainsKey(InvoiceTypeData.StandardDebit) ? invoices[InvoiceTypeData.StandardDebit] : null;
            //var standardDebitXml = await _xmlInvoiceStandard.GenerateXmlDebitAsync(standardDebit);

            //var standardInvoice = invoices.ContainsKey(InvoiceTypeData.StandardInvoice) ? invoices[InvoiceTypeData.StandardInvoice] : null;
            //var standardInvoiceXml = await _xmlInvoiceStandard.GenerateXmlInvoiceAsync(standardInvoice);

            var (simplifiedCreditUuid, simplifiedCreditXml) = InvoiceData.GetSimplifiedCredit(supplier);
            var (simplifiedDebitUuid, simplifiedDebitXml) = InvoiceData.GetSimplifiedDebit(supplier);
            var (simplifiedInvoiceUuid, simplifiedInvoiceXml) = InvoiceData.GetSimplifiedDebit(supplier);

            var (standardCreditUuid, standardCreditXml) = InvoiceData.GetStandardCredit(supplier);
            var (standardDebitUuid, standardDebitXml) = InvoiceData.GetStandardDebit(supplier);
            var (standardInvoiceUuid, standardInvoiceXml) = InvoiceData.GetStandardDebit(supplier);

            var simplifiedCreditResult = await SendComplianceDocumnet(response, privateKey, xml: simplifiedCreditXml, uuid: simplifiedCreditUuid, environment);
            
            if(!simplifiedCreditResult.IsSuccess)
                return simplifiedCreditResult;

            var simplifiedDebitResult = await SendComplianceDocumnet(response, privateKey, simplifiedDebitXml, simplifiedDebitUuid, environment);

            if (!simplifiedDebitResult.IsSuccess)
                return simplifiedDebitResult;

            var simplifiedInvoiceResult = await SendComplianceDocumnet(response, privateKey, simplifiedInvoiceXml, simplifiedInvoiceUuid, environment);

            if (!simplifiedInvoiceResult.IsSuccess)
                return simplifiedInvoiceResult;

            var standardCreditResult = await SendComplianceDocumnet(response, privateKey, standardCreditXml, standardCreditUuid, environment);

            if (!standardCreditResult.IsSuccess)
                return standardCreditResult;

            var standardDebitResult = await SendComplianceDocumnet(response, privateKey, standardDebitXml, standardDebitUuid, environment);
            if (!standardDebitResult.IsSuccess)
                return standardDebitResult;

            var standardInvoiceResult = await SendComplianceDocumnet(response, privateKey, standardInvoiceXml, standardInvoiceUuid, environment);

            if (!standardInvoiceResult.IsSuccess)
                return standardInvoiceResult;
            
            return new ComplianceResultDto();
        }


        public async Task<ComplianceResultDto> SendComplianceDocumnet(ComplianceResponseDto compliance, string privateKey, string xml, Guid uuid,ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            var sign = await SignDocument(compliance, privateKey, xml, uuid);

            string url = environment switch
            {
                ZatcaEnvironmentType.Production => "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/compliance/invoices",
                ZatcaEnvironmentType.Simulation => "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/compliance/invoices",
                ZatcaEnvironmentType.NonProduction => "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance/invoices",
                _ => throw new ArgumentOutOfRangeException(nameof(environment), "Invalid environment type")
            };

            var requestBody = new
            {
                invoiceHash = sign.InvoiceHash.Hash, //sign.Signer.Steps[1].ResultedValue,
                uuid = sign.Uuid,
                invoice = sign.Signer.SignedEInvoice.OuterXml.ToEncodeBase64()
            };

            var result = new ComplianceResultDto();
            var errorList = new ComplianceErrorListDto();

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(5);
                string base64Auth = $"{compliance.BinarySecurityToken}:{compliance.Secret}".ToEncodeBase64();

                // Required headers
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("Accept-Version", "V2");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var json = JsonSerializer.Serialize(requestBody);

                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest: // 400
                            errorList = JsonSerializer.Deserialize<ComplianceErrorListDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            ) ?? new ComplianceErrorListDto();
                            break;

                        case HttpStatusCode.NotAcceptable: // 406
                            var singleError = JsonSerializer.Deserialize<ComplianceErrorDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            );
                            if (singleError != null)
                                errorList.Errors.Add(singleError);
                            break;

                        case HttpStatusCode.InternalServerError: // 500
                            errorList.Errors.Add(new ComplianceErrorDto { Code = "", Message = responseContent });
                            break;

                        default:
                            errorList.Errors.Add(new ComplianceErrorDto { Code = response.StatusCode.ToString(), Message = responseContent });
                            break;
                    }
                }

                else
                {
                    // Success: deserialize response
                    result.Response = JsonSerializer.Deserialize<ComplianceResponseDto>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
            }
            catch (Exception ex)
            {
                // Log full exception
                errorList.Errors.Add(new ComplianceErrorDto
                {
                    Code = "Exception",
                    Message = ex.Message
                });
            }

            result.Errors = errorList.Errors;
            return result;
        }


        public async Task<ComplianceResultDto> SendComplianceDocumnet(ComplianceResponseDto compliance, string privateKey, XmlDocument xmlDocument, string uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            string csid = compliance.BinarySecurityToken.ToDecodeBase64();
            var sign = _eInvoiceSigner.SignDocument(xmlDocument, csid, privateKey);
            //var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(xmlDocument);
            var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(sign.SignedEInvoice);
            var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(sign.SignedEInvoice);

            var validator = new EInvoiceValidator();
            var resultValidator = validator.ValidateEInvoice(sign.SignedEInvoice, csid, privateKey);

            string url = environment switch
            {
                ZatcaEnvironmentType.Production => "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/compliance/invoices",
                ZatcaEnvironmentType.Simulation => "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/compliance/invoices",
                ZatcaEnvironmentType.NonProduction => "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance/invoices",
                _ => throw new ArgumentOutOfRangeException(nameof(environment), "Invalid environment type")
            };

            var requestBody = new
            {
                invoiceHash = invoiceHash.Hash,
                uuid,
                invoice = sign.SignedEInvoice.OuterXml.ToEncodeBase64()
            };

            var result = new ComplianceResultDto();
            var errorList = new ComplianceErrorListDto();

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(5);
                string base64Auth = $"{compliance.BinarySecurityToken}:{compliance.Secret}".ToEncodeBase64();

                // Required headers
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("Accept-Version", "V2");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var json = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest: // 400
                            errorList = JsonSerializer.Deserialize<ComplianceErrorListDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            ) ?? new ComplianceErrorListDto();
                            break;

                        case HttpStatusCode.NotAcceptable: // 406
                            var singleError = JsonSerializer.Deserialize<ComplianceErrorDto>(
                                responseContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            );
                            if (singleError != null)
                                errorList.Errors.Add(singleError);
                            break;

                        case HttpStatusCode.InternalServerError: // 500
                            errorList.Errors.Add(new ComplianceErrorDto { Code = "", Message = responseContent });
                            break;

                        default:
                            errorList.Errors.Add(new ComplianceErrorDto { Code = response.StatusCode.ToString(), Message = responseContent });
                            break;
                    }

                }

                else
                {
                    // Success: deserialize response
                    result.Response = JsonSerializer.Deserialize<ComplianceResponseDto>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
            }
            catch (Exception ex)
            {
                // Log full exception
                errorList.Errors.Add(new ComplianceErrorDto
                {
                    Code = "Exception",
                    Message = ex.Message
                });
            }

            result.Errors = errorList.Errors;
            return result;
        }

        private async Task<SignResultDto> SignDocument(ComplianceResponseDto response, string privateKey, string xml, Guid uuid)
        {
            var signResult = new SignResultDto();
            try
            {
                //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                //Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                string fullXml = xml;
                string csid = response.BinarySecurityToken.ToDecodeBase64();
                var invDoc = fullXml.ToXmlDocumentNormalize();
                //invDoc = invDoc.NormalizeXml();
                var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(invDoc);
                var signer = _eInvoiceSigner.SignDocument(invDoc, csid, privateKey);
                var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(signer.SignedEInvoice);

                signResult.Signer = signer;
                signResult.InvoiceHash = invoiceHash;
                signResult.MasterDocumentXml = invDoc;
                signResult.Uuid = uuid.ToString();
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                    msg = $"InnerException: {ex.InnerException.Message}";

                if (!string.IsNullOrWhiteSpace(ex.Message))
                    msg += $"Message: {ex.Message}";
            }

            return signResult;
        }

        private SignResultDto SignDocumentFile(ComplianceResponseDto response, string privateKey, string filePath)
        {
            var signResult = new SignResultDto();
            try
            {
                //string basePath = Path.Combine(Directory.GetCurrentDirectory(), @"Files\SigningDevice\");
                string issueDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                string issueTime = DateTime.UtcNow.ToString("HH:mm:ss");
                string fullXml = string.Empty;
                using (var reader = new StreamReader(filePath))
                {
                    fullXml = reader.ReadToEnd();

                    //var fullXml = File.ReadAllText(filePath);
                    var uuid = Guid.NewGuid();
                    fullXml = fullXml.Replace("@UUID", uuid.ToString());
                    fullXml = fullXml.Replace("@IssueDate", issueDate);
                    fullXml = fullXml.Replace("@IssueTime", issueTime);
                    fullXml = fullXml.Replace("@PIH", "0".ToSha256HexBase64());
                    string csid = response.BinarySecurityToken.ToDecodeBase64();
                    var simplifiedDoc = fullXml.ToXmlDocument();
                    var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(simplifiedDoc);
                    var signer = _eInvoiceSigner.SignDocument(simplifiedDoc, csid, privateKey);

                    var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(signer.SignedEInvoice);
                    signResult.Signer = signer;
                    signResult.InvoiceHash = invoiceHash;
                    signResult.MasterDocumentXml = simplifiedDoc;
                    signResult.Uuid = uuid.ToString();

                    //var validator = new EInvoiceValidator();
                    //var resultValidator = validator.ValidateEInvoice(signer.SignedEInvoice, csid, privateKey);
                }
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                    msg = $"InnerException: {ex.InnerException.Message}";
                
                if (!string.IsNullOrWhiteSpace(ex.Message))
                    msg += $"Message: {ex.Message}";
            }

            return signResult;
        }


        //clearance
        public async Task SendInvoiceAsync(string invoiceHash, string uuid, string invoice)
        {
            var client = new HttpClient();

            // Headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("accept-language", "en");
            client.DefaultRequestHeaders.Add("Clearance-Status", "0");
            client.DefaultRequestHeaders.Add("Accept-Version", "V2");

            
            var requestBody = new 
            {
                invoiceHash,
                uuid,
                invoice
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            
            var response = await client.PostAsync(
                "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single",
                content
            );

            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }


    }
}
