using BTFindTree;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCache
{

    public class FuzzyCache<TValue> : IDisposable
    {

        public Func<string, int> KeyGetter;
        public Func<TValue, int> ValueGetter;
        private Dictionary<string, string> _key_builder;
        private Dictionary<TValue, string> _value_builder;
        public string[] KeyCache;
        public TValue[] ValueCache;
        public int Length;
        public int Current;
        private AssemblyDomain _domain;


        public FuzzyCache(IDictionary<string, TValue> pairs)
        {

            var _cache = new Dictionary<string, TValue>(pairs);

            KeyCache = _cache.Keys.ToArray();
            var values = new TValue[KeyCache.Length];


            for (int i = 0; i < KeyCache.Length; i+=1)
            {
                _key_builder[KeyCache[i]] =$"return {i};";
                _value_builder[_cache[KeyCache[i]]] = $"return {i};";
                values[i] = _cache[KeyCache[i]];
            }


            _domain = DomainManagment.Create(Guid.NewGuid().ToString());


            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(BTFTemplate.GetFuzzyPointBTFScript(_key_builder));
            keyBuilder.Append("return -1;");


            var builder = FakeMethodOperator.New;
            builder.Complier.Domain = _domain;
            KeyGetter =  builder.MethodBody(keyBuilder.ToString())
                .Complie<Func<string, int>>();



            StringBuilder valueBuilder = new StringBuilder();
            keyBuilder.Append(BTFTemplate.GetHashBTFScript(_value_builder));
            keyBuilder.Append("return -1;");


            builder = FakeMethodOperator.New;
            builder.Complier.Domain = _domain;
            ValueGetter = builder.MethodBody(keyBuilder.ToString())
                .Complie<Func<TValue, int>>();
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
            if (index > -1)
            {
                return KeyCache[index];
            }
            return default;

        }




        public TValue GetValue(string key)
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



        public bool ContainsKey(string key)
        {
            return KeyGetter(key) != -1;
        }




        public void Dispose()
        {
            _domain.Dispose();
        }

    }

}
