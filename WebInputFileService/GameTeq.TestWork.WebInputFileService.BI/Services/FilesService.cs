using GameTeq.TestWork.General.FileService.Data;
using GameTeq.TestWork.General.FileService.Intefaces;
using GameTeq.TestWork.General.FileService.Resources;
using GameTeq.TestWork.WebInputFileService.BI.Interfaces;
using GameTrq.TestWork.General.Redis.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GameTeq.TestWork.OutputLocalFilesService.BI.Services
{
    public class FilesService : IFiles
    {
        private readonly IDiskWriter _disk;

        public FilesService(IDiskWriter disk)
        {
            _disk = disk;
        }

        public async Task<string[]> SaveFiles(IEnumerable<NewJsonFile> files)
        {
            var result = await _disk.AddFiles(files.Select(x => new NewJsonFile()
            {
                FileName= x.FileName,
                Json= x.Json,
                Type = x.Json.Deserialize<JsonObject>()[JsonElementsConstans.Type].GetValue<string>()
            }));

            return result.ToArray();
        }
    }
}
