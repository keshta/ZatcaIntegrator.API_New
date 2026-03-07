using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Model
{

    public class AccountCustomerOrSupplierPartyModel
    {
        [XmlElement("Party", Namespace = UblNamespaces.Cac, Order =1)]
        public AccountPartyModel Party { get; set; }
    }

    [XmlType(Namespace = UblNamespaces.Cac)]
    public class AccountPartyModel
    {
        [XmlElement("PartyIdentification", Namespace = UblNamespaces.Cac, Order = 1)]
        public AccountPartyIdentificationModel PartyIdentification { get; set; }

        [XmlElement("PostalAddress", Namespace = UblNamespaces.Cac, Order = 2)]
        public AccountPostalAddressModel PostalAddress { get; set; }

        [XmlElement("PartyTaxScheme", Namespace = UblNamespaces.Cac, Order = 3)]
        public AccountPartyTaxSchemeModel PartyTaxScheme { get; set; }

        [XmlElement("PartyLegalEntity", Namespace = UblNamespaces.Cac, Order = 4)]
        public AccountPartyLegalEntityModel PartyLegalEntity { get; set; }
    }


    
    public class AccountPartyIdentificationModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc)]
        public string ID { get; set; }

        [XmlAttribute("schemeID")]
        public string SchemeID { get; set; }
    }


    [XmlType(Namespace = UblNamespaces.Cac)]
    public class AccountPartyIdentificationMapModel
    {
        [XmlElement("ID", Namespace = UblNamespaces.Cbc)]
        public CbcIDModel ID { get; set; }
    }


    [XmlType("ID", Namespace = UblNamespaces.Cbc)]
    public class CbcIDModel
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("schemeID")]
        public string SchemeID { get; set; }
    }

    public class AccountPostalAddressModel
    {
        [XmlElement("StreetName", Namespace = UblNamespaces.Cbc, Order = 1)]
        public string StreetName { get; set; }

        [XmlElement("BuildingNumber", Namespace = UblNamespaces.Cbc, Order = 2)]
        public string BuildingNumber { get; set; }

        [XmlElement("CitySubdivisionName", Namespace = UblNamespaces.Cbc, Order = 3)]
        public string CitySubdivisionName { get; set; }

        [XmlElement("CityName", Namespace = UblNamespaces.Cbc, Order = 4)]
        public string CityName { get; set; }

        [XmlElement("PostalZone", Namespace = UblNamespaces.Cbc, Order = 5)]
        public string PostalZone { get; set; }

        [XmlElement("Country", Namespace = UblNamespaces.Cac, Order = 6)]
        public AccountCountryModel Country { get; set; }
    }

    public class AccountCountryModel
    {
        [XmlElement("IdentificationCode", Namespace = UblNamespaces.Cbc)]
        public string IdentificationCode { get; set; }
    }

    public class AccountPartyTaxSchemeModel
    {
        [XmlElement("CompanyID", Namespace = UblNamespaces.Cbc, Order =1)]
        public string CompanyID { get; set; }

        [XmlElement("TaxScheme", Namespace = UblNamespaces.Cac, Order = 2)]
        public TaxSchemeModel TaxScheme { get; set; }
    }

    public class AccountPartyLegalEntityModel
    {
        [XmlElement("RegistrationName", Namespace = UblNamespaces.Cbc)]
        public string RegistrationName { get; set; }
    }

}
