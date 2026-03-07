using System;
using System.Xml;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{
    public class UBLExtensions
    {
        [XmlElement("UBLExtension", Namespace = UblNamespaces.Ext)]
        public UBLExtension UBLExtension { get; set; }
    }

    public class UBLExtension
    {
        [XmlElement("ExtensionURI", Namespace = UblNamespaces.Ext)]
        public string ExtensionURI { get; set; }

        [XmlElement("ExtensionContent", Namespace = UblNamespaces.Ext)]
        public ExtensionContent ExtensionContent { get; set; }
    }

    public class ExtensionContent
    {
        [XmlAnyElement]
        public XmlElement Any { get; set; }
    }
}
