using System;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class PaymentMeansDto
    {
        public PaymentMeansCode PaymentMeansCode { get; set; } = PaymentMeansCode.Cash;
        public string? InstructionNote { get; set; }
        public string? IBAN { get; set; }
    }

    
}
