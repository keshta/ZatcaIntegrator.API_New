using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace ZatcaIntegratorV2.Dto
{
    public class SignResultDto
    {
        public SignResult Signer { get; set; }
        public HashResult InvoiceHash { get; set; }
        public XmlDocument MasterDocumentXml { get; set; }
        public string Uuid { get; set; }
    }

}
