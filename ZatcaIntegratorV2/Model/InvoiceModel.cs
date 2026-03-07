using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{

    [XmlRoot("Invoice", Namespace = UblNamespaces.Invoice)]
    public class InvoiceModel
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = UblNamespaces.Namespaces;

        [XmlElement("UBLExtensions", Namespace = UblNamespaces.Ext, Order = 1)]
        public UBLExtensions UBLExtensions { get; set; }

        [XmlElement("ProfileID", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string ProfileID { get; set; } = "reporting:1.0";

        [XmlElement("ID", Namespace = UblNamespaces.Cbc, Order = 3)]
        public string ID { get; set; }

        [XmlElement("UUID", Namespace = UblNamespaces.Cbc, Order = 4)]
        public string UUID { get; set; }

        [XmlElement("IssueDate", Namespace = UblNamespaces.Cbc, Order = 5)]
        public string IssueDate { get; set; }

        [XmlElement("IssueTime", Namespace = UblNamespaces.Cbc, Order = 6)]
        public string IssueTime { get; set; }

        [XmlElement("InvoiceTypeCode", Namespace = UblNamespaces.Cbc, Order = 7)]
        public InvoiceTypeCodeModel InvoiceTypeCode { get; set; }

        [XmlElement("DocumentCurrencyCode", Namespace = UblNamespaces.Cbc, Order = 8)]
        public string DocumentCurrencyCode { get; set; }

        [XmlElement("TaxCurrencyCode", Namespace = UblNamespaces.Cbc, Order = 9)]
        public string TaxCurrencyCode { get; set; }
        
        [XmlElement("BillingReference", Namespace = UblNamespaces.Cac, Order = 10)]
        public List<BillingReferenceModel>? BillingReferences { get; set; }

        [XmlElement("AdditionalDocumentReference", Namespace = UblNamespaces.Cac, Order = 11)]
        public List<AdditionalDocumentReference> AdditionalDocumentReferences { get; set; }

        [XmlElement("Signature", Namespace = UblNamespaces.Cac, Order = 12)]
        public Signature Signature { get; set; }

        [XmlElement("AccountingSupplierParty", Namespace = UblNamespaces.Cac, Order = 13)]
        public AccountCustomerOrSupplierPartyModel AccountingSupplierParty { get; set; }

        [XmlElement("AccountingCustomerParty", Namespace = UblNamespaces.Cac, Order = 14)]
        public AccountCustomerOrSupplierPartyModel AccountingCustomerParty { get; set; }

        [XmlElement("Delivery", Namespace = UblNamespaces.Cac, Order = 15)]
        public DeliveryModel Delivery { get; set; }

        [XmlElement("PaymentMeans", Namespace = UblNamespaces.Cac, Order = 16)]
        public PaymentMeansModel PaymentMeans { get; set; }

        [XmlElement("AllowanceCharge", Namespace = UblNamespaces.Cac, Order = 17)]
        public List<AllowanceChargeModel>? AllowanceCharges { get; set; }


        [XmlElement("TaxTotal", Namespace = UblNamespaces.Cac, Order = 18)]
        public TaxTotalModel TaxTotal { get; set; }

        [XmlElement("LegalMonetaryTotal", Namespace = UblNamespaces.Cac, Order = 19)]
        public LegalMonetaryTotalModel LegalMonetaryTotal { get; set; }

        [XmlElement("InvoiceLine", Namespace = UblNamespaces.Cac, Order = 20)]
        public List<InvoiceLineModel> InvoiceLines { get; set; }
    }


    public class InvoiceTypeCodeModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }


    public class AdditionalDocumentReference
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc)]
        public string ID { get; set; }

        [XmlElement("UUID", Namespace = UblNamespaces.Cbc)]
        public string? UUID { get; set; }

        [XmlElement("Attachment", Namespace = UblNamespaces.Cac)]
        public Attachment? Attachment { get; set; }
    }

    public class Attachment
    {
        [XmlElement("EmbeddedDocumentBinaryObject", Namespace = UblNamespaces.Cbc)]
        public EmbeddedDocumentBinaryObject EmbeddedDocumentBinaryObject { get; set; }
    }

    public class EmbeddedDocumentBinaryObject
    {
        [XmlAttribute("mimeCode")]
        public string MimeCode { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class Signature
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string ID { get; set; }

        [XmlElement("SignatureMethod", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string SignatureMethod { get; set; }
    }


    public class BillingReferenceModel
    {
        [XmlElement("InvoiceDocumentReference", Namespace = UblNamespaces.Cac)]
        public InvoiceDocumentReferenceModel InvoiceDocumentReference { get; set; }
    }

    public class InvoiceDocumentReferenceModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc)]
        public string ID { get; set; }

        [XmlElement("IssueDate", Namespace = UblNamespaces.Cbc)]
        public string IssueDate { get; set; }  // yyyy-MM-dd
    }

}
