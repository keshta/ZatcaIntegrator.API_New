using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class InvoiceLineDto
    {
        public string? Id { get; set; }
        public UnitCodeType UnitCodeType { get; set; } = UnitCodeType.PCE;
        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal? TotalAmountWithTax { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? DiscountReason { get; set; }
        public decimal RoundingAmount { get; set; }
        public string ItemName { get; set; }
        public VatCategoryCode TaxCategoryId { get; set; } = VatCategoryCode.Standard;
        //public TaxSubtotalDto TaxSubtotal { get; set; }
        public decimal PriceAmount { get; set; }
    }

    public class InvoiceReturnDto 
    {
        public string Id { get; set; }
        public DateTime? IssueDate { get; set; }
    }

}
