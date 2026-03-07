using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml;
using ZATCA.EInvoice.SDK;
using ZATCA.EInvoice.SDK.Contracts;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Service
{
    public class InvoiceSingleService : IInvoiceSingleService
    {
        IEInvoiceHashGenerator _eInvoiceHashGenerator = new EInvoiceHashGenerator();
        IEInvoiceSigner _eInvoiceSigner = new EInvoiceSigner();
        EInvoiceQRGenerator _eInvoiceQRGenerator = new EInvoiceQRGenerator();

        public InvoiceSingleService() { }

        public async Task<InvoiceSingleClearanceResultDto> ClearanceAsync(ComplianceResponseDto compliance, string privateKey, XmlDocument xmlDocument, Guid uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            string csid = compliance.BinarySecurityToken.ToDecodeBase64();
            var sign = _eInvoiceSigner.SignDocument(xmlDocument, csid, privateKey);
            //var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(xmlDocument);
            var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(sign.SignedEInvoice);
            var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(sign.SignedEInvoice);

            string url = environment switch
            {
                ZatcaEnvironmentType.Production => "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/invoices/clearance/single",
                ZatcaEnvironmentType.Simulation => "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/invoices/clearance/single",
                ZatcaEnvironmentType.NonProduction => "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/clearance/single",
                _ => throw new ArgumentOutOfRangeException(nameof(environment), "Invalid environment type")
            };

            var requestBody = new
            {
                invoiceHash = invoiceHash.Hash,
                uuid = uuid.ToString(),
                invoice = sign.SignedEInvoice.OuterXml.ToEncodeBase64()
            };

            var result = new InvoiceSingleClearanceResultDto();
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(5);
                string base64Auth = $"{compliance.BinarySecurityToken}:{compliance.Secret}".ToEncodeBase64();

                // Required headers
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("Accept-Version", "V2");
                client.DefaultRequestHeaders.Add("Clearance-Status", "0");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var json = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<InvoiceSingleClearanceResultDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result.Message = "Unauthorized";
                    }
                    else
                        result = JsonSerializer.Deserialize<InvoiceSingleClearanceResultDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                    result = JsonSerializer.Deserialize<InvoiceSingleClearanceResultDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                result.StatusCode = (int)response.StatusCode;
                result.InvoiceHash = requestBody.invoiceHash;
                result.UUID = requestBody.uuid;
            }
            catch (Exception ex)
            {
                string msg = "";
                if (!string.IsNullOrWhiteSpace(ex.Message))
                    msg += $"Message: {ex.Message}\n";

                if (ex.InnerException !=null)
                    msg += $"InnerException: {ex.InnerException}\n";

                result = new InvoiceSingleClearanceResultDto();
                result.StatusCode = 500;
                result.Message = msg;
            }

            return result;
        }

        public async Task<InvoiceSingleReportingResultDto> ReportingAsync(ComplianceResponseDto compliance, string privateKey, XmlDocument xmlDocument, Guid uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            string csid = compliance.BinarySecurityToken.ToDecodeBase64();
            var sign = _eInvoiceSigner.SignDocument(xmlDocument, csid, privateKey);
            //var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(xmlDocument);
            var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(sign.SignedEInvoice);
            var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(sign.SignedEInvoice);

            string url = environment switch
            {
                ZatcaEnvironmentType.Production => "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/invoices/reporting/single",
                ZatcaEnvironmentType.Simulation => "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/invoices/reporting/single",
                ZatcaEnvironmentType.NonProduction => "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single",
                _ => throw new ArgumentOutOfRangeException(nameof(environment), "Invalid environment type")
            };

            var requestBody = new
            {
                invoiceHash = invoiceHash.Hash,
                uuid = uuid.ToString(),
                invoice = sign.SignedEInvoice.OuterXml.ToEncodeBase64()
            };

            var result = new InvoiceSingleReportingResultDto();
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(5);
                string base64Auth = $"{compliance.BinarySecurityToken}:{compliance.Secret}".ToEncodeBase64();

                // Required headers
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("Accept-Version", "V2");
                client.DefaultRequestHeaders.Add("Clearance-Status", "0"); // important for reporting
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

                var json = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result.Message = "Unauthorized";
                    }
                    else
                        result = JsonSerializer.Deserialize<InvoiceSingleReportingResultDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                    result = JsonSerializer.Deserialize<InvoiceSingleReportingResultDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                result.StatusCode = (int)response.StatusCode;
                result.InvoiceHash = requestBody.invoiceHash;
                result.UUID = requestBody.uuid;
            }
            catch (Exception ex)
            {
                string msg = "";
                if (!string.IsNullOrWhiteSpace(ex.Message))
                    msg += $"Message: {ex.Message}\n";

                if (ex.InnerException != null)
                    msg += $"InnerException: {ex.InnerException}\n";

                result = new InvoiceSingleReportingResultDto();
                result.StatusCode = 500;
                result.Message = msg;
            }

            return result;
        }
    }
}
