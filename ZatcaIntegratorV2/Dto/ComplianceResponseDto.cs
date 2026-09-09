using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZatcaIntegratorV2.Dto
{
    public class ComplianceResponseDto
    {
        public long? RequestID { get; set; }
        public string? DispositionMessage { get; set; }
        public string? BinarySecurityToken { get; set; }
        public string? Secret { get; set; }
    }


    public class ComplianceErrorDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ComplianceErrorListDto
    {
        public List<ComplianceErrorDto> Errors { get; set; }
    }


    public class ComplianceResultDto
    {
        public ComplianceResponseDto Response { get; set; } = new ComplianceResponseDto();
        public List<ComplianceErrorDto> Errors { get; set; } = new List<ComplianceErrorDto>();
        public bool IsSuccess => Errors == null || Errors.Count == 0;
        public string ResponseJson { get; set; }
    }
}
