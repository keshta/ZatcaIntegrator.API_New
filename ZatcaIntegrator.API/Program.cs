using ZatcaIntegrator.API.IService;
using ZatcaIntegrator.API.Service;
using ZatcaIntegratorV2.IService;
using ZatcaIntegratorV2.Service;
using ZatcaIntegratorV2.XmlInvoice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ZatcaIntegratorV2 services
builder.Services.AddTransient<IDeviceConnectorService, DeviceConnectorService>();
builder.Services.AddTransient<IInvoiceSingleService, InvoiceSingleService>();
builder.Services.AddTransient<IXmlInvoiceStandard, XmlInvoiceStandard>();
builder.Services.AddTransient<IComplianceAPIService, ComplianceAPIService>();

// ZatcaIntegrationAPI services
builder.Services.AddTransient<IInvoiceStandardService, InvoiceStandardService>();
builder.Services.AddTransient<ISingleInvoiceService, SingleInvoiceService>();
builder.Services.AddTransient<IEnvironmentService, EnvironmentService>();
builder.Services.AddTransient<IDeviceConnectService, DeviceConnectService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
