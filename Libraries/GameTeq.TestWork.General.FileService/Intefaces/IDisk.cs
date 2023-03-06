using GameTrq.TestWork.General.Redis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Intefaces
{
    public interface IDiskReader
    {
        Task<IEnumerable<FileInfoRedis>> GetNewFiles();

        IEnumerable<IFileJson> GetFilesJson(IEnumerable<string> files);

        IEnumerable<IFileJson> GetFilesJson(string file);
    }
}
