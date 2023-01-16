using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcServer
{
    public class ApiInterceptor : Interceptor
    {
        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            context.WriteOptions = new WriteOptions(WriteFlags.BufferHint);
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }
    }
}
