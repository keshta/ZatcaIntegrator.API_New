using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.IService
{
    public interface ICsrGeneratorService
    {
        Task<CsrGeneratorResultDto> GenerateAsync(CsrAndCsidRequestDto dto, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);

    }
}
