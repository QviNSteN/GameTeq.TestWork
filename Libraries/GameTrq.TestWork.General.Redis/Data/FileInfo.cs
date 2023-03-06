using System;
using System.ComponentModel.DataAnnotations;
using GameTrq.TestWork.General.Redis.Resources;
using Redis.OM.Modeling;

namespace GameTrq.TestWork.General.Redis.Data
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { NamingConstans.NameTableFile })]
    public class FileInfoRedis
    {
        [RedisIdField]
        [Indexed]
        public string Filename { get; set; }

        [Indexed]
        public bool InWork { get; set; } = false;

        [Indexed]
        public DateTime? StartWorkTime { get; set; }
    }
}
