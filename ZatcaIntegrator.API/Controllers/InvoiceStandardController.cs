using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceStandardController : ControllerBase
    {
        private readonly IInvoiceStandardService _service;

        public InvoiceStandardController(IInvoiceStandardService service)
        {
            _service = service;
        }


        [HttpPost("GenerateXmlInvoice")]
        public async Task<IActionResult> GenerateXmlInvoiceAsync([FromBody] InvoiceDto model)
        {
            return Ok(await _service.GenerateXmlInvoiceAsync(model));
        }

        [HttpPost("GenerateXmlCredit")]
        public async Task<IActionResult> GenerateXmlCreditAsync([FromBody] InvoiceDto model)
        {
            return Ok(await _service.GenerateXmlCreditAsync(model));
        }

        [HttpPost("GenerateXmlDebit")]
        public async Task<IActionResult> GenerateXmlDebitAsync([FromBody] InvoiceDto model)
        {
            return Ok(await _service.GenerateXmlDebitAsync(model));
        }
    }
}
