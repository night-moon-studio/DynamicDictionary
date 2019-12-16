using BTFindTree;
using Natasha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{

    public class PrecisionCache<TValue> : IDisposable
    {

        private readonly Func<string, int> KeyGetter;
        private readonly Func<TValue, int> ValueGetter;
        private readonly string[] KeyCache;
        private readonly TValue[] ValueCache;
        public readonly int Length;


        public PrecisionCache(IDictionary<string, TValue> pairs)
        {

            var cache = new Dictionary<string, TValue>(pairs);
            Length = cache.Count;
            var key_builder = new Dictionary<string, string>();
            var value_builder = new Dictionary<TValue, string>();


            KeyCache = cache.Keys.ToArray();
            ValueCache = new TValue[KeyCache.Length];


            for (int i = 0; i < KeyCache.Length; i += 1)
            {
                key_builder[KeyCache[i]] = $"return {i};";
                value_builder[cache[KeyCache[i]]] = $"return {i};";
                ValueCache[i] = cache[KeyCache[i]];
            }





            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(BTFTemplate.GetPrecisionPointBTFScript(key_builder));
            keyBuilder.Append("return -1;");


            KeyGetter = NDomain.Random().UnsafeFunc<string, int>(keyBuilder.ToString());



            StringBuilder valueBuilder = new StringBuilder();
            valueBuilder.Append(BTFTemplate.GetHashBTFScript(value_builder));
            valueBuilder.Append("return -1;");


            ValueGetter = NDomain.Random().UnsafeFunc<TValue, int>(valueBuilder.ToString());

        }


        public TValue this[string key] 
        {

            get
            {

                return GetValue(key);

            }

        }




        public string GetKey(TValue value)
        {

            int index = ValueGetter(value);
            if (index != -1)
            {
                return KeyCache[index];
            }
            return default;

        }




        public TValue GetValue(string key)
        {

            int index = KeyGetter(key);
            if (index != -1)
            {
                return ValueCache[index];
            }
            return default;

        }




        public bool ContainValue(TValue value)
        {
            return ValueGetter(value) != -1;
        }



        public bool ContainsKey(string key)
        {
            return KeyGetter(key) != -1;
        }




        public void Dispose()
        {
            KeyGetter.DisposeDomain();
            ValueGetter.DisposeDomain();
        }

    }

}
