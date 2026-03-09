using System;
using System.Collections.Generic;
using System.Xml;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.IService
{
    public interface IInvoiceSingleService
    {
        Task<InvoiceSingleClearanceResultDto> ClearanceAsync(InvoiceSingleRequestDto invoice, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        Task<InvoiceSingleReportingResultDto> ReportingAsync(InvoiceSingleRequestDto invoice, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
    }
}
