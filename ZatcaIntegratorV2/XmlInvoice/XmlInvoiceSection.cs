using System;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Model;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.XmlInvoice
{
    public class XmlInvoiceSection : IXmlInvoiceSection
    {
        public async Task<InvoiceModel> InvoiceAsync(InvoiceDto invoiceInfo)
        {

            if (invoiceInfo == null) 
                return null;

            var obj = new InvoiceModel();
            obj.ID = invoiceInfo.ID;
            obj.UUID = invoiceInfo.UUID.ToString();
            obj.IssueDate = invoiceInfo.IssueDate;
            obj.IssueTime = invoiceInfo.IssueTime;
            obj.DocumentCurrencyCode = Transactions.DefaultCurrency;
            obj.TaxCurrencyCode = Transactions.DefaultCurrency;
            
            if(invoiceInfo.IsReturnInvoice)
                obj.BillingReferences = await BillingReferenceAsync(invoiceInfo.InvoiceReturns);
            else
                obj.BillingReferences = null;

            obj.AdditionalDocumentReferences = await AdditionalDocumentReferenceAsync(invoiceInfo.ICV, invoiceInfo.PIH);
            obj.AccountingSupplierParty = await AccountSupplierAsync(invoiceInfo.Supplier);

            if (invoiceInfo.Customer != null)
                obj.AccountingCustomerParty = await AccountCustomerAsync(invoiceInfo.Customer);
            else
                obj.AccountingCustomerParty = null;

            obj.Delivery = await DeliveryAsync(invoiceInfo.Delivery);
            obj.PaymentMeans = await PaymentMeansAsync(invoiceInfo.PaymentMeans);
            obj.AllowanceCharges = await AllowanceChargeAsync(invoiceInfo.AllowanceCharges);
            obj.TaxTotal = await TaxTotalAsync(invoiceInfo.TaxTotal);
            obj.LegalMonetaryTotal = await LegalMonetaryTotalAsync(invoiceInfo.LegalMonetaryTotal);
            obj.InvoiceLines = await InvoiceLinesAsync(invoiceInfo.InvoiceLines);
            return obj;
        }


        public async Task<List<AdditionalDocumentReference>> AdditionalDocumentReferenceAsync(string icv, string pih)
        {
            var list = new List<AdditionalDocumentReference>();
            icv = string.IsNullOrWhiteSpace(icv) ? "1" : icv.Trim();
            pih = string.IsNullOrWhiteSpace(pih) ? "0".ToSha256HexBase64() : pih.Trim();

            var icvDoc = new AdditionalDocumentReference();
            icvDoc.ID = "ICV";
            icvDoc.UUID = icv;
            list.Add(icvDoc);

            var pihDoc = new AdditionalDocumentReference();
            pihDoc.ID = "PIH";
            pihDoc.UUID = null;
            pihDoc.Attachment = new Attachment();
            pihDoc.Attachment.EmbeddedDocumentBinaryObject = new EmbeddedDocumentBinaryObject();
            pihDoc.Attachment.EmbeddedDocumentBinaryObject.MimeCode = "text/plain";
            pihDoc.Attachment.EmbeddedDocumentBinaryObject.Value = pih;

            list.Add(pihDoc);

            return list;
        }


        public async Task<List<BillingReferenceModel>> BillingReferenceAsync(List<InvoiceReturnDto> invoiceReturns)
        {
            if (invoiceReturns == null) 
                return null;

            var list = new List<BillingReferenceModel>();    
            foreach (var item in invoiceReturns)
            {
                var obj = new BillingReferenceModel();
                obj.InvoiceDocumentReference = new InvoiceDocumentReferenceModel();
                //obj.InvoiceDocumentReference.ID = $"?Invoice Number: {item.Id}; Invoice Issue Date: {item.IssueDate.ToDateInvoice()}?";
                obj.InvoiceDocumentReference.ID = item.Id;
                if(item.IssueDate.HasValue)
                    obj.InvoiceDocumentReference.IssueDate = item.IssueDate.Value.ToDateInvoice();
                
                list.Add(obj);
            }

            return list;
        }


        public async Task<AccountCustomerOrSupplierPartyModel> AccountSupplierAsync(AccountCustomerOrSupplierDto account)
        {
            if (account == null)
                return null;

            var obj = new AccountCustomerOrSupplierPartyModel();
            obj.Party = new AccountPartyModel();

            obj.Party.PostalAddress = new AccountPostalAddressModel();
            obj.Party.PostalAddress.StreetName = account.StreetName;
            obj.Party.PostalAddress.BuildingNumber = account.BuildNo;
            obj.Party.PostalAddress.CitySubdivisionName = account.CitySubdivisionName;
            obj.Party.PostalAddress.CityName = account.CityName;
            obj.Party.PostalAddress.PostalZone = account.PostalZone;

            obj.Party.PostalAddress.Country = new AccountCountryModel();
            obj.Party.PostalAddress.Country.IdentificationCode = Transactions.DefaultCountryCode;

            if(account.CommercialType == CompanyCommercialType.CRN || account.CommercialType == CompanyCommercialType.CTH)
            {
                obj.Party.PartyTaxScheme = new AccountPartyTaxSchemeModel();
                obj.Party.PartyTaxScheme.CompanyID = account.CommercialNumber;

                obj.Party.PartyTaxScheme.TaxScheme = new TaxSchemeModel();
                obj.Party.PartyTaxScheme.TaxScheme.ID = Transactions.VAT;

                obj.Party.PartyLegalEntity = new AccountPartyLegalEntityModel();
                obj.Party.PartyLegalEntity.RegistrationName = account.TaxCompanyName;
            }

            obj.Party.PartyIdentification = new AccountPartyIdentificationModel();
            obj.Party.PartyIdentification.ID = account.CommercialNumber;
            obj.Party.PartyIdentification.SchemeID = account.CommercialType.GetEnumDescription();

            return obj;
        }


        public async Task<AccountCustomerOrSupplierPartyModel> AccountCustomerAsync(AccountCustomerOrSupplierDto account)
        {
            if (account == null)
                return null;

            var obj = new AccountCustomerOrSupplierPartyModel();
            obj.Party = new AccountPartyModel();

            obj.Party.PostalAddress = new AccountPostalAddressModel();
            obj.Party.PostalAddress.StreetName = account.StreetName;
            obj.Party.PostalAddress.BuildingNumber = account.BuildNo;
            obj.Party.PostalAddress.CitySubdivisionName = account.CitySubdivisionName;
            obj.Party.PostalAddress.CityName = account.CityName;
            obj.Party.PostalAddress.PostalZone = account.PostalZone;

            obj.Party.PostalAddress.Country = new AccountCountryModel();
            obj.Party.PostalAddress.Country.IdentificationCode = Transactions.DefaultCountryCode;

            if (account.CommercialType == CompanyCommercialType.CRN || account.CommercialType == CompanyCommercialType.CTH)
            {
                obj.Party.PartyTaxScheme = new AccountPartyTaxSchemeModel();
                obj.Party.PartyTaxScheme.CompanyID = account.TaxNumber;

                obj.Party.PartyTaxScheme.TaxScheme = new TaxSchemeModel();
                obj.Party.PartyTaxScheme.TaxScheme.ID = Transactions.VAT;

                obj.Party.PartyLegalEntity = new AccountPartyLegalEntityModel();
                obj.Party.PartyLegalEntity.RegistrationName = account.TaxCompanyName;
            }

            if (account.CommercialType == CompanyCommercialType.OTH)
            {
                obj.Party.PartyIdentification = new AccountPartyIdentificationModel();
                obj.Party.PartyIdentification.ID = account.CommercialNumber;
                obj.Party.PartyIdentification.SchemeID = account.CommercialType.GetEnumDescription();
            }

            else
                obj.Party.PartyIdentification = null;

            return obj;
        }


        public async Task<DeliveryModel> DeliveryAsync(DeliveryDto delivery)
        {
            if (delivery == null) 
                return null;
            
            var obj = new DeliveryModel();
            obj.ActualDeliveryDate = delivery.ActualDeliveryDate;
            
            if(!string.IsNullOrWhiteSpace(delivery.LatestDeliveryDate))
                obj.LatestDeliveryDate = delivery.LatestDeliveryDate;
            
            return obj;
        }

        public async Task<PaymentMeansModel?> PaymentMeansAsync(PaymentMeansDto payment)
        {
            if (payment == null)
                return null;

            var obj = new PaymentMeansModel();
            obj.PaymentMeansCode = ((int)payment.PaymentMeansCode).ToString();

            switch (payment.PaymentMeansCode)
            {
                case PaymentMeansCode.Card:
                    obj.InstructionNote = string.IsNullOrEmpty(payment.InstructionNote)
                        ? "Paid via card"
                        : payment.InstructionNote;
                    break;

                case PaymentMeansCode.CreditTransfer:
                case PaymentMeansCode.BankAccountPayment:
                    obj.PayeeFinancialAccount = new PayeeFinancialAccountModel
                    {
                        ID = payment.IBAN
                    };
                    obj.InstructionNote = string.IsNullOrEmpty(payment.InstructionNote)
                        ? "Please transfer to the bank account below"
                        : payment.InstructionNote;
                    break;

                case PaymentMeansCode.DebitTransfer:
                case PaymentMeansCode.DirectDebit:
                    obj.PayeeFinancialAccount = new PayeeFinancialAccountModel
                    {
                        ID = payment.IBAN
                    };
                    obj.InstructionNote = string.IsNullOrEmpty(payment.InstructionNote)
                        ? "Amount will be debited automatically"
                        : payment.InstructionNote;
                    break;

                case PaymentMeansCode.Cheque:
                    obj.InstructionNote = string.IsNullOrEmpty(payment.InstructionNote)
                        ? "Payment by cheque"
                        : payment.InstructionNote;
                    break;

                case PaymentMeansCode.Cash:
                    obj.InstructionNote = string.IsNullOrEmpty(payment.InstructionNote)
                        ? "Paid in cash"
                        : payment.InstructionNote;
                    break;

                default:
                    obj.InstructionNote = null;
                    obj.PayeeFinancialAccount = null;
                    break;
            }

            return obj;
        }

        public async Task<List<AllowanceChargeModel>> AllowanceChargeAsync(List<AllowanceChargeDto> allowances)
        {
            if (allowances == null) 
                return null;

            var list = new List<AllowanceChargeModel>();
            var vatList = VatItemDataDto.GetList();

            foreach (var allowance in allowances)
            {
                var obj = new AllowanceChargeModel();
                obj.ChargeIndicator = allowance.ChargeIndicator;

                if (allowances.Count > 1)
                {
                    if (allowance.ChargeIndicator && allowance.ChargeReasonCode == AllowanceChargeReasonCodeType.Discount)
                        obj.AllowanceChargeReasonCode = AllowanceChargeReasonCodeType.Other.GetEnumValueAsString();

                    else if (!allowance.ChargeIndicator && allowance.ChargeReasonCode != AllowanceChargeReasonCodeType.Discount)
                        obj.AllowanceChargeReasonCode = AllowanceChargeReasonCodeType.Discount.GetEnumValueAsString();

                    else
                        obj.AllowanceChargeReasonCode = allowance.ChargeReasonCode.GetEnumValueAsString();
                }
                else
                {
                    obj.AllowanceChargeReasonCode = null;
                }

                obj.Amount = new AmountModel();
                obj.Amount.CurrencyID = Transactions.DefaultCurrency;
                obj.Amount.Value = allowance.Amount;
                
                obj.TaxCategory = new TaxCategoryModel();
                obj.TaxCategory.ID = allowance.TaxCategoryId.GetEnumDescription();

                var percent = allowance.TaxCategoryId == VatCategoryCode.Standard ?
                                          Transactions.VatStandard : Transactions.VatZero;

                obj.TaxCategory.Percent = percent.ToTwoDecimal();
                obj.TaxCategory.TaxScheme = new TaxSchemeModel();
                obj.TaxCategory.TaxScheme.ID = Transactions.VAT;

                obj.TaxCategory.TaxExemptionReasonCode = null;
                obj.TaxCategory.TaxExemptionReason = null;

                list.Add(obj);
            }

            return list;
        }
        
        public async Task<TaxTotalModel> TaxTotalAsync(TaxTotalDto taxTotal)
        {
            if (taxTotal == null) 
                return null;

            var obj = new TaxTotalModel();
            obj.TaxAmount = new AmountModel();
            obj.TaxAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.TaxAmount.Value = taxTotal.TotalTaxAmount;
            obj.RoundingAmount = null;
            obj.TaxSubtotals = new List<TaxSubtotalModel>();
            
            foreach (var item in taxTotal.TaxSubtotals)
            {
                var sub = new TaxSubtotalModel();

                sub.TaxableAmount = new AmountModel();
                sub.TaxableAmount.CurrencyID = Transactions.DefaultCurrency;
                sub.TaxableAmount.Value = item.TotalAmount;

                sub.TaxAmount = new AmountModel();
                sub.TaxAmount.CurrencyID = Transactions.DefaultCurrency;
                sub.TaxAmount.Value = item.TaxAmount;

                sub.TaxCategory = new TaxCategoryModel();
                sub.TaxCategory.TaxScheme = new TaxSchemeModel();
                sub.TaxCategory.TaxScheme.ID = Transactions.VAT;

                sub.TaxCategory.ID = item.TaxCategoryId.GetEnumDescription();
               
                if (item.TaxCategoryId != VatCategoryCode.Standard)
                {
                    var vat = VatItemDataDto.GetList().FirstOrDefault(v => v.ExemptionReasonCode == item.TaxExemptionReasonCode);
                    if (vat != null)
                    {
                        sub.TaxCategory.TaxExemptionReasonCode = item.TaxExemptionReasonCode;
                        sub.TaxCategory.TaxExemptionReason = vat.Description;
                    }

                    sub.TaxCategory.Percent = Transactions.VatZero;
                }
                else
                {
                    sub.TaxCategory.Percent = Transactions.VatStandard; 
                    sub.TaxCategory.TaxExemptionReasonCode = null;
                    sub.TaxCategory.TaxExemptionReason = null;
                }
           
                obj.TaxSubtotals.Add(sub);
            }
           
            return obj;
        }

        public async Task<LegalMonetaryTotalModel> LegalMonetaryTotalAsync(LegalMonetaryTotalDto legalMonetaryTotal)
        {
            if (legalMonetaryTotal == null)
                return null;

            var obj = new LegalMonetaryTotalModel();
            obj.LineExtensionAmount = new AmountModel();
            obj.LineExtensionAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.LineExtensionAmount.Value = legalMonetaryTotal.LineExtensionAmount ?? 0m;

            obj.TaxExclusiveAmount = new AmountModel();
            obj.TaxExclusiveAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.TaxExclusiveAmount.Value = legalMonetaryTotal.TaxExclusiveAmount ?? 0m;

            obj.TaxInclusiveAmount = new AmountModel();
            obj.TaxInclusiveAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.TaxInclusiveAmount.Value = legalMonetaryTotal.TaxInclusiveAmount ?? 0m;


            if (legalMonetaryTotal.AllowanceTotalAmount.HasValue)
            {
                obj.AllowanceTotalAmount = new AmountModel();
                obj.AllowanceTotalAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.AllowanceTotalAmount.Value = legalMonetaryTotal.AllowanceTotalAmount ?? 0;
            }
            else
            {
                obj.AllowanceTotalAmount = null;
            }

            if (legalMonetaryTotal.ChargeTotalAmount.HasValue)
            {
                obj.ChargeTotalAmount = new AmountModel();
                obj.ChargeTotalAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.ChargeTotalAmount.Value = legalMonetaryTotal.ChargeTotalAmount ?? 0m;
            }
            else
            {
                obj.ChargeTotalAmount = null;
            }

           
            obj.PrepaidAmount = new AmountModel();
            obj.PrepaidAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.PrepaidAmount.Value = legalMonetaryTotal.PrepaidAmount ?? 0m;

            obj.PayableRoundingAmount = null;

            obj.PayableAmount = new AmountModel();
            obj.PayableAmount.CurrencyID = Transactions.DefaultCurrency;
            obj.PayableAmount.Value = legalMonetaryTotal.PayableAmount ?? 0m;

            return obj;
        }

        public async Task<List<InvoiceLineModel>> InvoiceLinesAsync(List<InvoiceLineDto> invoiceLines)
        {
            if (invoiceLines == null)
                return null;

            var list = new List<InvoiceLineModel>();
            
            int index = 0;
            foreach (var line in invoiceLines)
            {
                index++;
                var obj = new InvoiceLineModel();
                obj.ID = string.IsNullOrWhiteSpace(line.Id)? index.ToString() : line.Id;
                obj.InvoicedQuantity = new QuantityModel();
                obj.InvoicedQuantity.UnitCode = line.UnitCodeType.GetEnumDescription();
                obj.InvoicedQuantity.Value = line.Quantity;
                
                obj.LineExtensionAmount = new AmountModel();
                obj.LineExtensionAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.LineExtensionAmount.Value = line.TotalAmount;

                obj.LineExtensionAmountWithTax = null;
                //obj.LineExtensionAmountWithTax = new AmountModel();
                //obj.LineExtensionAmountWithTax.CurrencyID = Transactions.DefaultCurrency;
                //obj.LineExtensionAmountWithTax.Value = line.TotalAmountWithTax??0m;

                obj.TaxTotal = new TaxTotalModel();
                obj.TaxTotal.TaxAmount = new AmountModel();
                obj.TaxTotal.TaxAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.TaxTotal.TaxAmount.Value = line.TaxAmount;

                obj.TaxTotal.RoundingAmount = new AmountModel();
                obj.TaxTotal.RoundingAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.TaxTotal.RoundingAmount.Value = line.RoundingAmount.ToTwoDecimal();

                //obj.TaxTotal.TaxSubtotals = new List<TaxSubtotalModel>();
                //if((line.DiscountAmount??0)>0)
                //{
                //    obj.AllowanceCharge = new AllowanceChargeModel();
                //    obj.AllowanceCharge.ChargeIndicator = false;
                //    obj.AllowanceCharge.AllowanceChargeReason = line.DiscountReason?? "Discount";
                //    obj.AllowanceCharge.MultiplierFactorNumeric = line.DiscountPercentage??0m;

                //    obj.AllowanceCharge.Amount = new AmountModel();
                //    obj.AllowanceCharge.Amount.CurrencyID = Transactions.DefaultCurrency;
                //    obj.AllowanceCharge.Amount.Value = line.PriceAmount;

                //    obj.AllowanceCharge.BaseAmount = new AmountModel();
                //    obj.AllowanceCharge.BaseAmount.CurrencyID = Transactions.DefaultCurrency;
                //    obj.AllowanceCharge.BaseAmount.Value = line.Quantity * line.PriceAmount;

                //    obj.AllowanceCharge.TaxCategory = new TaxCategoryModel();
                //    obj.AllowanceCharge.TaxCategory.ID = line.TaxCategoryId.GetEnumDescription();
                //    obj.AllowanceCharge.TaxCategory.Percent = line.TaxCategoryId == VatCategoryCode.Standard ?
                //                                              Transactions.VatStandard : Transactions.VatZero;

                //    obj.AllowanceCharge.TaxCategory.TaxScheme = new TaxSchemeModel();
                //    obj.AllowanceCharge.TaxCategory.TaxScheme.ID = "VAT";
                //    obj
                //}



                //obj.TaxTotal.RoundingAmount = new AmountModel();
                //obj.TaxTotal.RoundingAmount.CurrencyID = Transactions.DefaultCurrency;
                //obj.TaxTotal.RoundingAmount.Value = 0m; //line.RoundingAmount

                obj.Item = new ItemModel();
                obj.Item.Name = line.ItemName;
                obj.Item.ClassifiedTaxCategory = new TaxCategoryModel();
                obj.Item.ClassifiedTaxCategory.ID = line.TaxCategoryId.GetEnumDescription();
                obj.Item.ClassifiedTaxCategory.Percent = line.TaxCategoryId == VatCategoryCode.Standard?
                                                         Transactions.VatStandard : Transactions.VatZero;

                obj.Item.ClassifiedTaxCategory.Percent = obj.Item.ClassifiedTaxCategory.Percent.ToTwoDecimal();
                obj.Item.ClassifiedTaxCategory.TaxScheme = new TaxSchemeModel();
                obj.Item.ClassifiedTaxCategory.TaxScheme.ID = Transactions.VAT;
                
                obj.Price = new PriceModel();
                obj.Price.PriceAmount = new AmountModel();
                obj.Price.PriceAmount.CurrencyID = Transactions.DefaultCurrency;
                obj.Price.PriceAmount.Value = line.PriceAmount;

                list.Add(obj);
            }

            return list;
        }

    }
}
