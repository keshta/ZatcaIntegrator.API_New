using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    public class InvoiceTaxTotalModel
    {
        [XmlElement("TaxAmount", Namespace = UblNamespaces.Cbc, Order = 1)]
        public AmountModel? TaxAmount { get; set; }

        [XmlElement("RoundingAmount", Namespace = UblNamespaces.Cbc, Order = 2)]
        public AmountModel? RoundingAmount { get; set; }
    }

    public class TaxTotalModel
    {
        [XmlElement("TaxAmount", Namespace = UblNamespaces.Cbc, Order = 1)]
        public AmountModel? TaxAmount { get; set; }

        [XmlElement("TaxSubtotal", Namespace = UblNamespaces.Cac, Order = 2)]
        public List<TaxSubtotalModel>? TaxSubtotals { get; set; }

        [XmlElement("RoundingAmount", Namespace = UblNamespaces.Cbc, Order = 3)]
        public AmountModel? RoundingAmount { get; set; }
    }

    public class TaxSubtotalModel
    {
        [XmlElement("TaxableAmount", Namespace = UblNamespaces.Cbc, Order =1)]
        public AmountModel TaxableAmount { get; set; }

        [XmlElement("TaxAmount", Namespace = UblNamespaces.Cbc, Order = 2)]
        public AmountModel TaxAmount { get; set; }

        [XmlElement("TaxCategory", Namespace = UblNamespaces.Cac, Order = 3)]
        public TaxCategoryModel TaxCategory { get; set; }
    }

    public class TaxCategoryModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string ID { get; set; }

        [XmlElement("Percent", Namespace = UblNamespaces.Cbc, Order = 2)]
        public decimal Percent { get; set; }

        [XmlElement("TaxExemptionReasonCode", Namespace = UblNamespaces.Cbc, Order = 3)]
        public string? TaxExemptionReasonCode { get; set; }

        [XmlElement("TaxExemptionReason", Namespace = UblNamespaces.Cbc, Order = 4)]
        public string? TaxExemptionReason { get; set; }

        [XmlElement("TaxScheme", Namespace = UblNamespaces.Cac, Order = 5)]
        public TaxSchemeModel TaxScheme { get; set; }
    }

    public class AmountModel
    {
        [XmlAttribute("currencyID")]
        public string CurrencyID { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }

    public class LegalMonetaryTotalModel
    {
        // Total of all invoice line amounts (before discounts/charges)
        [XmlElement("LineExtensionAmount", Namespace = UblNamespaces.Cbc, Order = 1)]
        public AmountModel LineExtensionAmount { get; set; }

        // Tax-exclusive total (sum of line amounts minus allowances plus charges)
        [XmlElement("TaxExclusiveAmount", Namespace = UblNamespaces.Cbc, Order = 2)]
        public AmountModel? TaxExclusiveAmount { get; set; }

        // Tax-inclusive total (TaxExclusiveAmount + total tax)
        [XmlElement("TaxInclusiveAmount", Namespace = UblNamespaces.Cbc, Order = 3)]
        public AmountModel? TaxInclusiveAmount { get; set; }

        // Total allowances (discounts) at document level (BT-107)
        [XmlElement("AllowanceTotalAmount", Namespace = UblNamespaces.Cbc, Order = 4)]
        public AmountModel? AllowanceTotalAmount { get; set; }

        // Total charges at document level (BT-108)
        [XmlElement("ChargeTotalAmount", Namespace = UblNamespaces.Cbc, Order = 5)]
        public AmountModel? ChargeTotalAmount { get; set; }

        // Prepaid amount (optional)
        [XmlElement("PrepaidAmount", Namespace = UblNamespaces.Cbc, Order = 6)]
        public AmountModel? PrepaidAmount { get; set; }

        // Rounding amount (optional)
        [XmlElement("PayableRoundingAmount", Namespace = UblNamespaces.Cbc, Order = 7)]
        public AmountModel? PayableRoundingAmount { get; set; }

        // Payable amount (final total due) - mandatory
        [XmlElement("PayableAmount", Namespace = UblNamespaces.Cbc, Order = 8)]
        public AmountModel PayableAmount { get; set; }
    }

    public class TaxSchemeModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc)]
        public string ID { get; set; }
    }
}
