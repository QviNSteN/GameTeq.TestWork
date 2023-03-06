using GameTeq.TestWork.General.FileService.Intefaces;
using GameTeq.TestWork.General.FileService.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Engines
{
    public class FileJson : IFileJson
    {
        private readonly FilesConfig _config;
        private readonly string _filePath;
        private readonly string _fileName;
        private readonly List<string> _lines = new List<string>();
        private readonly ReadWriteLock _lock = new ReadWriteLock();

        public FileJson(FilesConfig config, string fileName)
        {
            _config = config;
            _fileName = fileName;
            _filePath = Path.Combine(config.MainDirectory, fileName);
        }

        public FileJson(FilesConfig config, string fileName, string fileType)
        {
            _config = config;
            _fileName = fileName;
            _filePath = Path.Combine(config.MainDirectory, fileType, fileName);
        }

        public string GetName() => Path.GetFileName(_filePath);

        public Stream GetStream(bool write, bool _readonly)
        {
            return new FileStream(
                _filePath,
                _readonly ? FileMode.Open : FileMode.OpenOrCreate,
                write ? FileAccess.ReadWrite : FileAccess.Read,
                write ? FileShare.Read : FileShare.ReadWrite);
        }

        public async Task<string[]> GetAllLines()
        {
            using(var stream = GetStream(false, true))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer);

                var lines = text.Split(new char[] { '\n'}, StringSplitOptions.RemoveEmptyEntries);

                CashJsonLines(lines);

                return lines;
            }
        }

        public async Task SaveFile(Stream stream)
        {
            using(var saveStream = GetStream(true, false))
            {
                byte[] buffer = new byte[stream.Length];

                await stream.ReadAsync(buffer, 0, buffer.Length);
                await saveStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public async Task SaveFile(string json)
        {
            using (var stream = GenerateStreamFromString(json))
            {
                await SaveFile(stream);
            }
        }

        public void CashJsonLines(string[] jsonLines)
        {
            _lock.Lock(() =>
            {
                foreach(var line in jsonLines)
                {
                    if (!_lines.Contains(line))
                        _lines.Add(line);
                }
            }, ReadWriteLock.FileAction.Write);
        }

        public List<string> GetJsonLines()
        {
            return _lock.Lock(() =>
            {
                return _lines;
            }, ReadWriteLock.FileAction.Read);
        }


        public long GetLength()
        {
            // if not file do not exists, returns 0
            if (!Exists()) return 0;

            // get physical file length from OS
            var length = new FileInfo(_filePath).Length;

            return length;
        }

        /// <summary>
        /// Check if file exists (without open it)
        /// </summary>
        public bool Exists()
        {
            return File.Exists(_filePath);
        }

        /// <summary>
        /// Delete file (must all stream be closed)
        /// </summary>
        public void Delete()
        {
            File.Delete(_filePath);
        }

        /// <summary>
        /// Test if this file are locked by another process
        /// </summary>
        public bool IsLocked() => Exists() && IsFileLocked(_filePath);

        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static bool IsFileLocked(string filename)
        {
            FileStream stream = null;
            var file = new FileInfo(filename);

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException ex)
            {
                return true;
            }
            finally
            {
                stream?.Dispose();
            }

            //file is not locked
            return false;
        }
    }
}
