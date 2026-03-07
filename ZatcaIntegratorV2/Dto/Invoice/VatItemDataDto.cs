using System;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Dto
{
    public static class VatItemDataDto
    {
        public static List<VatItemDto> GetList()
        {
            var list = new List<VatItemDto>
            {
                new VatItemDto
                {
                    Description = "الخدمات المالية الواردة في المادة 29 من لائحة ضريبة القيمة المضافة",
                    VatCategory = "E",
                    ExemptionReasonCode = "VATEX-SA-29"
                },
                new VatItemDto
                {
                    Description = "خدمات التأمين على الحياة المذكورة في المادة 29 من لائحة ضريبة القيمة المضافة",
                    VatCategory = "E",
                    ExemptionReasonCode = "VATEX-SA-29-7"
                },
                new VatItemDto
                {
                    Description = "المعاملات العقارية المذكورة في المادة 30 من لائحة ضريبة القيمة المضافة",
                    VatCategory = "E",
                    ExemptionReasonCode = "VATEX-SA-30"
                },
                new VatItemDto
                {
                    Description = "تصدير السلع",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-32"
                },
                new VatItemDto
                {
                    Description = "تصدير الخدمات",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-33"
                },
                new VatItemDto
                {
                    Description = "النقل الدولي للبضائع",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-34-1"
                },
                new VatItemDto
                {
                    Description = "النقل الدولي للركاب",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-34-2"
                },
                new VatItemDto
                {
                    Description = "الخدمات المرتبطة مباشرة والعرضية لتوريد نقل الركاب الدولي",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-34-3"
                },
                new VatItemDto
                {
                    Description = "توريد وسيلة النقل المؤهلة",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-34-4"
                },
                new VatItemDto
                {
                    Description = "أي خدمات تتعلق بالبضائع أو نقل الركاب، على النحو المحدد في المادة الخامسة والعشرين من هذه اللائحة",
                    VatCategory = "Z",
                    ExemptionReasonCode = "VATEX-SA-34-5"
                }
             };
            
             for (int i = 0; i < list.Count; i++)
             {
                 list[i].Id = i + 1;
             }

            return list;
        }
    }

}
