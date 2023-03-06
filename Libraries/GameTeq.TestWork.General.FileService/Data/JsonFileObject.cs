using GameTeq.TestWork.General.FileService.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Data
{
    public class JsonFileObject
    {
        public IEnumerable<JsonLineObject> Lines { get; set; }

        public IFileJson File {get; set;}
    }
}
