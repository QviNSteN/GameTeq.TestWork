using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Nodes;
using GameTeq.TestWork.General.FileService.Intefaces;

namespace GameTeq.TestWork.General.FileService.Data
{
    public class JsonLineObject
    {
        /// <summary>
        /// We consider, according to the terms of reference, that type exists. In any case, it was previously checked for its presence.
        /// </summary>
        public string Type { get; set; }
        public JsonObject Object { get; set; }
    }
}
