using GameTeq.TestWork.General.FileService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.WebInputFileService.BI.Interfaces
{
    public interface IFiles
    {
        Task<string[]> SaveFiles(IEnumerable<NewJsonFile> files);
    }
}
