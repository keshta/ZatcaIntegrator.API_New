using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZatcaIntegratorV2.Dto
{
    public class CsrAndCsidRequestDto
    {
        // Company Name in (Commercial)
        public string CommercialName { get; set; }
        public string SerialNumber { get; set; }
        public string TaxNumber { get; set; }
        public string TaxUnitName { get; set; }

        // Company Name in (VAT)
        public string TaxCompanyName { get; set; }
        public string CountryName { get; set; } = "SA";
        public string InvoiceType { get; set; }
        public string LocationAddress { get; set; }
        public string IndustryBusinessCategory { get; set; }
    }
}
