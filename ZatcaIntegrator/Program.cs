using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZatcaIntegrator;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Service;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.XmlInvoice;

IDeviceConnectorService deviceConnectorService = new DeviceConnectorService();
IXmlInvoiceStandard xmlInvoiceStandard = new XmlInvoiceStandard();


string base64Token = "TUlJRDNqQ0NBNFNnQXdJQkFnSVRFUUFBT0FQRjkwQWpzL3hjWHdBQkFBQTRBekFLQmdncWhrak9QUVFEQWpCaU1SVXdFd1lLQ1pJbWlaUHlMR1FCR1JZRmJHOWpZV3d4RXpBUkJnb0praWFKay9Jc1pBRVpGZ05uYjNZeEZ6QVZCZ29Ka2lhSmsvSXNaQUVaRmdkbGVIUm5ZWHAwTVJzd0dRWURWUVFERXhKUVVscEZTVTVXVDBsRFJWTkRRVFF0UTBFd0hoY05NalF3TVRFeE1Ea3hPVE13V2hjTk1qa3dNVEE1TURreE9UTXdXakIxTVFzd0NRWURWUVFHRXdKVFFURW1NQ1FHQTFVRUNoTWRUV0Y0YVcxMWJTQlRjR1ZsWkNCVVpXTm9JRk4xY0hCc2VTQk1WRVF4RmpBVUJnTlZCQXNURFZKcGVXRmthQ0JDY21GdVkyZ3hKakFrQmdOVkJBTVRIVlJUVkMwNE9EWTBNekV4TkRVdE16azVPVGs1T1RrNU9UQXdNREF6TUZZd0VBWUhLb1pJemowQ0FRWUZLNEVFQUFvRFFnQUVvV0NLYTBTYTlGSUVyVE92MHVBa0MxVklLWHhVOW5QcHgydmxmNHloTWVqeThjMDJYSmJsRHE3dFB5ZG84bXEwYWhPTW1Obzhnd25pN1h0MUtUOVVlS09DQWdjd2dnSURNSUd0QmdOVkhSRUVnYVV3Z2FLa2daOHdnWnd4T3pBNUJnTlZCQVFNTWpFdFZGTlVmREl0VkZOVWZETXRaV1F5TW1ZeFpEZ3RaVFpoTWkweE1URTRMVGxpTlRndFpEbGhPR1l4TVdVME5EVm1NUjh3SFFZS0NaSW1pWlB5TEdRQkFRd1BNems1T1RrNU9UazVPVEF3TURBek1RMHdDd1lEVlFRTURBUXhNVEF3TVJFd0R3WURWUVFhREFoU1VsSkVNamt5T1RFYU1CZ0dBMVVFRHd3UlUzVndjR3g1SUdGamRHbDJhWFJwWlhNd0hRWURWUjBPQkJZRUZFWCtZdm1tdG5Zb0RmOUJHYktvN29jVEtZSzFNQjhHQTFVZEl3UVlNQmFBRkp2S3FxTHRtcXdza0lGelZ2cFAyUHhUKzlObk1Ic0dDQ3NHQVFVRkJ3RUJCRzh3YlRCckJnZ3JCZ0VGQlFjd0FvWmZhSFIwY0RvdkwyRnBZVFF1ZW1GMFkyRXVaMjkyTG5OaEwwTmxjblJGYm5KdmJHd3ZVRkphUlVsdWRtOXBZMlZUUTBFMExtVjRkR2RoZW5RdVoyOTJMbXh2WTJGc1gxQlNXa1ZKVGxaUFNVTkZVME5CTkMxRFFTZ3hLUzVqY25Rd0RnWURWUjBQQVFIL0JBUURBZ2VBTUR3R0NTc0dBUVFCZ2pjVkJ3UXZNQzBHSlNzR0FRUUJnamNWQ0lHR3FCMkUwUHNTaHUyZEpJZk8reG5Ud0ZWbWgvcWxaWVhaaEQ0Q0FXUUNBUkl3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdNR0NDc0dBUVVGQndNQ01DY0dDU3NHQVFRQmdqY1ZDZ1FhTUJnd0NnWUlLd1lCQlFVSEF3TXdDZ1lJS3dZQkJRVUhBd0l3Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUxFL2ljaG1uV1hDVUtVYmNhM3ljaThvcXdhTHZGZEhWalFydmVJOXVxQWJBaUE5aEM0TThqZ01CQURQU3ptZDJ1aVBKQTZnS1IzTEUwM1U3NWVxYkMvclhBPT0=";

