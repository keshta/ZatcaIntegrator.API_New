using System.ComponentModel.DataAnnotations;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.Model
{
    public class ConnectDeviceRequestModel : AccountCustomerOrSupplierDto
    {
        //[Required(AllowEmptyStrings = false)]
        //public string SerialNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CommercialName { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string TaxUnitName { get; set; }

        //public string CountryName { get; set; } = "SA";

        [Required(AllowEmptyStrings = false)]
        //public InvoiceTypeEnum InvoiceType { get; set; } = InvoiceTypeEnum.B2BAndB2C;
        public string InvoiceType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LocationAddress { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string IndustryBusinessCategory { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string OTP { get; set; }
    }
}
