using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeq.TestWork.General.FileService.Engines
{
    public class ReadWriteLock
    {
        public enum FileAction { Read, Write };

        private readonly ReaderWriterLockSlim _lock = new();

        public void Lock(Action action, FileAction fileAction)
        {
            switch (fileAction)
            {
                case FileAction.Read:
                    try
                    {
                        _lock.EnterReadLock();
                        action();
                    }
                    finally
                    {
                        _lock.ExitReadLock();
                    }
                    break;
                case FileAction.Write:
                    try
                    {
                        _lock.EnterWriteLock();
                        action();
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                    break;
            }
        }

        public T? Lock<T>(Func<T> action, FileAction fileAction)
        {
            switch (fileAction)
            {
                case FileAction.Read:
                    try
                    {
                        _lock.EnterReadLock();
                        return action();
                    }
                    finally
                    {
                        _lock.ExitReadLock();
                    }
                case FileAction.Write:
                    try
                    {
                        _lock.EnterWriteLock();
                        return action();
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
            }

            return default(T);
        }
    }
}
