using BTFindTree;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{

    public class HashCache<TKey,TValue> : IDisposable
    {

        private readonly Func<TKey, int> KeyGetter;
        private readonly Func<TValue, int> ValueGetter;
        private readonly TKey[] KeyCache;
        private readonly TValue[] ValueCache;
        public readonly int Length;
        private readonly AssemblyDomain _domain;


        public HashCache(IDictionary<TKey, TValue> pairs)
        {

            var cache = new Dictionary<TKey, TValue>(pairs);
            Length = cache.Count;
            var key_builder = new Dictionary<TKey, string>();
            var value_builder = new Dictionary<TValue, string>();


            KeyCache = cache.Keys.ToArray();
            ValueCache = new TValue[KeyCache.Length];


            for (int i = 0; i < KeyCache.Length; i+=1)
            {
                key_builder[KeyCache[i]] =$"return {i};";
                value_builder[cache[KeyCache[i]]] = $"return {i};";
                ValueCache[i] = cache[KeyCache[i]];
            }



            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(BTFTemplate.GetHashBTFScript(key_builder));
            keyBuilder.Append("return -1;");


            KeyGetter = RFunc<TKey, int>.UnsafeDelegate(keyBuilder.ToString());



            StringBuilder valueBuilder = new StringBuilder();
            valueBuilder.Append(BTFTemplate.GetHashBTFScript(value_builder));
            valueBuilder.Append("return -1;");


            ValueGetter = RFunc<TValue, int>.UnsafeDelegate(valueBuilder.ToString());
        }


        public TValue this[TKey key] 
        {

            get
            {

                return GetValue(key);

            }

        }




        public TKey GetKey(TValue value)
        {

            int index = ValueGetter(value);
            if (index > -1)
            {
                return KeyCache[index];
            }
            return default;

        }




        public TValue GetValue(TKey key)
        {

            int index = KeyGetter(key);
            if (index > -1)
            {
                return ValueCache[index];
            }
            return default;

        }




        public bool ContainValue(TValue value)
        {
            return ValueGetter(value) != -1;
        }



        public bool ContainsKey(TKey key)
        {
            return KeyGetter(key) != -1;
        }




        public void Dispose()
        {
            _domain.Dispose();
        }

    }

}
