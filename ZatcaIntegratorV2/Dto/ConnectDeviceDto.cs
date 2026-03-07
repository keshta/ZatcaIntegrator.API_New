using System;
using System.Collections.Generic;

namespace ZatcaIntegratorV2.Dto
{
    public class ConnectDeviceRequestDto
    {
        public CsrAndCsidRequestDto CsrRequest { get; set; }
        public string OTP { get; set; }
    }

    public class ConnectDeviceResultDto
    {
        public ConnectDeviceResultDataDto Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<ComplianceErrorDto> Errors { get; set; }
    }

    public class ConnectDeviceResultDataDto : CsrGeneratorDto
    {
        public long? RequestID { get; set; }
        public string? DispositionMessage { get; set; }
        public string? BinarySecurityToken { get; set; }
        public string? Secret { get; set; }
        public string? SerialNumber { get; set; }
    }

}
