using System.Runtime.CompilerServices;
using System.Threading;

namespace DynamicDictionary.Api.Utils
{
    public class CurrentLock
    {

        private int _lockCount = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetLock()
        {
            return Interlocked.CompareExchange(ref _lockCount, 1, 0) == 0;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanGetLock()
        {
            bool result = Interlocked.CompareExchange(ref _lockCount, 1, 0) == 0;
            if (result)
            {
                _lockCount = 0;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAndWaitLock()
        {
            while (Interlocked.CompareExchange(ref _lockCount, 1, 0) != 0)
            {
                Thread.Sleep(20);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleaseLock()
        {
            _lockCount = 0;
        }

    }
}