byte[] bytes = Convert.FromBase64String(base64Token);
string tokenValue = Encoding.UTF8.GetString(bytes);


byte[] certBytes = Convert.FromBase64String(tokenValue);
X509Certificate2 cert = new X509Certificate2(certBytes);

Console.WriteLine("Subject: " + cert.Subject);
Console.WriteLine("Issuer: " + cert.Issuer);
Console.WriteLine("Valid From: " + cert.NotBefore);
Console.WriteLine("Valid To: " + cert.NotAfter);
Console.WriteLine("Thumbprint: " + cert.Thumbprint);

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

var supplier = new AccountCustomerOrSupplierDto
{
    // نوع المعرف: CRN
    CommercialType = CompanyCommercialType.CRN,
    CommercialNumber = "1010010000",

    // رقم ضريبة القيمة المضافة
    TaxNumber = "399999999900003",

    // الاسم القانوني للشركة
    TaxCompanyName = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD",

    // العنوان
    StreetName = "الامير سلطان | Prince Sultan",
    BuildNo = "2322",
    CitySubdivisionName = "المربع | Al-Murabba",
    CityName = "الرياض | Riyadh",
    PostalZone = "23333"
};

var device = new ConnectDeviceRequestDto { CsrRequest = csrRequest, OTP = "12345" };
var deviceResult = await deviceConnectorService.ConnectDeviceAsync(device, supplier); //InvoicesData()

Guid invoiceGuid = Guid.NewGuid();


var api = new ComplianceAPIService();

//var exampleInvoice2 = GetInvoice3(invoiceGuid);

var compliance = new ComplianceResponseDto();
compliance.RequestID = deviceResult.Data.RequestID;
compliance.Secret = deviceResult.Data.Secret;
compliance.BinarySecurityToken = deviceResult.Data.BinarySecurityToken;
compliance.DispositionMessage = deviceResult.Data.DispositionMessage;

var xmlTxt = await xmlInvoiceStandard.GenerateXmlInvoiceAsync(GetInvoice3(invoiceGuid));
//var cre = await api.SendComplianceDocumnet(compliance, deviceResult.Data.PrivateKey, xmlTxt.ToXmlDocument(), invoiceGuid);

var invoice = new InvoiceSingleRequestDto();
invoice.InvoiceData = GetInvoice3(invoiceGuid);
invoice.Compliance = compliance;
invoice.Uuid = invoiceGuid.ToString();
invoice.PrivateKey = deviceResult.Data.PrivateKey;
invoice.SingleType = InvoiceSingleType.Standard;
invoice.DocumentType = InvoiceDocumentType.Invoice;

IInvoiceSingleService _invoiceSingleService = new InvoiceSingleService();
var report = await _invoiceSingleService.ReportingAsync(invoice);


Console.WriteLine("Hello, World!");


