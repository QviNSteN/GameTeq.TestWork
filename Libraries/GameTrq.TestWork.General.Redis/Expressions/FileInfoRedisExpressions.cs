using GameTrq.TestWork.General.Redis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrq.TestWork.General.Redis.Expressions
{
    public static class FileInfoRedisExpressions
    {
        public static IEnumerable<FileInfoRedis> ToFilter(this IEnumerable<FileInfoRedis> list) => list.Where(x => x.Filename.EndsWith(".json")).AsEnumerable();
    }
}
