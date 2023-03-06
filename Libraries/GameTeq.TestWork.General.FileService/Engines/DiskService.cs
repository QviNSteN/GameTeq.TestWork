using GameTeq.TestWork.General.FileService.Data;
using GameTeq.TestWork.General.FileService.Intefaces;
using GameTeq.TestWork.General.FileService.Options;
using GameTrq.TestWork.General.Redis.Data;
using GameTrq.TestWork.General.Redis.Expressions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace GameTeq.TestWork.General.FileService.Engines
{
    public class DiskService : IDiskReader, IDiskWriter
    {
        private readonly FilesConfig _config;

        public DiskService(FilesConfig config)
        {
            _config = config;
        }

        public async Task<List<string>> AddFiles(IEnumerable<NewJsonFile> files)
        {
            var result = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    await new FileJson(_config, $"{DateTime.Now.Date:d}", file.Type).SaveFile(file.Json);

                    result.Add(file.FileName);
                }
                catch
                {
                    continue;
                }
            }

            return result;
        }

        public async Task<IEnumerable<FileInfoRedis>> GetNewFiles()
        {
            var files = GetFilesInDirectory(_config.MainDirectory);

            return files.Select(x => new FileInfoRedis()
            {
                Filename = x,
                InWork = false,
                StartWorkTime = null
            }).ToFilter()
            .AsEnumerable();
        }

        public IEnumerable<IFileJson> GetFilesJson(IEnumerable<string> files)
        {
            foreach(var file in files)
            {
                yield return new FileJson(_config, file);
            }
        }

        public IEnumerable<IFileJson> GetFilesJson(string file)
        {
            yield return new FileJson(_config, file);
        }

        private IEnumerable<string> GetFilesInDirectory(string directory)
        {
            if (Directory.Exists(directory))
                return Directory.GetFiles(directory).Select(x => x.Split(Path.PathSeparator).Last());

            return null;
        }
    }
}
