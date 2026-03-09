using System;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class InvoiceSingleRequestDto
    {
        public ComplianceResponseDto Compliance { get; set; }
        public string PrivateKey { get; set; }
        public InvoiceDto InvoiceData { get; set; }
        public string Uuid { get; set; }
        public InvoiceSingleType SingleType { get; set; } = InvoiceSingleType.Standard;
        public InvoiceDocumentType DocumentType { get; set; } = InvoiceDocumentType.Invoice;
    }
}
