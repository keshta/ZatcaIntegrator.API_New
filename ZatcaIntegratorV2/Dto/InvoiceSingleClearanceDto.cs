using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZatcaIntegratorV2.Dto
{
    public class InvoiceSingleClearanceDto
    {

    }

    public class InvoiceSingleClearanceResultDto
    {
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        // HTTP status code (populate this from the HTTP response)
        public int StatusCode { get; set; }

        // These fields typically appear in responses 200, 202, 208, 400
        public InvoiceSingleClearanceValidationResultsDto ValidationResults { get; set; }
        public string ClearanceStatus { get; set; }      // CLEARED, NOT_CLEARED
        public string ClearedInvoice { get; set; }       // Can be empty or null

        // These fields cover other responses
        public string Message { get; set; }              // For example, the message in 303
        public long? Timestamp { get; set; }             // For 401
        public int? Status { get; set; }                 // For 401
        public string Error { get; set; }                // For 401
        public string Category { get; set; }             // For 500
        public string Code { get; set; }                 // For 500
        public string? InvoiceHash { get; set; }          
        public string? UUID { get; set; }
        public string ResponseJson { get; set; }
        public string? InvoiceQrCode { get; set; }
    }

    public class InvoiceSingleClearanceValidationResultsDto
    {
        public List<InvoiceSingleClearanceMessageDto> InfoMessages { get; set; } = new();
        public List<InvoiceSingleClearanceMessageDto> WarningMessages { get; set; } = new();
        public List<InvoiceSingleClearanceMessageDto> ErrorMessages { get; set; } = new();
        public string Status { get; set; }        // PASS, WARNING, ERROR
    }

    public class InvoiceSingleClearanceMessageDto
    {
        public string Type { get; set; }          // INFO, WARNING, ERROR
        public string Code { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }        // PASS, WARNING, ERROR
    }

}
