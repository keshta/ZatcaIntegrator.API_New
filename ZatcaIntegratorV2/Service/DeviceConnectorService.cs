using System;
using ZATCA.EInvoice.SDK;
using ZATCA.EInvoice.SDK.Contracts;
using ZatcaIntegratorV2.Dto;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Shared;

namespace ZatcaIntegratorV2.Service
{
    public class DeviceConnectorService : IDeviceConnectorService
    {
        ICsrGenerator _generator;
        ICsrGeneratorService _csrGeneratorService;
        IComplianceAPIService _complianceAPIService;

        public DeviceConnectorService()
        {
            _generator = new CsrGenerator();
            _csrGeneratorService = new CsrGeneratorService(_generator);
            _complianceAPIService = new ComplianceAPIService();
        }

        //public async Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestDto request, Dictionary<InvoiceTypeData, InvoiceDto> invoices, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        public async Task<ConnectDeviceResultDto> ConnectDeviceAsync(ConnectDeviceRequestDto request, AccountCustomerOrSupplierDto supplier, ZatcaEnvironmentType environment = ZatcaEnvironmentType.NonProduction)
        {
            if (string.IsNullOrWhiteSpace(request.CsrRequest.SerialNumber))
                request.CsrRequest.SerialNumber = request.CsrRequest.CommercialName.ToSerialNo();

            var envCsr = environment == ZatcaEnvironmentType.NonProduction ? ZatcaEnvironmentType.Simulation : environment;

            var result = new ConnectDeviceResultDto();
            var generateResult = await _csrGeneratorService.GenerateAsync(request.CsrRequest, envCsr);
            if (!generateResult.IsSuccess)
            {
                result = new ConnectDeviceResultDto
                {
                    IsSuccess = false,
                    Errors = generateResult.Errors
                };
                return result;
            }


            var complianceResult = await _complianceAPIService.GetComplianceCSIDAsync(generateResult.Data.CSR, otp: request.OTP, environment);

            if (!complianceResult.IsSuccess)
            {
                result = new ConnectDeviceResultDto
                {
                    IsSuccess = false,
                    Errors = complianceResult.Errors,
                    ResponseJson = complianceResult.ResponseJson
                };

                return result;
            }

            var signAllResult = await _complianceAPIService.SignAllDocumentAsync(complianceResult.Response, generateResult.Data.PrivateKey, supplier, environment);
            if (!signAllResult.IsSuccess)
            {
                result = new ConnectDeviceResultDto
                {
                    IsSuccess = false,
                    Errors = signAllResult.Errors,
                    ResponseJson = signAllResult.ResponseJson
                };
                return result;
            }

            if (complianceResult.Response.DispositionMessage == Transactions.Issued)
            {
                var stamp = await _complianceAPIService.GetStampCSIDAsync(complianceResult.Response, environment);
                
                if (!stamp.IsSuccess)
                {
                    result = new ConnectDeviceResultDto
                    {
                        IsSuccess = false,
                        Errors = stamp.Errors,
                        ResponseJson = stamp.ResponseJson
                    };
                    return result;
                }


                //var data = new ConnectDeviceResultDataDto
                //{
                //    Id = generateResult.Data.Id,
                //    CSR = generateResult.Data.CSR,
                //    PrivateKey = generateResult.Data.PrivateKey,
                //    BinarySecurityToken = complianceResult.Response.BinarySecurityToken,
                //    DispositionMessage = complianceResult.Response.DispositionMessage,
                //    RequestID = complianceResult.Response.RequestID,
                //    Secret = complianceResult.Response.Secret,
                //    SerialNumber = request.CsrRequest.SerialNumber,
                //};


                var data = new ConnectDeviceResultDataDto
                {
                    Id = generateResult.Data.Id,
                    CSR = generateResult.Data.CSR,
                    PrivateKey = generateResult.Data.PrivateKey,
                    BinarySecurityToken = stamp.Response.BinarySecurityToken,
                    DispositionMessage = stamp.Response.DispositionMessage,
                    RequestID = stamp.Response.RequestID,
                    Secret = stamp.Response.Secret,
                    SerialNumber = request.CsrRequest.SerialNumber,
                };

                result = new ConnectDeviceResultDto
                {
                    Data = data,
                    ResponseJson = stamp.ResponseJson,
                    IsSuccess = true,
                };
            }
            else
            {
                result = new ConnectDeviceResultDto
                {
                    IsSuccess = false,
                    Errors = new List<ComplianceErrorDto>
                    {
                        new ComplianceErrorDto
                        {
                            Message = complianceResult.Response.DispositionMessage
                        }
                    }
                };
            }

            return result;
        }
    }
}
