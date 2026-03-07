using System;
using ZATCA.EInvoice.SDK.Contracts;
using ZATCA.EInvoice.SDK.Contracts.Models;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Service
{
    public class CsrGeneratorService : ICsrGeneratorService
    {
        private ICsrGenerator _csrGenerator;
        public CsrGeneratorService(ICsrGenerator csrGenerator)
        {
            _csrGenerator = csrGenerator;
        }
        public async Task<CsrGeneratorResultDto> GenerateAsync(CsrAndCsidRequestDto dto, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            var result = new CsrGeneratorResultDto();
            try
            {
                var csr = new CsrGenerationDto(commonName: dto.CommercialName, 
                    serialNumber: dto.SerialNumber,
                    organizationIdentifier: dto.TaxNumber,
                    organizationUnitName: dto.TaxUnitName,
                    organizationName: dto.TaxCompanyName,
                    countryName: dto.CountryName,
                    invoiceType: dto.InvoiceType,
                    locationAddress: dto.LocationAddress,
                    industryBusinessCategory: dto.IndustryBusinessCategory
                    );

                List<string> errors = new();
                csr.IsValid(out errors);
                if (errors.Count > 0)
                {
                    result.Errors = errors.Select(e => new ComplianceErrorDto { Message = e }).ToList();
                }
                else
                {
                    var csrAndPrivateKey = _csrGenerator.GenerateCsr(csr, environment.ToEnvironmentType(), false);
                    if (csrAndPrivateKey.IsValid)
                    {
                        var data = new CsrGeneratorDto();
                        data.CSR = csrAndPrivateKey.Csr;
                        data.PrivateKey = csrAndPrivateKey.PrivateKey;
                        result.Data = data;
                    }
                    else
                    {
                        result.Errors = csrAndPrivateKey.ErrorMessages.Select(e => new ComplianceErrorDto { Message = e }).ToList();
                    }
                }
            }

            catch (Exception ex)
            {
                result.Errors = new List<ComplianceErrorDto>();
                if (!string.IsNullOrWhiteSpace(ex.Message))
                    result.Errors.Add(new ComplianceErrorDto { Message = $"Message: {ex.Message}" });

                if (ex.InnerException != null)
                    result.Errors.Add(new ComplianceErrorDto { Message = $"InnerException: {ex.InnerException}" });
            }

            return result;
        }

    }
}
