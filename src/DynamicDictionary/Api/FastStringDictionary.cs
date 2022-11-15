using DynamicDictionary.Api.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DynamicDictionary.Api
{
    public class FastStringDictionary<TValue> : IDictionary<string, TValue>
    {
        private readonly CurrentLock _currentLock;
        protected internal readonly ConcurrentDictionary<string, TValue> _dict_cache;
        protected internal DynamicDictionaryBase<string, TValue> _fast_cache;
        protected Action SaveFastCache;
        protected internal bool use_default;
        public FastStringDictionary(bool useDefault)
        {
            use_default = useDefault;
            _currentLock = new CurrentLock();
            _dict_cache = new ConcurrentDictionary<string, TValue>();
            _fast_cache = _dict_cache.PrecisioTree(use_default);

        }
        public TValue this[string key] 
        { 

            get
            {
                if (_currentLock.CanGetLock())
                {
                    return _fast_cache[key];
                }
                return _dict_cache[key];
            }
            set 
            {

                _dict_cache[key] = value;
                Add(key, value);

            } 
        }

        public ICollection<string> Keys => _dict_cache.Keys;

        public ICollection<TValue> Values => _dict_cache.Values;

        public int Count => _dict_cache.Count;

        public bool IsReadOnly => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(string key, TValue value)
        {
            _dict_cache[key] = value;
            Refresh();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(KeyValuePair<string, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dict_cache.Clear();
            Refresh();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Refresh()
        {
            _currentLock.GetAndWaitLock();
            SaveFastCache();
            _currentLock.ReleaseLock();
        }

        public bool Contains(KeyValuePair<string, TValue> item)
        {

            if (TryGetValue(item.Key, out var result))
            {

                return result.Equals(item.Value);

            }
            return false;
            
        }

        public bool ContainsKey(string key)
        {
            if (_currentLock.CanGetLock())
            {
                return _fast_cache.TryGetValue(key, out var _);
            }
            return _dict_cache.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i+=1)
            {
                _dict_cache[array[i].Key] = array[i].Value;
            }
            Refresh();
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return _dict_cache.GetEnumerator();
        }

        public bool Remove(string key)
        {
            if (_dict_cache.TryRemove(key,out TValue _))
            {
                Refresh();
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<string, TValue> item)
        {
            if (_dict_cache.TryRemove(item.Key, out TValue value))
            {
                if (item.Value.Equals(value))
                {
                    Refresh();
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(string key, out TValue value)
        {
            if (_currentLock.CanGetLock())
            {
                return _fast_cache.TryGetValue(key, out value);
            }
            return _dict_cache.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict_cache.GetEnumerator();
        }
    }
}
