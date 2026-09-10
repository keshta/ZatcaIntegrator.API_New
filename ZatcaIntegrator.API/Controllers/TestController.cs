using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZatcaIntegrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("runtime")]
        public object GetRuntime()
        {
            return new
            {
                Framework = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
                Runtime = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier,
                OS = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                Process = Environment.ProcessPath,
                Version = Environment.Version.ToString()
            };
        }

        [HttpGet("environment")]
        public object GetEnvironment()
        {
            return new
            {
                CurrentDirectory = Environment.CurrentDirectory,
                BaseDirectory = AppContext.BaseDirectory,
                ContentRoot = Directory.GetCurrentDirectory(),
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };
        }
    }
}
