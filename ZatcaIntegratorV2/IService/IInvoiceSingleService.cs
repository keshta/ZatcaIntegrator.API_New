using System;
using System.Collections.Generic;
using System.Xml;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.IService
{
    public interface IInvoiceSingleService
    {
        Task<InvoiceSingleClearanceResultDto> ClearanceAsync(ComplianceResponseDto compliance, string privateKey, XmlDocument xmlDocument, Guid uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        Task<InvoiceSingleReportingResultDto> ReportingAsync(ComplianceResponseDto compliance, string privateKey, XmlDocument xmlDocument, Guid uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
    }
}
