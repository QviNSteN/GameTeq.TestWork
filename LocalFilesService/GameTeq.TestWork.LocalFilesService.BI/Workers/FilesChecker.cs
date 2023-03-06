using GameTeq.TestWork.General.FileService.Engines;
using GameTeq.TestWork.LocalFilesService.BI.Options;
using GameTrq.TestWork.General.Redis.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.LocalFilesService.BI.Workers
{
    public class FilesChecker : BackgroundService
    {
        private readonly DiskService _disk;
        private readonly IRedisNewFiles _redis;

        private readonly CheckerConfig _config;

        public FilesChecker(DiskService disk, IRedisNewFiles redis, CheckerConfig config)
        {
            _disk = disk;
            _redis = redis;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                var files = await _disk.GetNewFiles();
                if(files.Any())
                {
                    await _redis.AddNewFilesInfo(files);
                }

                if (!_config.Period.IsWorkforever)
                    await Task.Delay(_config.Period.PeriodWorkSeconds * 1000);
            }
        }
    }
}


