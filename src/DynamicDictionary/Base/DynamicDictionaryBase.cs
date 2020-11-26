using System.Runtime.CompilerServices;



#if NET5_0
[SkipLocalsInit]
#endif
public abstract class DynamicDictionaryBase<TKey, TValue>
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract TValue GetValue(TKey key);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract bool TryGetValue(TKey key, out TValue value);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Change(TKey key, TValue value);


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
