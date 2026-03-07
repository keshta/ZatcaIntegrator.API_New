using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZatcaIntegratorV2.Shared;
using ZatcaIntegratorV2.Dto;

namespace ZatcaIntegratorV2.IService
{
    public interface IDeviceConnectorService
    {
        //Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestDto request, Dictionary<InvoiceTypeData, InvoiceDto> invoices,ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
        Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestDto request, AccountCustomerOrSupplierDto supplier,ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction);
    }
}
