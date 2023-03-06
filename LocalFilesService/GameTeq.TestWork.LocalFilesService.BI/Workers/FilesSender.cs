using GameTeq.TestWork.General.FileService.Engines;
using GameTeq.TestWork.LocalFilesService.BI.Options;
using GameTrq.TestWork.General.Redis.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.LocalFilesService.BI.Workers
{
    public class FilesSender : BackgroundService
    {
        private ConcurrentBag<Task<string[]>> Jsons = new ConcurrentBag<Task<string[]>>();

        private readonly IRedisFileToWork _redis;
        private readonly DiskService _disk;
        private readonly FilesTransfer.FilesTransferClient _grpc;

        private readonly SenderConfig _config;

        public FilesSender(IRedisFileToWork redis, DiskService disk, SenderConfig config, FilesTransfer.FilesTransferClient grpc)
        {
            _redis = redis;
            _disk = disk;
            _grpc = grpc;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                var filesJson = _config.IsOneInstanse
                    ? _disk.GetFilesJson((await _redis.TakeToWorkAllFiles()).Select(x => x.Filename))
                    : _disk.GetFilesJson((await _redis.TakeToWorkFile()).Filename);

                var result = new ReceiveFilesRequest();

                foreach(var file in filesJson)
                {
                    Jsons.Add(file.GetAllLines());

                    if(Jsons.TryPeek(out var task))
                    {
                        result.Files.AddRange(await task.ConfigureAwait(false));
                    }
                };

                var response = await _grpc.ReceiveFilesAsync(result);

                foreach (var file in filesJson.Where(x => response.FileNames.Contains(x.GetName())))
                {
                    try
                    {
                        file.Delete();

                        await _redis.RemoveFileInfo(file.GetName());
                    }
                    catch
                    {
                        //Надо придумать что нить тут.
                    }
                }

                if (!_config.Period.IsWorkforever)
                    await Task.Delay(_config.Period.PeriodWorkSeconds * 1000);
            }
        }
    }
}