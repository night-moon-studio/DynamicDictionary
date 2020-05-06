using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCache
{
    public abstract class DynamicCacheBuilder<TKey,TValue> : IDisposable
    {

        private readonly Func<TKey, int> KeyGetter;
        private readonly Func<TValue, int[]> ValueGetter;
        private readonly TKey[] KeyCache;
        private readonly TValue[] ValueCache;
        public readonly int Length;
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict);
        public abstract string ScriptValueAction(IDictionary<TValue, string> dict);

        public DynamicCacheBuilder(IDictionary<TKey, TValue> pairs, DyanamicCacheDirection queryDirection =  DyanamicCacheDirection.Both)
        {

            var cache = new Dictionary<TKey, TValue>(pairs);
            Length = cache.Count;
            KeyCache = pairs.Keys.ToArray();
            ValueCache = new TValue[Length];
            for (int i = 0; i < Length; i += 1)
            {

                var value = cache[KeyCache[i]];
                ValueCache[i] = value;

            }


            if (queryDirection != DyanamicCacheDirection.ValueToKey)
            {

               
                var key_builder = new Dictionary<TKey, string>();                
                for (int i = 0; i < Length; i += 1)
                {
                    var key = KeyCache[i];
                    key_builder[key] = $"return {i};";

                }
                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(ScriptKeyAction(key_builder));
                keyBuilder.Append("return -1;");
                KeyGetter = NDelegate.RandomDomain().UnsafeFunc<TKey, int>(keyBuilder.ToString());

            }
            


            if (queryDirection != DyanamicCacheDirection.KeyToValue)
            {

                var value_builder = new Dictionary<TValue, string>();
                var temp_value_builder = new Dictionary<TValue, string>();
                for (int i = 0; i < Length; i += 1)
                {
                    var value = cache[KeyCache[i]];
                    if (!temp_value_builder.ContainsKey(value))
                    {
                        temp_value_builder[value] = $"return new int[]{{{i}";
                    }
                    else
                    {
                        temp_value_builder[value] += $",{i}";
                    }

                }
                foreach (var item in temp_value_builder)
                {

                    value_builder[item.Key] = item.Value + "};";

                }

                StringBuilder valueBuilder = new StringBuilder();
                valueBuilder.Append(ScriptValueAction(value_builder));
                valueBuilder.Append("return null;");
                ValueGetter = NDelegate.RandomDomain().UnsafeFunc<TValue, int[]>(valueBuilder.ToString());

            }

        }


        public TValue this[TKey key]
        {

            get
            {

                return GetValue(key);

            }

        }




        public TKey[] GetKeys(TValue value)
        {

            int[] index = ValueGetter(value);
            if (index != null)
            {
                var result = new TKey[index.Length];
                for (int i = 0; i < index.Length; i++)
                {
                    result[i] = KeyCache[i];
                }
                return result;
            }
            return default;

        }




        public TValue GetValue(TKey key)
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
            return ValueGetter(value) != null;
        }



        public bool ContainsKey(TKey key)
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
