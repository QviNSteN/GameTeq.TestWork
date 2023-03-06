using GameTeq.TestWork.General.FileService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Intefaces
{
    public interface IDiskWriter
    {
        Task<List<string>> AddFiles(IEnumerable<NewJsonFile> files);
    }
}
