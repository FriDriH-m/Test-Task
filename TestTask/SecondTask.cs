namespace SecondTask
{
    public static class Server
    {
        private static ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        private static int _count = 0;

        public static int GetCount()
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

        public static void AddToCount(int value)
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
