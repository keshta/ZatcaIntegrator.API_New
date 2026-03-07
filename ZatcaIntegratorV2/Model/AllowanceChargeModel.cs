using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    public class AllowanceChargeModel
    {
        [XmlElement("ChargeIndicator", Namespace = UblNamespaces.Cbc, Order = 1)]
        public bool ChargeIndicator { get; set; } // false = خصم، true = زيادة

        [XmlElement("AllowanceChargeReasonCode", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string? AllowanceChargeReasonCode { get; set; }

        [XmlElement("AllowanceChargeReason", Namespace = UblNamespaces.Cbc, Order = 3)]
        public string AllowanceChargeReason { get; set; }

        [XmlElement("Amount", Namespace = UblNamespaces.Cbc, Order = 4)]
        public AmountModel Amount { get; set; }

        [XmlElement("BaseAmount", Namespace = UblNamespaces.Cbc, Order = 5)]
        public AmountModel? BaseAmount { get; set; } // المبلغ الأساسي الذي يحسب منه الخصم

        //[XmlElement("MultiplierFactorNumeric", Namespace = UblNamespaces.Cbc, Order = 6)]
        //public decimal? MultiplierFactorNumeric { get; set; } = null; // نسبة الخصم (0.10 = 10%)

        [XmlElement("TaxCategory", Namespace = UblNamespaces.Cac, Order = 6)]
        public TaxCategoryModel? TaxCategory { get; set; } // اختياري، فقط إذا الخصم يؤثر على الضريبة
    }
}
