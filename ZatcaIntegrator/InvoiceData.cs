using System;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator
{
    public static class InvoiceData
    {

        public static Dictionary<InvoiceTypeData, InvoiceDto> GetInvoicesDataList()
        {
            var list = new Dictionary<InvoiceTypeData, InvoiceDto>();
            list.Add(InvoiceTypeData.SimplifiedCredit, InvoiceData.SimplifiedCredit());
            list.Add(InvoiceTypeData.SimplifiedDebit, InvoiceData.SimplifiedDebit());
            list.Add(InvoiceTypeData.SimplifiedInvoice, InvoiceData.SimplifiedInvoice());
            list.Add(InvoiceTypeData.StandardCredit, InvoiceData.StandardCredit());
            list.Add(InvoiceTypeData.StandardDebit, InvoiceData.StandardDebit());
            list.Add(InvoiceTypeData.StandardInvoice, InvoiceData.StandardInvoice());
            return list;
        }


        public static InvoiceDto SimplifiedCredit()
        {
            var invoice = new InvoiceDto
            {
                ID = "00015",
                UUID = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                IssueTime = DateTime.UtcNow.ToString("HH:mm:ss"),
                ICV = "15",
                PIH = "0".ToSha256HexBase64(),
                IsReturnInvoice = true,
                InvoiceReturns = new List<InvoiceReturnDto> 
                {
                    new InvoiceReturnDto
                    { 
                        Id = "00002",
                        //IssueDate = DateTime.UtcNow.AddDays(-1)
                    }
                },

                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = null,

                Delivery = new DeliveryDto
                {
                    ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice(),
                    //LatestDeliveryDate = DateTime.UtcNow.AddDays(5).ToDateInvoice()
                },

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash,
                    InstructionNote = "In case of goods or services refund | عند ترجيع السلع أو الخدمات",
                    IBAN = null
                },

                AllowanceCharges = new List<AllowanceChargeDto>
                {
                    new AllowanceChargeDto
                    {
                        ChargeIndicator = false,
                        ChargeReason = "discount",
                        ChargeReasonCode = AllowanceChargeReasonCodeType.Discount,
                        Amount = 0.00m,
                        TaxCategoryId = VatCategoryCode.Standard
                    }
                },

                InvoiceLines = new List<InvoiceLineDto>
                {
                    new InvoiceLineDto
                    {
                        Id = "1",
                        ItemName = "Pencil | قلم",
                        Quantity = 2,
                        UnitCodeType = UnitCodeType.PCE,
                        PriceAmount = 2.00m,
                        TotalAmount = 4.00m,
                        TaxAmount = 0.60m,
                        TotalAmountWithTax = 4.60m,
                        RoundingAmount = 4.60m,
                        TaxCategoryId = VatCategoryCode.Standard
                    }
                },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 0.60m,
                    TaxSubtotals = new List<TaxSubtotalDto>
                    {
                        new TaxSubtotalDto
                        {
                            TotalAmount = 4.00m,
                            TaxAmount = 0.60m,
                            TaxCategoryId = VatCategoryCode.Standard
                        }
                    }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 4.00m,
                    TaxExclusiveAmount = 4.00m,
                    TaxInclusiveAmount = 4.60m,
                    AllowanceTotalAmount = 0.00m,
                    ChargeTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 4.60m
                },

                ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice()
            };

            return invoice;
        }

        public static InvoiceDto SimplifiedDebit()
        {
            var invoiceDto = new InvoiceDto
            {
                ID = "00016",
                UUID = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                IssueTime = DateTime.UtcNow.ToString("HH:mm:ss"),
                ICV = "16",
                PIH = "5fceb66ffc86f38d952786c6d696c79c2dbc239dd4e91b46729d73a27fb57e9",
                InvoiceReturns = new List<InvoiceReturnDto>
                {
                    new InvoiceReturnDto
                    {
                        Id = "00002",
                        //IssueDate = DateTime.UtcNow.AddDays(-2)
                    }
                },

                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = null,

                Delivery = new DeliveryDto
                {
                    ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice(),
                    LatestDeliveryDate = DateTime.UtcNow.AddDays(5).ToDateInvoice()
                },

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash,
                    InstructionNote = "Amendment of the supply value which is pre-agreed upon between the supplier and consumer|تم الاتفاق على تعديل قيمة التوريد مسبقاً"
                },

                AllowanceCharges = new List<AllowanceChargeDto>
                {
                    new AllowanceChargeDto
                    {
                        ChargeIndicator = false,
                        ChargeReason = "discount",
                        ChargeReasonCode = AllowanceChargeReasonCodeType.Discount,
                        Amount = 0.00m,
                        TaxCategoryId = VatCategoryCode.Standard
                    }
                },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 3.60m,
                    TaxSubtotals = new List<TaxSubtotalDto>
                    {
                        new TaxSubtotalDto
                        {
                            TotalAmount = 24.00m,
                            TaxAmount = 3.60m,
                            TaxCategoryId = VatCategoryCode.Standard
                        }
                    }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 24.00m,
                    TaxExclusiveAmount = 24.00m,
                    TaxInclusiveAmount = 27.60m,
                    AllowanceTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 27.60m
                },

                InvoiceLines = new List<InvoiceLineDto>
                {
                    new InvoiceLineDto
                    {
                        Id = "1",
                        ItemName = "Pencil | قلم",
                        Quantity = 12,
                        UnitCodeType = UnitCodeType.PCE,
                        PriceAmount = 2.00m,
                        TotalAmount = 24.00m,
                        TaxAmount = 3.60m,
                        TotalAmountWithTax = 27.60m,
                        RoundingAmount = 27.60m,
                        TaxCategoryId = VatCategoryCode.Standard
                    }
                },
                ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice()
            };

            return invoiceDto;
        }

        public static InvoiceDto SimplifiedInvoice()
        {
            var invoice = new InvoiceDto
            {
                ID = "00010",
                UUID = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                IssueTime = DateTime.UtcNow.ToString("HH:mm:ss"),
                ICV = "10",
                PIH = "5fceb66ffc86f38d952786c6d952786c6d969c2dbc239dd4e91b46729d73a27fb57e9", // decoded base64 from XML

                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = null, // Empty in XML

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash,
                    InstructionNote = null
                },

                AllowanceCharges = new List<AllowanceChargeDto>
    {
        new AllowanceChargeDto
        {
            ChargeIndicator = false,
            ChargeReason = "discount",
            Amount = 0.00m,
            TaxCategoryId = VatCategoryCode.Standard
        }
    },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 30.15m,
                    TaxSubtotals = new List<TaxSubtotalDto>
        {
            new TaxSubtotalDto
            {
                TotalAmount = 201.00m,
                TaxAmount = 30.15m,
                TaxCategoryId = VatCategoryCode.Standard,
            }
        }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 201.00m,
                    TaxExclusiveAmount = 201.00m,
                    TaxInclusiveAmount = 231.15m,
                    AllowanceTotalAmount = 0.00m,
                    ChargeTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 231.15m
                },

                InvoiceLines = new List<InvoiceLineDto>
    {
        new InvoiceLineDto
        {
            Id = "1",
            ItemName = "كتاب",
            Quantity = 33,
            UnitCodeType = UnitCodeType.PCE,
            PriceAmount = 3.00m,
            TotalAmount = 99.00m,
            TaxAmount = 14.85m,
            TotalAmountWithTax = 113.85m,
            RoundingAmount = 113.85m,
            TaxCategoryId = VatCategoryCode.Standard,
        },
        new InvoiceLineDto
        {
            Id = "2",
            ItemName = "قلم",
            Quantity = 3,
            UnitCodeType = UnitCodeType.PCE,
            PriceAmount = 34.00m,
            TotalAmount = 102.00m,
            TaxAmount = 15.30m,
            TotalAmountWithTax = 117.30m,
            RoundingAmount = 117.30m,
            TaxCategoryId = VatCategoryCode.Standard,
        }
    },
                ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice()

            };
            return invoice;
        }

        public static InvoiceDto StandardCredit()
        {
            var invoice = new InvoiceDto
            {
                ID = "00015",
                UUID = Guid.NewGuid(),
                IssueDate = "2026-02-28",
                IssueTime = "03:18:38",
                ICV = "15",
                PIH = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==",
                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "399999999800003",
                    TaxCompanyName = "شركة نماذج فاتورة المحدودة | Fatoora Samples LTD",
                    StreetName = "صلاح الدين | Salah Al-Din",
                    BuildNo = "1111",
                    CitySubdivisionName = "المروج | Al-Murooj",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "12222"
                },

                Delivery = new DeliveryDto
                {
                    ActualDeliveryDate = DateTime.Parse("2022-09-05").ToDateInvoice()
                },

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash,
                    InstructionNote = "In case of goods or services refund | عند ترجيع السلع أو الخدمات"
                },

                AllowanceCharges = new List<AllowanceChargeDto>
                {
                    new AllowanceChargeDto
                    {
                        ChargeIndicator = false,
                        ChargeReason = "discount",
                        Amount = 0.00m,
                        TaxCategoryId = VatCategoryCode.Standard
                    }
                },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 0.60m,
                    TaxSubtotals = new List<TaxSubtotalDto>
                    {
                        new TaxSubtotalDto
                        {
                            TotalAmount = 4.00m,
                            TaxAmount = 0.60m,
                            TaxCategoryId = VatCategoryCode.Standard,
                        }
                    }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 4.00m,
                    TaxExclusiveAmount = 4.00m,
                    TaxInclusiveAmount = 4.60m,
                    AllowanceTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 4.60m
                },

                InvoiceLines = new List<InvoiceLineDto>
                {
                    new InvoiceLineDto
                    {
                        Id = "1",
                        ItemName = "Pencil | قلم",
                        Quantity = 2,
                        UnitCodeType = UnitCodeType.PCE,
                        PriceAmount = 2.00m,
                        TotalAmount = 4.00m,
                        TaxAmount = 0.60m,
                        TotalAmountWithTax = 4.60m,
                        RoundingAmount = 4.60m,
                        TaxCategoryId = VatCategoryCode.Standard,
                    }
                },

                InvoiceReturns = new List<InvoiceReturnDto>
                {
                    new InvoiceReturnDto
                    {
                        Id = "00002",
                        //IssueDate = DateTime.UtcNow.AddDays(-3)
                    }
                },

            };
            
            return invoice;
        }

        public static InvoiceDto StandardDebit()
        {
            var invoice = new InvoiceDto
            {
                ID = "00016",
                UUID = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                IssueTime = DateTime.UtcNow.ToString("HH:mm:ss"),
                ICV = "16",
                PIH = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==",
                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "399999999800003",
                    TaxCompanyName = "شركة نماذج فاتورة المحدودة | Fatoora Samples LTD",
                    StreetName = "صلاح الدين | Salah Al-Din",
                    BuildNo = "1111",
                    CitySubdivisionName = "المروج | Al-Murooj",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "12222"
                },

                Delivery = new DeliveryDto
                {
                    ActualDeliveryDate = DateTime.Parse("2022-09-05").ToDateInvoice()
                },

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash,
                    InstructionNote = "Amendment of the supply value which is pre-agreed upon between the supplier and consumer | تم الاتفاق على تعديل قيمة التوريد مسبقاً"
                },

                AllowanceCharges = new List<AllowanceChargeDto>
    {
        new AllowanceChargeDto
        {
            ChargeIndicator = false,
            ChargeReason = "discount",
            Amount = 0.00m,
            TaxCategoryId = VatCategoryCode.Standard
        }
    },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 3.60m,
                    TaxSubtotals = new List<TaxSubtotalDto>
        {
            new TaxSubtotalDto
            {
                TotalAmount = 24.00m,
                TaxAmount = 3.60m,
                TaxCategoryId = VatCategoryCode.Standard,
            }
        }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 24.00m,
                    TaxExclusiveAmount = 24.00m,
                    TaxInclusiveAmount = 27.60m,
                    AllowanceTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 27.60m
                },

                InvoiceLines = new List<InvoiceLineDto>
    {
        new InvoiceLineDto
        {
            Id = "1",
            ItemName = "Pen| قلم",
            Quantity = 12,
            UnitCodeType = UnitCodeType.PCE,
            PriceAmount = 2.00m,
            TotalAmount = 24.00m,
            TaxAmount = 3.60m,
            TotalAmountWithTax = 27.60m,
            RoundingAmount = 27.60m,
            TaxCategoryId = VatCategoryCode.Standard,
        }
    },

                InvoiceReturns = new List<InvoiceReturnDto>
    {
        new InvoiceReturnDto
        {
            Id = "00002"
        }
    },

            };

            return invoice;
        }

        public static InvoiceDto StandardInvoice()
        {
            var invoice = new InvoiceDto
            {
                ID = "00023",
                UUID = Guid.NewGuid(),
                IssueDate = "2026-02-28",
                IssueTime = "03:27:37",
                ICV = "23",
                PIH = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==",
                Supplier = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "1010010000",
                    TaxNumber = "399999999900003",
                    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",
                    StreetName = "الامير سلطان | Prince Sultan",
                    BuildNo = "2322",
                    CitySubdivisionName = "المربع | Al-Murabba",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "23333"
                },

                Customer = new AccountCustomerOrSupplierDto
                {
                    CommercialType = CompanyCommercialType.CRN,
                    CommercialNumber = "399999999800003",
                    TaxCompanyName = "شركة نماذج فاتورة المحدودة | Fatoora Samples LTD",
                    StreetName = "صلاح الدين | Salah Al-Din",
                    BuildNo = "1111",
                    CitySubdivisionName = "المروج | Al-Murooj",
                    CityName = "الرياض | Riyadh",
                    PostalZone = "12222"
                },

                Delivery = new DeliveryDto
                {
                    ActualDeliveryDate = DateTime.Parse("2022-09-07").ToDateInvoice()
                },

                PaymentMeans = new PaymentMeansDto
                {
                    PaymentMeansCode = PaymentMeansCode.Cash
                },

                AllowanceCharges = new List<AllowanceChargeDto>
    {
        new AllowanceChargeDto
        {
            ChargeIndicator = false,
            ChargeReason = "discount",
            Amount = 0.00m,
            TaxCategoryId = VatCategoryCode.Standard
        }
    },

                TaxTotal = new TaxTotalDto
                {
                    TotalTaxAmount = 0.60m,
                    TaxSubtotals = new List<TaxSubtotalDto>
        {
            new TaxSubtotalDto
            {
                TotalAmount = 4.00m,
                TaxAmount = 0.60m,
                TaxCategoryId = VatCategoryCode.Standard,
            }
        }
                },

                LegalMonetaryTotal = new LegalMonetaryTotalDto
                {
                    LineExtensionAmount = 4.00m,
                    TaxExclusiveAmount = 4.00m,
                    TaxInclusiveAmount = 4.60m,
                    AllowanceTotalAmount = 0.00m,
                    PrepaidAmount = 0.00m,
                    PayableAmount = 4.60m
                },

                InvoiceLines = new List<InvoiceLineDto>
    {
        new InvoiceLineDto
        {
            Id = "1",
            ItemName = "قلم رصاص",
            Quantity = 2,
            UnitCodeType = UnitCodeType.PCE,
            PriceAmount = 2.00m,
            TotalAmount = 4.00m,
            TaxAmount = 0.60m,
            TotalAmountWithTax = 4.60m,
            RoundingAmount = 4.60m,
            TaxCategoryId = VatCategoryCode.Standard,
        }
    },
            };

            return invoice;

        }
    }
}