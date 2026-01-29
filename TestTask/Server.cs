
using System.Reflection.Metadata.Ecma335;

namespace SecondTask
{
    public class Server
    {
        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        private int _count = 0;

        public int GetCount()
        {
            _locker.EnterReadLock();
            try
            {
                return _count;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void AddToCount(int value)
        {
            _locker.EnterWriteLock();
            try
            {
                _count += value;
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}
