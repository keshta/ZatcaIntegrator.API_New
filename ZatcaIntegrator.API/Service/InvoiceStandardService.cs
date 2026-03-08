using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.XmlInvoice;

namespace ZatcaIntegrator.API.Service
{
    public class InvoiceStandardService : IInvoiceStandardService
    {
        readonly IXmlInvoiceStandard _xmlInvoiceStandard;
        public InvoiceStandardService(IXmlInvoiceStandard xmlInvoiceStandard)
        {
            _xmlInvoiceStandard = xmlInvoiceStandard;
        }

        public async Task<string> GenerateXmlCreditAsync(InvoiceDto model)
        {
            try
            {
                var invoiceXml = await _xmlInvoiceStandard.GenerateXmlCreditAsync(model);
                return invoiceXml;
            }
            catch (Exception ex)
            {
               return string.Empty;
            }
        }

        public async Task<string> GenerateXmlDebitAsync(InvoiceDto model)
        {
            try
            {
                var invoiceXml = await _xmlInvoiceStandard.GenerateXmlDebitAsync(model);
                return invoiceXml;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<string> GenerateXmlInvoiceAsync(InvoiceDto model)
        {
            try
            {
                var invoiceXml = await _xmlInvoiceStandard.GenerateXmlInvoiceAsync(model);
                return invoiceXml;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


    }
}
