using System;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public class AccountCustomerOrSupplierDto
    {
        public CompanyCommercialType CommercialType { get; set; } = CompanyCommercialType.CRN;
        public string CommercialNumber { get; set; }
        public string TaxNumber { get; set; }
        public string TaxCompanyName { get; set; }
        public string StreetName { get; set; }
        public string BuildNo { get; set; }
        public string CitySubdivisionName { get; set; }
        public string CityName { get; set; }
        public string PostalZone { get; set; }
        public string CountryCode { get; set; } = Transactions.DefaultCountryCode;

    }

}
