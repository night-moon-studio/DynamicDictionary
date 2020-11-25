using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



#if NET5_0
[SkipLocalsInit]
#endif
public abstract class DynamicDictionaryBase<TKey, TValue>
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe abstract TValue GetValue(TKey key);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe abstract bool TryGetValue(TKey key, out TValue value);
    //public abstract bool TryGetKeys(TValue value, out TKey[] keys);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe abstract void Change(TKey key, TValue value);


    public TValue this[TKey key]
    {

        get
        {

            return GetValue(key);

        }
        set
        {
            Change(key, value);
        }

    }

}
