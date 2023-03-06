using GameTeq.TestWork.General.FileService.Options;
using GameTeq.TestWork.LocalFilesService.BI.Options;
using GameTrq.TestWork.General.Redis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.LocalFilesService.Options
{
    public class Config
    {
        public FilesConfig Files { get; set; }

        public FileInfoConfig FileInfoRedis { get; set; }

        public CheckerConfig Checker { get; set; }

        public SenderConfig Sender { get; set; }
    }
}
