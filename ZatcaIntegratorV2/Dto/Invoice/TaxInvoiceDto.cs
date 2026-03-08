using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{

    //public class TaxTotalDto
    //{
    //    public decimal TaxAmount { get; set; }
    //    public TaxSubtotalDto TaxSubtotal { get; set; }
    //    public decimal RoundingAmount { get; set; }
    //}

    public class TaxTotalDto
    {
        public decimal TotalTaxAmount { get; set; }
        public List<TaxSubtotalDto> TaxSubtotals { get; set; }
    }


    public class TaxSubtotalDto
    {
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public VatCategoryCode TaxCategoryId { get; set; } = VatCategoryCode.Standard;
        public string? TaxExemptionReasonCode { get; set; }
        public string? TaxExemptionReason { get; set; }
    }


    public class TaxCategoryDto
    {
        public VatCategoryCode ID { get; set; } = VatCategoryCode.Standard;
        public decimal Percent { get; set; }
        public string? TaxExemptionReasonCode { get; set; }
        public string? TaxExemptionReason { get; set; }
        public string? TaxScheme { get; set; } = "VAT";
    }

    public class LegalMonetaryTotalDto
    {
        public decimal? LineExtensionAmount { get; set; }
        public decimal? TaxExclusiveAmount { get; set; }
        public decimal? AllowanceTotalAmount { get; set; }
        public decimal? ChargeTotalAmount { get; set; }
        public decimal? TaxInclusiveAmount { get; set; }
        public decimal? PrepaidAmount { get; set; }
        public decimal? PayableAmount { get; set; }
    }

}
