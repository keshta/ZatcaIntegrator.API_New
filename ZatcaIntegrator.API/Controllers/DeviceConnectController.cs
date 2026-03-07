using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.Data;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Service;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceConnectController : ControllerBase
    {
        IDeviceConnectorService deviceConnectorService = new DeviceConnectorService();

        [HttpGet("Connect")]
        public async Task<IActionResult> Connect()
        {
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

            var device = new ConnectDeviceRequestDto { CsrRequest = AccountData.CsrData(), OTP = "12345" };
            var deviceResult = await deviceConnectorService.ConnectDeviceAsync(device, supplier);
            return Ok(deviceResult);
        }
    }
}
