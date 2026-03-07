using System;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Data
{
    public static class AccountData
    {
        public static CsrAndCsidRequestDto CsrData()
        {
            var csrRequest = new CsrAndCsidRequestDto
            {
                //SerialNumber = "ABC Trading Co.".ToSerialNo(),          // توليد الرقم التسلسلي من الاسم
                // الاسم الرسمي للشركة
                CommercialName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",

                // الاسم القانوني/المسجل، عادة مطابق للاسم الرسمي
                TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",

                // رقم المعرف (VAT) من XML
                // رقم VAT/CRN 15 رقم، أول وآخر رقم = 3
                TaxNumber = "399999999900003",

                // الرقم التسلسلي يولد تلقائيًا من الاسم
                SerialNumber = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD".ToSerialNo(),

                // اسم الوحدة/القسم المسؤول داخل الشركة
                TaxUnitName = "IT Department",

                // رمز الدولة حسب ISO Alpha-2
                CountryName = "SA",

                // نوع الفواتير (B2B, B2C)
                InvoiceType = Transactions.B2BAndB2C,

                // العنوان الكامل للشركة من XML
                LocationAddress = "الامير سلطان | Prince Sultan, المربع | Al-Murabba, الرياض | Riyadh, Saudi Arabia",

                // قطاع الشركة أو نوع النشاط
                IndustryBusinessCategory = "Technology Supply"
            };

            return csrRequest;
        }

    }
}