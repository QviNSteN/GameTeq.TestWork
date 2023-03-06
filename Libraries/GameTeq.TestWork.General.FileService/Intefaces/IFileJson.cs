using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Intefaces
{
    public interface IFileJson
    {
        /// <summary>
        /// Get file name
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Get streame for write or read file.
        /// </summary>
        /// <param name="write">if true stream a write mode.</param>
        /// <param name="_readonly"></param>
        /// <returns></returns>
        Stream GetStream(bool write, bool _readonly);

        /// <summary>
        /// Save stream in new file
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task SaveFile(Stream stream);

        Task<string[]> GetAllLines();

        List<string> GetJsonLines();

        void Delete();
    }
}
