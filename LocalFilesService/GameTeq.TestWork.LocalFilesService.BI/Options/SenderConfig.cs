using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.LocalFilesService.BI.Options
{
    public class SenderConfig
    {
        public bool IsOneInstanse { get; set; }
        public WorkTimer Period { get; set; } = new WorkTimer();
    }
}
