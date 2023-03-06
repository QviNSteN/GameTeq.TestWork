using GameTeq.TestWork.General.FileService.Data;
using GameTeq.TestWork.WebInputFileService;
using GameTeq.TestWork.WebInputFileService.BI.Interfaces;
using Grpc.Core;

namespace GameTeq.TestWork.WebInputFileService.Services
{
    public class InputService : FilesTransfer.FilesTransferBase
    {
        private readonly ILogger<InputService> _logger;
        private readonly IFiles _files;
        public InputService(ILogger<InputService> logger, IFiles files)
        {
            _logger = logger;
            _files = files;
        }

        public override async Task<ReceiveFilesReply> ReceiveFiles(ReceiveFilesRequest request, ServerCallContext context)
        {
            var savedFiles = await _files.SaveFiles(request.Files.Select(x => new NewJsonFile()
            {
                FileName = x.FileName,
                Json = x.Json
            }));

            var result = new ReceiveFilesReply();

            result.FileNames.AddRange(savedFiles);

            return result;
        }
    }
}