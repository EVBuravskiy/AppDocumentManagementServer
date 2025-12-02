using AppDocumentManagement.ExternalDocumentService;
using Grpc.Core;

namespace AppDocumentManagement.ExternalDocumentService.Services
{
    public class ExternalDocumentAPI : Greeter.GreeterBase
    {
        private readonly ILogger<ExternalDocumentAPI> _logger;
        public ExternalDocumentAPI(ILogger<ExternalDocumentAPI> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
