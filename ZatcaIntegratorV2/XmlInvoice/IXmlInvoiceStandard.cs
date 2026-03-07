using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.XmlInvoice
{
    public interface IXmlInvoiceStandard
    {
        Task<string> GenerateXmlInvoiceAsync(InvoiceDto invoiceInfo);
        Task<string> GenerateXmlCreditAsync(InvoiceDto invoiceInfo);
        Task<string> GenerateXmlDebitAsync(InvoiceDto invoiceInfo);
    }
}
