using System;
using System.Xml.Serialization;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{

    public class VatItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string VatCategory { get; set; }
        public string ExemptionReasonCode { get; set; }
    }

    

}
