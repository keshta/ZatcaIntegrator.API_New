using System.Xml;
using Zatca.EInvoice.SDK;
using Zatca.EInvoice.SDK.Contracts;
using Zatca.EInvoice.SDK.Utilities;
using ZatcaIntegrator.API.IService;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.XmlInvoice;
//using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Service
{
    public class SingleInvoiceService : ISingleInvoiceService
    {
        private readonly IInvoiceSingleService _invoiceSingleService;
        private readonly IComplianceAPIService _complianceAPIService;
        private readonly IEnvironmentService _environmentService;
        private readonly IInvoiceStandardService _invoiceStandardService;
        IEInvoiceHashGenerator _eInvoiceHashGenerator = new EInvoiceHashGenerator();
        IEInvoiceSigner _eInvoiceSigner = new EInvoiceSigner();
        EInvoiceQRGenerator _eInvoiceQRGenerator = new EInvoiceQRGenerator();
        IXmlInvoiceStandard _xmlInvoiceStandard = new XmlInvoiceStandard();

        public SingleInvoiceService(IInvoiceSingleService invoiceSingleService,
                                    IComplianceAPIService complianceAPIService,
                                    IEnvironmentService environmentService,
                                    IInvoiceStandardService invoiceStandardService) 
        {
            _invoiceSingleService = invoiceSingleService;
            _complianceAPIService = complianceAPIService;
            _environmentService = environmentService;
            _invoiceStandardService = invoiceStandardService;
        }

        public async Task<InvoiceSingleClearanceResultDto> ClearanceAsync(InvoiceSingleRequestDto model)
        {
            var uuid = Guid.Parse(model.Uuid);
            var xml = await GetInvoiceXml(model);
            var xmlDocument = xml.ToXmlDocumentNormalize();
            var env = await _environmentService.GetCurrentEnvironmentAsync();
            //model.Compliance.BinarySecurityToken = "TUlJRDNqQ0NBNFNnQXdJQkFnSVRFUUFBT0FQRjkwQWpzL3hjWHdBQkFBQTRBekFLQmdncWhrak9QUVFEQWpCaU1SVXdFd1lLQ1pJbWlaUHlMR1FCR1JZRmJHOWpZV3d4RXpBUkJnb0praWFKay9Jc1pBRVpGZ05uYjNZeEZ6QVZCZ29Ka2lhSmsvSXNaQUVaRmdkbGVIUm5ZWHAwTVJzd0dRWURWUVFERXhKUVVscEZTVTVXVDBsRFJWTkRRVFF0UTBFd0hoY05NalF3TVRFeE1Ea3hPVE13V2hjTk1qa3dNVEE1TURreE9UTXdXakIxTVFzd0NRWURWUVFHRXdKVFFURW1NQ1FHQTFVRUNoTWRUV0Y0YVcxMWJTQlRjR1ZsWkNCVVpXTm9JRk4xY0hCc2VTQk1WRVF4RmpBVUJnTlZCQXNURFZKcGVXRmthQ0JDY21GdVkyZ3hKakFrQmdOVkJBTVRIVlJUVkMwNE9EWTBNekV4TkRVdE16azVPVGs1T1RrNU9UQXdNREF6TUZZd0VBWUhLb1pJemowQ0FRWUZLNEVFQUFvRFFnQUVvV0NLYTBTYTlGSUVyVE92MHVBa0MxVklLWHhVOW5QcHgydmxmNHloTWVqeThjMDJYSmJsRHE3dFB5ZG84bXEwYWhPTW1Obzhnd25pN1h0MUtUOVVlS09DQWdjd2dnSURNSUd0QmdOVkhSRUVnYVV3Z2FLa2daOHdnWnd4T3pBNUJnTlZCQVFNTWpFdFZGTlVmREl0VkZOVWZETXRaV1F5TW1ZeFpEZ3RaVFpoTWkweE1URTRMVGxpTlRndFpEbGhPR1l4TVdVME5EVm1NUjh3SFFZS0NaSW1pWlB5TEdRQkFRd1BNems1T1RrNU9UazVPVEF3TURBek1RMHdDd1lEVlFRTURBUXhNVEF3TVJFd0R3WURWUVFhREFoU1VsSkVNamt5T1RFYU1CZ0dBMVVFRHd3UlUzVndjR3g1SUdGamRHbDJhWFJwWlhNd0hRWURWUjBPQkJZRUZFWCtZdm1tdG5Zb0RmOUJHYktvN29jVEtZSzFNQjhHQTFVZEl3UVlNQmFBRkp2S3FxTHRtcXdza0lGelZ2cFAyUHhUKzlObk1Ic0dDQ3NHQVFVRkJ3RUJCRzh3YlRCckJnZ3JCZ0VGQlFjd0FvWmZhSFIwY0RvdkwyRnBZVFF1ZW1GMFkyRXVaMjkyTG5OaEwwTmxjblJGYm5KdmJHd3ZVRkphUlVsdWRtOXBZMlZUUTBFMExtVjRkR2RoZW5RdVoyOTJMbXh2WTJGc1gxQlNXa1ZKVGxaUFNVTkZVME5CTkMxRFFTZ3hLUzVqY25Rd0RnWURWUjBQQVFIL0JBUURBZ2VBTUR3R0NTc0dBUVFCZ2pjVkJ3UXZNQzBHSlNzR0FRUUJnamNWQ0lHR3FCMkUwUHNTaHUyZEpJZk8reG5Ud0ZWbWgvcWxaWVhaaEQ0Q0FXUUNBUkl3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdNR0NDc0dBUVVGQndNQ01DY0dDU3NHQVFRQmdqY1ZDZ1FhTUJnd0NnWUlLd1lCQlFVSEF3TXdDZ1lJS3dZQkJRVUhBd0l3Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUxFL2ljaG1uV1hDVUtVYmNhM3ljaThvcXdhTHZGZEhWalFydmVJOXVxQWJBaUE5aEM0TThqZ01CQURQU3ptZDJ1aVBKQTZnS1IzTEUwM1U3NWVxYkMvclhBPT0=";
            //model.Compliance.Secret = "CkYsEXfV8c1gFHAtFWoZv73pGMvh/Qyo4LzKM2h/8Hg=";

            //var apiTest = await _complianceAPIService.SendComplianceDocumnet(model.Compliance, model.PrivateKey, xml, uuid);

            var result = await _invoiceSingleService.ClearanceAsync(model, env);

            return result;
        }

        public async Task<InvoiceSingleReportingResultDto> ReportingAsync(InvoiceSingleRequestDto model)
        {
            var uuid = Guid.Parse(model.Uuid);
            var xml = await GetInvoiceXml(model);
            var xmlDocument = xml.ToXmlDocumentNormalize();
            var env = await _environmentService.GetCurrentEnvironmentAsync();
            //model.Compliance.BinarySecurityToken = "TUlJRDNqQ0NBNFNnQXdJQkFnSVRFUUFBT0FQRjkwQWpzL3hjWHdBQkFBQTRBekFLQmdncWhrak9QUVFEQWpCaU1SVXdFd1lLQ1pJbWlaUHlMR1FCR1JZRmJHOWpZV3d4RXpBUkJnb0praWFKay9Jc1pBRVpGZ05uYjNZeEZ6QVZCZ29Ka2lhSmsvSXNaQUVaRmdkbGVIUm5ZWHAwTVJzd0dRWURWUVFERXhKUVVscEZTVTVXVDBsRFJWTkRRVFF0UTBFd0hoY05NalF3TVRFeE1Ea3hPVE13V2hjTk1qa3dNVEE1TURreE9UTXdXakIxTVFzd0NRWURWUVFHRXdKVFFURW1NQ1FHQTFVRUNoTWRUV0Y0YVcxMWJTQlRjR1ZsWkNCVVpXTm9JRk4xY0hCc2VTQk1WRVF4RmpBVUJnTlZCQXNURFZKcGVXRmthQ0JDY21GdVkyZ3hKakFrQmdOVkJBTVRIVlJUVkMwNE9EWTBNekV4TkRVdE16azVPVGs1T1RrNU9UQXdNREF6TUZZd0VBWUhLb1pJemowQ0FRWUZLNEVFQUFvRFFnQUVvV0NLYTBTYTlGSUVyVE92MHVBa0MxVklLWHhVOW5QcHgydmxmNHloTWVqeThjMDJYSmJsRHE3dFB5ZG84bXEwYWhPTW1Obzhnd25pN1h0MUtUOVVlS09DQWdjd2dnSURNSUd0QmdOVkhSRUVnYVV3Z2FLa2daOHdnWnd4T3pBNUJnTlZCQVFNTWpFdFZGTlVmREl0VkZOVWZETXRaV1F5TW1ZeFpEZ3RaVFpoTWkweE1URTRMVGxpTlRndFpEbGhPR1l4TVdVME5EVm1NUjh3SFFZS0NaSW1pWlB5TEdRQkFRd1BNems1T1RrNU9UazVPVEF3TURBek1RMHdDd1lEVlFRTURBUXhNVEF3TVJFd0R3WURWUVFhREFoU1VsSkVNamt5T1RFYU1CZ0dBMVVFRHd3UlUzVndjR3g1SUdGamRHbDJhWFJwWlhNd0hRWURWUjBPQkJZRUZFWCtZdm1tdG5Zb0RmOUJHYktvN29jVEtZSzFNQjhHQTFVZEl3UVlNQmFBRkp2S3FxTHRtcXdza0lGelZ2cFAyUHhUKzlObk1Ic0dDQ3NHQVFVRkJ3RUJCRzh3YlRCckJnZ3JCZ0VGQlFjd0FvWmZhSFIwY0RvdkwyRnBZVFF1ZW1GMFkyRXVaMjkyTG5OaEwwTmxjblJGYm5KdmJHd3ZVRkphUlVsdWRtOXBZMlZUUTBFMExtVjRkR2RoZW5RdVoyOTJMbXh2WTJGc1gxQlNXa1ZKVGxaUFNVTkZVME5CTkMxRFFTZ3hLUzVqY25Rd0RnWURWUjBQQVFIL0JBUURBZ2VBTUR3R0NTc0dBUVFCZ2pjVkJ3UXZNQzBHSlNzR0FRUUJnamNWQ0lHR3FCMkUwUHNTaHUyZEpJZk8reG5Ud0ZWbWgvcWxaWVhaaEQ0Q0FXUUNBUkl3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdNR0NDc0dBUVVGQndNQ01DY0dDU3NHQVFRQmdqY1ZDZ1FhTUJnd0NnWUlLd1lCQlFVSEF3TXdDZ1lJS3dZQkJRVUhBd0l3Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUxFL2ljaG1uV1hDVUtVYmNhM3ljaThvcXdhTHZGZEhWalFydmVJOXVxQWJBaUE5aEM0TThqZ01CQURQU3ptZDJ1aVBKQTZnS1IzTEUwM1U3NWVxYkMvclhBPT0=";
            //model.Compliance.Secret = "CkYsEXfV8c1gFHAtFWoZv73pGMvh/Qyo4LzKM2h/8Hg=";

            //var apiTest = await _complianceAPIService.SendComplianceDocumnet(model.Compliance, model.PrivateKey, xml, uuid);
            //.Compliance, model.PrivateKey, xmlDocument, uuid
            var result = await _invoiceSingleService.ReportingAsync(model, env);

            return result;
        }

        public async Task<InvoiceSingleQrCodeResultDto> GetQrCodeAsync(InvoiceSingleRequestDto model)
        {
            var result = new InvoiceSingleQrCodeResultDto();
            try
            {
                var uuid = Guid.Parse(model.Uuid);
                //var xml = await GetInvoiceXml(model);
                //var xmlDocument = xml.ToXmlDocumentNormalize();
                var env = await _environmentService.GetCurrentEnvironmentAsync();

                var xml = await GetInvoiceXml(model);
                var xmlDocument = xml.ToXmlDocumentNormalize();
                string csid = model.Compliance.BinarySecurityToken.ToDecodeBase64();
                var sign = _eInvoiceSigner.SignDocument(xmlDocument, csid, model.PrivateKey);
                //var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(xmlDocument);
                var invoiceHash = _eInvoiceHashGenerator.GenerateEInvoiceHashing(sign.SignedEInvoice);
                //var qr = _eInvoiceQRGenerator.GenerateEInvoiceQRCode(sign.SignedEInvoice);
                if (sign != null)
                {
                    if (sign.IsValid)
                    {
                        result.StatusCode = 200;
                        result.InvoiceQrCode = sign.SignedEInvoice._GetQR_CODE();
                    }
                    else
                    {
                        result.StatusCode = 400;
                        result.Error = sign.ErrorMessage;
                    }
                }
                else
                {
                    result.StatusCode = 500;
                    result.Error = "Failed to generate QR code.";
                }
            }
            catch (Exception ex)
            {
                string msg = "";
                result.StatusCode = 500;

                if (ex.InnerException != null)
                    msg = $"InnerException: {ex.InnerException.Message} " + Environment.NewLine;

                if (!string.IsNullOrEmpty(msg))
                    result.Error = $"{ex.Message}: {msg} "+ Environment.NewLine;
                else
                    result.Error = ex.Message;
            }

            return result;
        }


        private async Task<string> GetInvoiceXml(InvoiceSingleRequestDto model)
        {
            if(model == null)
                return string.Empty;

            if(model.SingleType == InvoiceSingleType.Standard)
            {
                if (model.DocumentType == InvoiceDocumentType.Credit)
                    return await _invoiceStandardService.GenerateXmlCreditAsync(model.InvoiceData);
                else if (model.DocumentType == InvoiceDocumentType.Debit)
                    return await _invoiceStandardService.GenerateXmlDebitAsync(model.InvoiceData);

                else
                    return await _invoiceStandardService.GenerateXmlInvoiceAsync(model.InvoiceData);
            }

            return string.Empty;
        }

    }
}
