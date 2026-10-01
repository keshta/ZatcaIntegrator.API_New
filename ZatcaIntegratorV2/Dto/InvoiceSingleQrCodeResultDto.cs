using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZatcaIntegratorV2.Dto
{
    public class InvoiceSingleQrCodeResultDto
    {
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
        // HTTP status code (populate this from the HTTP response)
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public string? InvoiceQrCode { get; set; }
    }
}
