using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
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

        [HttpPost("Clearance")]
        public async Task<IActionResult> ClearanceAsync([FromBody] InvoiceSingleRequestDto model)
        {
            return Ok(await _service.ClearanceAsync(model));

        }

        [HttpPost("Reporting")]
        public async Task<IActionResult> ReportingAsync([FromBody] InvoiceSingleRequestDto model)
        {
            return Ok(await _service.ReportingAsync(model));
        }

    }
}
