using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Model;

namespace ZatcaIntegratorV2.XmlInvoice
{
    //InvoiceInfoDto invoiceInfo, 
    internal interface IXmlInvoiceSection
    {
        Task<InvoiceModel> InvoiceAsync(InvoiceDto invoiceInfo);
        Task<List<BillingReferenceModel>> BillingReferenceAsync(List<InvoiceReturnDto> invoiceReturns);
        Task<List<AdditionalDocumentReference>> AdditionalDocumentReferenceAsync(string icv, string pih);
        Task<AccountCustomerOrSupplierPartyModel> AccountSupplierAsync(AccountCustomerOrSupplierDto account);
        Task<AccountCustomerOrSupplierPartyModel> AccountCustomerAsync(AccountCustomerOrSupplierDto account);
        Task<DeliveryModel> DeliveryAsync(DeliveryDto delivery);
        Task<PaymentMeansModel> PaymentMeansAsync(PaymentMeansDto payment);
        Task<List<AllowanceChargeModel>> AllowanceChargeAsync(List<AllowanceChargeDto> allowances);
        Task<TaxTotalModel> TaxTotalAsync(TaxTotalDto taxTotal);
        Task<LegalMonetaryTotalModel> LegalMonetaryTotalAsync(LegalMonetaryTotalDto legalMonetaryTotal);
        Task<List<InvoiceLineModel>> InvoiceLinesAsync(List<InvoiceLineDto> invoiceLines);
    }
}
