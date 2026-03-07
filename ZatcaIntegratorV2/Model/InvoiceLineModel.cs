using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    public class InvoiceLineModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string ID { get; set; }

        [XmlElement("InvoicedQuantity", Namespace = UblNamespaces.Cbc, Order = 2)]
        public QuantityModel InvoicedQuantity { get; set; }

        [XmlElement("LineExtensionAmount", Namespace = UblNamespaces.Cbc, Order = 3)]
        public AmountModel LineExtensionAmount { get; set; }

        [XmlElement("LineExtensionAmountWithTax", Namespace = UblNamespaces.Cbc, Order = 4)]
        public AmountModel? LineExtensionAmountWithTax { get; set; }

        [XmlElement("AllowanceCharge", Namespace = UblNamespaces.Cac, Order = 5)]
        public AllowanceChargeModel? AllowanceCharge { get; set; }

        [XmlElement("TaxTotal", Namespace = UblNamespaces.Cac, Order = 6)]
        public TaxTotalModel? TaxTotal { get; set; }

        [XmlElement("Item", Namespace = UblNamespaces.Cac, Order = 7)]
        public ItemModel Item { get; set; }

        [XmlElement("Price", Namespace = UblNamespaces.Cac, Order = 8)]
        public PriceModel Price { get; set; }
    }

    public class QuantityModel
    {
        [XmlAttribute("unitCode")]
        public string UnitCode { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }

    public class ItemModel
    {
        [XmlElement("Name", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string Name { get; set; }

        [XmlElement("ClassifiedTaxCategory", Namespace = UblNamespaces.Cac, Order = 2)]
        public TaxCategoryModel ClassifiedTaxCategory { get; set; }
    }

    public class PriceModel
    {
        [XmlElement("PriceAmount", Namespace = UblNamespaces.Cbc)]
        public AmountModel PriceAmount { get; set; }
    }
}
