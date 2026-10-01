using System;
using System.Collections.Generic;

namespace ZatcaIntegratorV2.Dto
{
    
    public class InvoiceSingleReportingDto
    {
    }

    public class InvoiceSingleReportingResultDto
    {
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        // HTTP status code (populate this from the HTTP response)
        public int StatusCode { get; set; }

        // Present in most responses (200, 202, 400, 409)
        public InvoiceSingleReportingValidationResultsDto? ValidationResults { get; set; }

        // Invoice reporting status (e.g., REPORTED, NOT_REPORTED)
        public string? ReportingStatus { get; set; }

        // Present in 401 Unauthorized responses
        public long? Timestamp { get; set; }
        public string? Error { get; set; }

        // Present in 500 Internal Server Error responses
        public string? Category { get; set; }
        public string? Code { get; set; }

        // General message (used in multiple scenarios)
        public string? Message { get; set; }
        public string? InvoiceHash { get; set; }
        public string? UUID { get; set; }
        public string? ResponseJson { get; set; }
        public string? InvoiceQrCode { get; set; }
    }

    public class InvoiceSingleReportingValidationResultsDto
    {
        public List<InvoiceSingleReportingValidationMessageDto>? InfoMessages { get; set; }
        public List<InvoiceSingleReportingValidationMessageDto>? WarningMessages { get; set; }
        public List<InvoiceSingleReportingValidationMessageDto>? ErrorMessages { get; set; }
        public string? Status { get; set; }
    }

    public class InvoiceSingleReportingValidationMessageDto
    {
        public string? Type { get; set; }
        public string? Code { get; set; }
        public string? Category { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
    }
}
