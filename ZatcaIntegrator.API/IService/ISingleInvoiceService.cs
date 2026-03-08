using System.Xml;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.IService
{
    public interface ISingleInvoiceService
    {
        Task<InvoiceSingleClearanceResultDto> ClearanceAsync(InvoiceSingleRequestModel model);
        Task<InvoiceSingleReportingResultDto> ReportingAsync(InvoiceSingleRequestModel model);
    }
}
