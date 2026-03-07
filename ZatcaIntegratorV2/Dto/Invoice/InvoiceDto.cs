using System;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class InvoiceDto
    {
        // Invoice No
        public string ID { get; set; }
        public Guid UUID { get; set; }
        public string IssueDate { get; set; } = DateTime.UtcNow.ToDateInvoice();
        public string IssueTime { get; set; } = DateTime.UtcNow.ToTimeInvoice();
        public string ActualDeliveryDate { get; set; } = DateTime.UtcNow.ToDateInvoice();

        // Invoice Counter
        public string ICV { get; set; }
        public string PIH { get; set; }
        public bool IsReturnInvoice { get; set; } = false;

        // Company
        public AccountCustomerOrSupplierDto Supplier { get; set; }
        public AccountCustomerOrSupplierDto Customer { get; set; }
        public DeliveryDto Delivery { get; set; }
        public PaymentMeansDto PaymentMeans { get; set; }
        public List<AllowanceChargeDto> AllowanceCharges { get; set; }
        public TaxTotalDto TaxTotal { get; set; }
        public LegalMonetaryTotalDto LegalMonetaryTotal { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public List<InvoiceReturnDto>? InvoiceReturns { get; set; }
    }

    
}
