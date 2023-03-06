using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.LocalFilesService.BI.Options
{
    public class WorkTimer
    {
        public bool IsWorkforever { get; set; } = true;

        public int PeriodWorkSeconds { get; set; } = 1;
    }
}
