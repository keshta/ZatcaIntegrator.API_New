using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;

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
        public async Task<IActionResult> ClearanceAsync([FromBody] InvoiceSingleRequestModel model)
        {
            return Ok(await _service.ClearanceAsync(model));

        }

        [HttpPost("Reporting")]
        public async Task<IActionResult> ReportingAsync([FromBody] InvoiceSingleRequestModel model)
        {
            return Ok(await _service.ReportingAsync(model));
        }

    }
}
