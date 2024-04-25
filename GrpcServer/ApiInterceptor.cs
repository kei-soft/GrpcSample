using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcServer
{
    public class ApiInterceptor : Interceptor
    {
        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            // 옵션 설정 및 요청을 처리하기 전 로직
            context.WriteOptions = new WriteOptions(WriteFlags.BufferHint);

            var response = base.ServerStreamingServerHandler(request, responseStream, context, continuation);

            // 요청이 성공적으로 처리된 후 로직

            return response;
        }
    }
}
