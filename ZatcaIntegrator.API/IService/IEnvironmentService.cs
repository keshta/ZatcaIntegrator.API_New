using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.IService
{
    public interface IEnvironmentService
    {
        Task<ZatcaEnvironmentType> GetCurrentEnvironmentAsync();
    }
}
