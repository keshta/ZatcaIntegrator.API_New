using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    public class DeliveryModel
    {
        [XmlElement("ActualDeliveryDate", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string? ActualDeliveryDate { get; set; }

        [XmlElement("LatestDeliveryDate", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string? LatestDeliveryDate { get; set; }
    }
}
