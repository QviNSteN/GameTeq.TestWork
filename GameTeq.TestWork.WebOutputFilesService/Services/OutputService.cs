using GameTeq.TestWork.WebOutputFilesService;
using Grpc.Core;

namespace GameTeq.TestWork.WebOutputFilesService.Services
{
    public class OutputService : FilesOutput.FilesOutputBase
    {
        private readonly ILogger<OutputService> _logger;
        public OutputService(ILogger<OutputService> logger)
        {
            _logger = logger;
        }

        public override async Task<FilesReply> Get(FilterRequest request, ServerCallContext context)
        {
            return new FilesReply
            {
            };
        }

        public override async Task<FilesReply> GetAll(HelloRequest request, ServerCallContext context)
        {
            return new FilesReply
            {
            };
        }
    }
}