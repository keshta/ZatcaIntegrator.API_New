using System;
using System.ComponentModel;

namespace ZatcaIntegratorV2.Shared
{
    public enum CompanyCommercialType
    {
        [Description(Transactions.CRN)]
        CRN,  // Commercial Registration Number

        [Description(Transactions.CTH)]
        CTH,   // Contractor Tax Header (or Tax Number)
        
        [Description(Transactions.OTH)]
        OTH   // Other / foreign or unregistered customer (no VAT)
    }

    public enum PaymentMeansCode
    {
        // 1 Instrument not defined (Free text)
        Cash = 10,                 // دفع نقدي
        Cheque = 20,               // شيك
        CreditTransfer = 30,       // تحويل بنكي (العميل يحوّل)
        DebitTransfer = 31,        // خصم بنكي
        BankAccountPayment = 42,   // بيع آجل / دفع لاحق
        Card = 48,                 // بطاقة (مدى / فيزا / ماستر)
        DirectDebit = 49           // خصم مباشر بتفويض
    }

    public enum VatCategoryCode
    {
        // Standard rate
        [Description("S")]
        Standard,

        // Zero-rated goods
        [Description("Z")]
        ZeroRated,

        // Exempt from tax
        [Description("E")]
        Exempt,

        // Services outside the scope of VAT or non-taxable supplies
        [Description("O")]
        OutsideScope
    }

    public enum InvoiceType
    {
        Invoice = 388,      // Standard Tax Invoice
        CreditNote = 381,   // Credit Note (Decrease invoice amount)
        DebitNote = 383     // Debit Note (Increase invoice amount)
    }


    public enum UnitCodeType
    {
        [Description("EA")]
        EA,   // Each - قطعة واحدة

        [Description("PCE")]
        PCE,  // Piece - قطعة

        [Description("KG")]
        KG,   // Kilogram - كيلوجرام

        [Description("G")]
        G,    // Gram - جرام

        [Description("TNE")]
        TNE,  // Tonne - طن

        [Description("LTR")]
        LTR,  // Liter - لتر

        [Description("MLT")]
        MLT,  // Milliliter - مليلتر

        [Description("MTR")]
        MTR,  // Meter - متر

        [Description("CMT")]
        CMT,  // Centimeter - سنتيمتر

        [Description("MMT")]
        MMT,  // Millimeter - مليمتر

        [Description("MTK")]
        MTK,  // Square Meter - متر مربع

        [Description("MTQ")]
        MTQ,  // Cubic Meter - متر مكعب

        [Description("BOX")]
        BOX,  // Box - صندوق

        [Description("PKG")]
        PKG,  // Package - عبوة

        [Description("CTN")]
        CTN,  // Carton - كرتونة

        [Description("PAL")]
        PAL,  // Pallet - منصة تحميل

        [Description("HUR")]
        HUR,  // Hour - ساعة

        [Description("DAY")]
        DAY,  // Day - يوم

        [Description("WEE")]
        WEE,  // Week - أسبوع

        [Description("MON")]
        MON,  // Month - شهر

        [Description("ANN")]
        ANN,  // Year - سنة

        [Description("E51")]
        E51   // Service Unit - وحدة خدمة
    }

    public enum AllowanceChargeReasonCodeType
    {
        [Description("01")]
        Discount = 1,            // خصم على الفاتورة | Invoice Discount

        [Description("02")]
        Shipping = 2,            // رسوم الشحن | Shipping Fee

        [Description("03")]
        Other = 3,               // رسوم خدمات أخرى | Other Charges

        [Description("04")]
        Correction = 4,         // رسوم إعادة الفاتورة | Correction Fee

        [Description("05")]
        Warranty = 5           // رسوم الضمان أو التأمين | Warranty / Insurance Fee
    }

    public enum ZatcaEnvironmentType
    {
        Production,
        Simulation,
        NonProduction
    }

    public enum InvoiceTypeData
    {
        SimplifiedCredit,
        SimplifiedDebit,
        SimplifiedInvoice,
        StandardCredit,
        StandardDebit,
        StandardInvoice,
    }

}
