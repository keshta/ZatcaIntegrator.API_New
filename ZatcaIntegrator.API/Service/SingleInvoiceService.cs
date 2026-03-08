using System.Xml;
using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;
//using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Service
{
    public class SingleInvoiceService : ISingleInvoiceService
    {
        private readonly IInvoiceSingleService _invoiceSingleService;
        private readonly IComplianceAPIService _complianceAPIService;
        private readonly IEnvironmentService _environmentService;
        private readonly IInvoiceStandardService _invoiceStandardService;
        
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

        public async Task<InvoiceSingleClearanceResultDto> ClearanceAsync(InvoiceSingleRequestModel model)
        {
            var uuid = Guid.Parse(model.Uuid);
            var xml = await GetInvoiceXml(model);
            var xmlDocument = xml.ToXmlDocumentNormalize();
            var env = await _environmentService.GetCurrentEnvironmentAsync();

            var apiTest = await _complianceAPIService.SendComplianceDocumnet(model.Compliance, model.PrivateKey, xml, uuid);

            var result = await _invoiceSingleService.ClearanceAsync(model.Compliance, model.PrivateKey, xmlDocument, uuid, env);

            return result;
        }

        public async Task<InvoiceSingleReportingResultDto> ReportingAsync(InvoiceSingleRequestModel model)
        {
            var uuid = Guid.Parse(model.Uuid);
            var xml = await GetInvoiceXml(model);
            var xmlDocument = xml.ToXmlDocumentNormalize();
            var env = await _environmentService.GetCurrentEnvironmentAsync();

            var apiTest = await _complianceAPIService.SendComplianceDocumnet(model.Compliance, model.PrivateKey, xml, uuid);

            var result = await _invoiceSingleService.ReportingAsync(model.Compliance, model.PrivateKey, xmlDocument, uuid, env);

            return result;
        }

        private async Task<string> GetInvoiceXml(InvoiceSingleRequestModel model)
        {
            if(model == null)
                return string.Empty;

            if(model.SingleType == InvoiceSingleType.Standard)
            {
                if (model.DocumentType == InvoiceDocumentType.Credit)
                    return await _invoiceStandardService.GenerateXmlCreditAsync(model.InvoiceDate);
                else if (model.DocumentType == InvoiceDocumentType.Debit)
                    return await _invoiceStandardService.GenerateXmlDebitAsync(model.InvoiceDate);

                else
                    return await _invoiceStandardService.GenerateXmlInvoiceAsync(model.InvoiceDate);
            }

            return string.Empty;
        }

    }
}
