using Grpc.Net.Client;

using GrpcClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5253", new GrpcChannelOptions { HttpHandler = new HttpClientHandler() });
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();