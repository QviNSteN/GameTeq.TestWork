using GameTrq.TestWork.General.Redis.Interfaces;
using GameTrq.TestWork.General.Redis.Options;
using Redis.OM;
using Redis.OM.Searching;
using GameTrq.TestWork.General.Redis.Data;
using GameTrq.TestWork.General.Redis.Resources;

namespace GameTrq.TestWork.General.Redis.Engines
{
    public class FileRedisService : IRedisNewFiles, IRedisFileToWork
    {
        private readonly RedisConnectionProvider _redis;
        private readonly RedisCollection<FileInfoRedis> _files;
        private readonly FileInfoConfig _config;

        public FileRedisService(RedisConnectionProvider redis, FileInfoConfig config)
        {
            _config = config;
            _redis = redis;
            _files = (RedisCollection<FileInfoRedis>)redis.RedisCollection<FileInfoRedis>();
        }

        public async Task AddNewFileInfo(FileInfoRedis file)
        {
            if (file is null)
                return;

            await _files.InsertAsync(file, TimeSpan.FromSeconds(_config.LifeTimeSecondsKey));
        }

        public async Task AddNewFilesInfo(IEnumerable<FileInfoRedis> files)
        {
            if(files is null)
                return;

            foreach(var file in files)
                await AddNewFileInfo(file);
        }

        public async Task RemoveFileInfo(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return;

            await _redis.Connection.UnlinkAsync($"{NamingConstans.NameTableFile}:{fileName}");
        }

        public async Task<FileInfoRedis> TakeToWorkFile()
        {
            var file = await _files.Where(x => x.InWork).FirstOrDefaultAsync();

            if(file is null)
                return null;

            FileInWork(file);

            return file;
        }

        public async Task<IEnumerable<FileInfoRedis>> TakeToWorkAllFiles()
        {
            var files = _files.Where(x => x.InWork);

            foreach (var file in files)
            {
                FileInWork(file);
            }

            await _files.SaveAsync();

            return await files.ToListAsync();
        }

        private void FileInWork(FileInfoRedis? file)
        {
            if (file is null)
                return;

            file.InWork = true;
            file.StartWorkTime = DateTime.Now;
        }
    }
}