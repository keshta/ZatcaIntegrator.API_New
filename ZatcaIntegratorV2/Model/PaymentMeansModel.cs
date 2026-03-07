using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    //public class PaymentMeans
    //{
    //    [XmlElement("PaymentMeansCode", Namespace = UblNamespaces.Cbc)]
    //    public string PaymentMeansCode { get; set; }
    //}


    public class PaymentMeansModel
    {
        [XmlElement("PaymentMeansCode", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string PaymentMeansCode { get; set; }

        [XmlElement("InstructionNote", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string? InstructionNote { get; set; }

        [XmlElement("PayeeFinancialAccount", Namespace = UblNamespaces.Cac, Order = 3)]
        public PayeeFinancialAccountModel? PayeeFinancialAccount { get; set; }
    }

    public class PayeeFinancialAccountModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string ID { get; set; }
    }

}
