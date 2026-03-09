using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.IService;

namespace ZatcaIntegrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceConnectorController : ControllerBase
    {
        private readonly IDeviceConnectService _service;
        public DeviceConnectorController(IDeviceConnectService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConnectDeviceRequestModel model)
        {
            return Ok(await _service.ConnectDeviceAsync(model));
        }
    }
}
