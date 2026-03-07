using System;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class AllowanceChargeDto
    {
        public bool ChargeIndicator { get; set; } = false;
        public string? ChargeReason { get; set; }
        public AllowanceChargeReasonCodeType? ChargeReasonCode { get; set; } = AllowanceChargeReasonCodeType.Discount;
        public decimal Amount { get; set; }
        public VatCategoryCode TaxCategoryId { get; set; } = VatCategoryCode.Standard;
        //public decimal Percent { get; set; }
    }

    
}
