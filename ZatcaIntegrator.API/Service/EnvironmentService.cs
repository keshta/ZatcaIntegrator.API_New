using ZatcaIntegrator.API.IService;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Service
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EnvironmentService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ZatcaEnvironmentType> GetCurrentEnvironmentAsync()
        {
            var type = _httpContextAccessor.HttpContext?.Request.Headers["Environment-Type"].ToString();
            var obj = ZatcaEnvironmentType.NonProduction;
            obj = !string.IsNullOrWhiteSpace(type) ? (ZatcaEnvironmentType)int.Parse(type) : obj;
            return obj;
        }

    }
}
