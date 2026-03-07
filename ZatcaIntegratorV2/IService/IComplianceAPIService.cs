using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.IService
{
    public interface IComplianceAPIService
    {
        Task<ComplianceResultDto> GetComplianceCSIDAsync(string csr, string otp, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        //Task<ComplianceResultDto> SignAllDocumentAsync(ComplianceResponseDto response, string privateKey, Dictionary<InvoiceTypeData, InvoiceDto> invoices, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        Task<ComplianceResultDto> SignAllDocumentAsync(ComplianceResponseDto response, string privateKey, AccountCustomerOrSupplierDto supplier, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        Task<ComplianceResultDto> SendComplianceDocumnet(ComplianceResponseDto compliance, string privateKey, string xml, Guid uuid, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
    }
}
