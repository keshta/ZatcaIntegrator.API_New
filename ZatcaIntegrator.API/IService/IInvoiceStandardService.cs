using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.IService
{
    public interface IInvoiceStandardService
    {
        Task<string> GenerateXmlInvoiceAsync(InvoiceDto model);
        Task<string> GenerateXmlCreditAsync(InvoiceDto model);
        Task<string> GenerateXmlDebitAsync(InvoiceDto model);
    }
}
