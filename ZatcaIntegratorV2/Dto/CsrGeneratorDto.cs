using System;
using System.Collections.Generic;

namespace ZatcaIntegratorV2.Dto
{
    public class CsrGeneratorDto
    {
        public int Id { get; set; }
        public string CSR { get; set; }
        public string PrivateKey { get; set; }
    }

    public class CsrGeneratorResultDto
    {
        public CsrGeneratorDto Data { get; set; }
        public bool IsSuccess => Errors == null || Errors.Count == 0;
        public List<ComplianceErrorDto> Errors { get; set; }
    }

}
