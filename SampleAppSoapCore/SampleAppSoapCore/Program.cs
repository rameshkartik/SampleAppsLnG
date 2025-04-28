using Services;
using SoapCore;
using static SoapCore.DocumentationWriter.SoapDefinition;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSoapCore();

//builder.Services.AddWebApi();

builder.Services.AddScoped<IEndPointService, EndPointService>();

builder.Services.AddSoapCore();

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");

app.UseEndpoints(endpoints =>
{
    var endpointES = endpoints.UseSoapEndpoint<IEndPointService>("/EndPointServiceNew.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
    var endpointGS = endpoints.UseSoapEndpoint<IGroupService>("/GroupService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

app.Run();