InvoiceDto GetInvoice3(Guid invoiceGuid)
{
    var exampleInvoice = new InvoiceDto
    {
        ID = "1002",
        UUID = invoiceGuid,
        IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
        IssueTime = DateTime.UtcNow.ToString("HH:mm:ss"),
        ICV = "2",
        PIH = "0".ToSha256HexBase64(),

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
            TaxNumber = "399999999800003",
            TaxCompanyName = "شركة نماذج فاتورة المحدودة | Fatoora Samples LTD",
            StreetName = "صلاح الدين | Salah Al-Din",
            BuildNo = "1111",
            CitySubdivisionName = "المروج | Al-Murooj",
            CityName = "الرياض | Riyadh",
            PostalZone = "12222"
        },

        Delivery = new DeliveryDto
        {
            ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice(),
            LatestDeliveryDate = DateTime.UtcNow.AddDays(5).ToDateInvoice()
        },

        PaymentMeans = new PaymentMeansDto
        {
            PaymentMeansCode = PaymentMeansCode.CreditTransfer,
            InstructionNote = "Transfer to bank account",
            IBAN = "SA4420000001234567891234"
        },

        // خصم الفاتورة موزع على أنواع الضريبة
        AllowanceCharges = new List<AllowanceChargeDto>
        {
            new AllowanceChargeDto
            {
                ChargeIndicator = false,
                ChargeReason = "Invoice Discount",
                ChargeReasonCode = AllowanceChargeReasonCodeType.Discount,
                Amount = 24.18m, // Standard portion
                TaxCategoryId = VatCategoryCode.Standard
            },
            new AllowanceChargeDto
            {
                ChargeIndicator = false,
                ChargeReason = "Invoice Discount",
                ChargeReasonCode = AllowanceChargeReasonCodeType.Discount,
                Amount = 35.82m, // Zero Rated portion
                TaxCategoryId = VatCategoryCode.ZeroRated
            }
        },
        InvoiceLines = new List<InvoiceLineDto>
        {
           new InvoiceLineDto
           {
               Id = "1",
               ItemName = "Item A",
               Quantity = 2,
               UnitCodeType = UnitCodeType.PCE,
               PriceAmount = 30m,
               TotalAmount = 60m,
               TaxAmount = 9m,    // 60 × 15% (S)
               TotalAmountWithTax = 69m, // 60 + 9
               RoundingAmount = 69 ,
               TaxCategoryId = VatCategoryCode.Standard
           },
           new InvoiceLineDto
           {
               Id = "2",
               ItemName = "Item B",
               Quantity = 3,
               UnitCodeType = UnitCodeType.PCE,
               PriceAmount = 25m,
               TotalAmount = 75m,
               TaxAmount = 11.25m, // 75 × 15% (S)
               TotalAmountWithTax = 86.25m, // 75 + 11.25
               RoundingAmount = 86.25m,
               TaxCategoryId = VatCategoryCode.Standard
           },
           new InvoiceLineDto
           {
               Id = "3",
               ItemName = "Item C",
               Quantity = 4,
               UnitCodeType = UnitCodeType.PCE,
               PriceAmount = 50m,
               TotalAmount = 200m, // 4*50
               TaxAmount = 0m,
               TotalAmountWithTax = 200m,
               RoundingAmount = 200m,
               TaxCategoryId = VatCategoryCode.ZeroRated
           }
        },
        TaxTotal = new TaxTotalDto
        {
            TotalTaxAmount = 16.62m,
            TaxSubtotals = new List<TaxSubtotalDto>
            {
              new TaxSubtotalDto
              {
                  TotalAmount = 110.82m, // Standard 135 - 24.18
                  TaxAmount = 16.62m,
                  TaxCategoryId = VatCategoryCode.Standard
              },
              new TaxSubtotalDto
              {
                  TotalAmount = 164.18m, // Zero Rated 200 - 35.82
                  TaxAmount = 0m,
                  TaxCategoryId = VatCategoryCode.ZeroRated,
                  TaxExemptionReason = "Zero Rated",
                  TaxExemptionReasonCode = "VATEX-SA-32"
              }
            }
        },

        LegalMonetaryTotal = new LegalMonetaryTotalDto
        {
            LineExtensionAmount = 335m,       // 60 + 75 + 200   مجموع الأصناف
            TaxExclusiveAmount = 275m,        // بعد خصم 60
            TaxInclusiveAmount = 291.62m,     // TaxExclusiveAmount + ضريبة S
            AllowanceTotalAmount = 60m,
            ChargeTotalAmount = 0m,
            PrepaidAmount = 0m,
            PayableAmount = 291.62m
        },

        ActualDeliveryDate = DateTime.UtcNow.ToDateInvoice()
    };

    //exampleInvoice.RecalculateTaxes();
    return exampleInvoice;
}


Dictionary<InvoiceTypeData, InvoiceDto> InvoicesData()
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


