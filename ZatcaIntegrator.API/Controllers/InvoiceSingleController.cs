using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.IService;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceSingleController : ControllerBase
    {
        private readonly ISingleInvoiceService _service;
        public InvoiceSingleController(ISingleInvoiceService service)
        {
            _service = service;
        }

        [ProducesResponseType(typeof(InvoiceSingleClearanceResultDto), 200)]
        [HttpPost("Clearance")]
        public async Task<IActionResult> ClearanceAsync([FromBody] InvoiceSingleRequestDto model)
        {
            return Ok(await _service.ClearanceAsync(model));

        }

        [ProducesResponseType(typeof(InvoiceSingleReportingResultDto), 200)]
        [HttpPost("Reporting")]
        public async Task<IActionResult> ReportingAsync([FromBody] InvoiceSingleRequestDto model)
        {
            return Ok(await _service.ReportingAsync(model));
        }

        [ProducesResponseType(typeof(InvoiceSingleQrCodeResultDto), 200)]
        [HttpPost("GetQrCode")]
        public async Task<IActionResult> GetQrCode([FromBody] InvoiceSingleRequestDto model)
        {
            return Ok(await _service.GetQrCodeAsync(model));
        }


    }
}
