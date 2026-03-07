using System;
using System.Collections.Generic;

namespace ZatcaIntegratorV2.Dto
{
    public class EInvoiceResponseDto
    {
        public ValidationResultDto ValidationResults { get; set; }
        public string ReportingStatus { get; set; }
        public string ClearanceStatus { get; set; }
        public string QrSellertStatus { get; set; }
        public string QrBuyertStatus { get; set; }
    }

    public class ValidationResultDto
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public class ValidationResultListDto
    {
        public List<ValidationResultDto> InfoMessages { get; set; }
        public List<object> WarningMessages { get; set; }
        public List<object> ErrorMessages { get; set; }
        public string Status { get; set; }
    }


}
