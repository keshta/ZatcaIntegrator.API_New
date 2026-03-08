using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.Model
{
    public class InvoiceSingleRequestModel
    {
        public ComplianceResponseDto Compliance { get; set; }
        public string PrivateKey { get; set; }
        public InvoiceDto InvoiceDate { get; set; }
        public string Uuid { get; set; }
        public InvoiceSingleType SingleType { get; set; } = InvoiceSingleType.Standard;
        public InvoiceDocumentType DocumentType { get; set; }
    }

    public enum InvoiceSingleType
    {
        Standard=1,   // Standard Invoice
        Simplified  // Simplified Invoice
    }

    public enum InvoiceDocumentType
    {
        Invoice=1,    // فاتورة
        Credit,     // فاتورة ائتمان
        Debit       // فاتورة خصم
    }

}
