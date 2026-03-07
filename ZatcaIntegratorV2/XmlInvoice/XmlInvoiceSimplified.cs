using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.XmlInvoice
{
    public class XmlInvoiceSimplified : IXmlInvoiceSimplified
    {
        private IXmlInvoiceSection _xmlInvoiceSection = new XmlInvoiceSection();

        public async Task<string> GenerateXmlInvoiceAsync(InvoiceDto invoiceInfo)
        {
            InvoiceModel invoice = await _xmlInvoiceSection.InvoiceAsync(invoiceInfo);
            invoice.InvoiceTypeCode = new InvoiceTypeCodeModel();
            invoice.InvoiceTypeCode.Name = Transactions.SimplifiedCode;
            invoice.InvoiceTypeCode.Value = InvoiceType.Invoice.GetEnumValueAsString();
            var xmlString = invoice.SerializeInvoiceMap();
            return xmlString;
        }


        public async Task<string> GenerateXmlCreditAsync(InvoiceDto invoiceInfo)
        {
            var invoice = await _xmlInvoiceSection.InvoiceAsync(invoiceInfo);
            invoice.InvoiceTypeCode = new InvoiceTypeCodeModel();
            invoice.InvoiceTypeCode.Name = Transactions.SimplifiedCode;
            invoice.InvoiceTypeCode.Value = InvoiceType.CreditNote.GetEnumValueAsString();

            if (!string.IsNullOrWhiteSpace(invoiceInfo.PaymentMeans.InstructionNote))
                invoice.PaymentMeans.InstructionNote = invoiceInfo.PaymentMeans.InstructionNote;

            var xmlString = invoice.SerializeInvoiceMap();
            return xmlString;
        }

        public async Task<string> GenerateXmlDebitAsync(InvoiceDto invoiceInfo)
        {
            var invoice = await _xmlInvoiceSection.InvoiceAsync(invoiceInfo);
            invoice.InvoiceTypeCode = new InvoiceTypeCodeModel();
            invoice.InvoiceTypeCode.Name = Transactions.SimplifiedCode;
            invoice.InvoiceTypeCode.Value = InvoiceType.DebitNote.GetEnumValueAsString();

            if (!string.IsNullOrWhiteSpace(invoiceInfo.PaymentMeans.InstructionNote))
                invoice.PaymentMeans.InstructionNote = invoiceInfo.PaymentMeans.InstructionNote;

            var xmlString = invoice.SerializeInvoiceMap();
            return xmlString;
        }

    }
}
