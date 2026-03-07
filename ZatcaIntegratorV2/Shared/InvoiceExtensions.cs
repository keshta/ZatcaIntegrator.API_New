using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.Shared
{
    public static class InvoiceExtensions
    {
       
        public static decimal GetVatPercentage(this VatCategoryCode code)
        {
            return code switch
            {
                VatCategoryCode.Standard => 15m,
                VatCategoryCode.ZeroRated => 0m,
                VatCategoryCode.Exempt => 0m,
                VatCategoryCode.OutsideScope => 0m,
                _ => 0m
            };
        }

        public static string GetVatDescription(this VatCategoryCode code)
        {
            return code switch
            {
                VatCategoryCode.Standard => "S",
                VatCategoryCode.ZeroRated => "Z",
                VatCategoryCode.Exempt => "E",
                VatCategoryCode.OutsideScope => "O",
                _ => ""
            };
        }

        public static XDocument RemovePartyTaxScheme(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException("XML content is empty.");

            XDocument doc = XDocument.Parse(xml);

            XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";

            doc.Descendants(cac + "AccountingSupplierParty")
               .Descendants(cac + "PartyTaxScheme")
               .Remove();

            return doc;
        }
    }
}
