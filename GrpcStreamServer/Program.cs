using GrpcStreamServer.Services;

using Serilog;
using Serilog.Core;

// Serilog #1
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//var serilog = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(config)
               .CreateLogger();


var levelSwitch = new LoggingLevelSwitch();

// Serilog #2
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    // Seq 에 로그 정보 입력
//    .WriteTo.Seq("http://localhost:5341", controlLevelSwitch: levelSwitch)
//    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Serilog #1
//builder.WebHost.ConfigureLogging(logging => { logging.AddSerilog(serilog); });

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(option =>
{
    option.MaxSendMessageSize = 1000 * 1024 * 1024;
    option.MaxReceiveMessageSize = 1000 * 1024 * 1024;
});


var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<WeatherService>();
});

// Configure the HTTP request pipeline.
//app.MapGrpcService<WeatherService>();

app.Run();
