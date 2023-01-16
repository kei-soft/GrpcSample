using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Serilog;

using static GrpcStreamServer.WeatherForecasts;

namespace GrpcStreamServer.Services
{
    public class WeatherService : WeatherForecastsBase
    {
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(ILogger<WeatherService> logger) => _logger = logger;

        public override async Task GetWeatherStream(Empty _, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
        {
            var rng = new Random();
            var now = DateTime.UtcNow;

            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested && i < 20)
            {
                await Task.Delay(500); // Gotta look busy

                Log.Information($"{i} - Serilog Server Test");

                var forecast = new WeatherData
                {
                    DateTimeStamp = Timestamp.FromDateTime(now.AddDays(i++)),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = ""//Summaries[rng.Next(Summaries.Length)]
                };

                _logger.LogInformation("Sending WeatherData response");

                responseStream.WriteOptions = new WriteOptions(WriteFlags.BufferHint);

                await responseStream.WriteAsync(forecast);
            }
        }
    }
}