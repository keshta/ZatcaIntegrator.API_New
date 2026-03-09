using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegrator.API.IService
{
    public interface IDeviceConnectService
    {
        Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestModel model);
    }
}
