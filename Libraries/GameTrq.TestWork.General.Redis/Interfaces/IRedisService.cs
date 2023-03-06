using GameTrq.TestWork.General.Redis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrq.TestWork.General.Redis.Interfaces
{
    public interface IRedisFileToWork
    {
        Task<FileInfoRedis> TakeToWorkFile();

        Task<IEnumerable<FileInfoRedis>> TakeToWorkAllFiles();

        Task RemoveFileInfo(string fileName);
    }
    
    public interface IRedisNewFiles
    {
        Task AddNewFileInfo(FileInfoRedis file);

        Task AddNewFilesInfo(IEnumerable<FileInfoRedis> files);
    }
}
