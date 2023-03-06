using GameTeq.TestWork.OutputLocalFilesService;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GameTeq.TestWork.OutputLocalFilesService.Services
{
    public class GetFilesService : GetFiles.GetFilesBase
    {
        private readonly ILogger<GetFilesService> _logger;
        public GetFilesService(ILogger<GetFilesService> logger)
        {
            _logger = logger;
        }

        public override async Task<FilesReply> Get(FilterRequest request, ServerCallContext context)
        {
            return new FilesReply
            {
                
            };
        }
    }
}