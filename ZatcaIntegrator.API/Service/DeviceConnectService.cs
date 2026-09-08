using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Model;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegrator.API.Service
{
    public class DeviceConnectService : IDeviceConnectService
    {
        private readonly IDeviceConnectorService _service;
        private readonly IEnvironmentService _environmentService;
        public DeviceConnectService(IDeviceConnectorService service,
                                    IEnvironmentService environmentService)
        {
            _service = service;
            _environmentService = environmentService;
        }

        public async Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestModel model)
        {
            var env = await _environmentService.GetCurrentEnvironmentAsync();
            var device = new ConnectDeviceRequestDto();
            
            device.CsrRequest = CsrRequestMap(model);
            device.OTP = model.OTP;
            
            var supplier = SupplierMap(model);
            var deviceResult = await _service.ConnectDeviceAsync(device, supplier, env);
            return deviceResult;
        }


        private CsrAndCsidRequestDto CsrRequestMap(ConnectDeviceRequestModel model)
        {
            var obj = new CsrAndCsidRequestDto();
            obj.CommercialName = model.CommercialName;
            obj.SerialNumber = model.CommercialName.ToSerialNo();//model.CommercialNumber.ToSerialNo();
            obj.TaxNumber = model.TaxNumber;
            obj.TaxUnitName = model.TaxUnitName;
            obj.TaxCompanyName = model.TaxCompanyName;
            obj.InvoiceType = model.InvoiceType;
            obj.LocationAddress = model.LocationAddress;
            obj.IndustryBusinessCategory = model.IndustryBusinessCategory;
            obj.CountryName = Transactions.DefaultCountryCode;
            return obj;
        }

        private AccountCustomerOrSupplierDto SupplierMap(ConnectDeviceRequestModel model)
        {
            var obj = new AccountCustomerOrSupplierDto();
            obj.CommercialType = model.CommercialType;
            obj.CommercialNumber = model.CommercialNumber;
            obj.TaxNumber = model.TaxNumber;
            obj.TaxCompanyName = model.TaxCompanyName;
            obj.StreetName = model.StreetName;
            obj.BuildNo = model.BuildNo;
            obj.CitySubdivisionName = model.CitySubdivisionName;
            obj.CityName = model.CityName;
            obj.CountryCode = model.CountryCode??Transactions.DefaultCountryCode;
            obj.PostalZone = model.PostalZone;
            return obj;
        }

    }
}
