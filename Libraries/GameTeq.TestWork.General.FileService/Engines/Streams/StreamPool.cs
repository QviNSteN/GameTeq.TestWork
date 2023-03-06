using GameTeq.TestWork.General.FileService.Intefaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Engines.Streams
{
    internal class StreamPool : IDisposable
    {
        private readonly ConcurrentBag<Stream> _pool = new ConcurrentBag<Stream>();
        private readonly Lazy<Stream> _writer;
        private IFileJson _file;

        public StreamPool(IFileJson file)
        {
            _writer = new Lazy<Stream>(() => file.GetStream(false,false), true);
            _file = file;
        }

        /// <summary>
        /// Get single Stream writer instance
        /// </summary>
        public Stream Writer => _writer.Value;

        public Stream Rent()
        {
            if (!_pool.TryTake(out var stream))
            {
                stream = _file.GetStream(false,false);
            }

            return stream;
        }

        public void Return(Stream stream)
        {
            _pool.Add(stream);
        }

        public void Dispose()
        {
            foreach (var stream in _pool)
            {
                stream.Dispose();
            }

            // do writer dispose (wait async writer thread)
            if (_writer.IsValueCreated)
            {
                _writer.Value.Dispose();
            }
        }
    }
}
