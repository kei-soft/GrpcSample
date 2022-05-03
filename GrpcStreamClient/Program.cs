using Google.Protobuf.WellKnownTypes;

using Grpc.Core;
using Grpc.Net.Client;

using GrpcStreamClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5038");

var client = new WeatherForecasts.WeatherForecastsClient(channel);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
using var streamingCall = client.GetWeatherStream(new Empty(), cancellationToken: cts.Token);

try
{
    await foreach (var weatherData in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
    {
        Console.WriteLine($"{weatherData.DateTimeStamp.ToDateTime():s} | {weatherData.Summary} | {weatherData.TemperatureC} C");
    }
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
{
    Console.WriteLine("Stream cancelled.");
}