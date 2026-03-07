using System;
using System.Collections.Generic;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.XmlInvoice
{
    internal interface IXmlInvoiceSimplified
    {
        Task<string> GenerateXmlInvoiceAsync(InvoiceDto invoiceInfo);
        Task<string> GenerateXmlCreditAsync(InvoiceDto invoiceInfo);
        Task<string> GenerateXmlDebitAsync(InvoiceDto invoiceInfo);
    }
}
