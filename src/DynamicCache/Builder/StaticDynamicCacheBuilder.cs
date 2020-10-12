using Natasha.CSharp;
using RuntimeToDynamic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DynamicCache
{

    public unsafe abstract class StaticDynamicCacheBuilder<TKey,TValue> : IDisposable
    {

        public readonly Type ProxyType;
        public readonly delegate* managed<TKey, TValue> GetValue;
        public readonly delegate* managed<TValue, TKey[]> GetKeys;

        private string _callerMethod;
        private string _builderMethod;
        private ConcurrentDictionary<string, string> _typeReturnCache;
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict);
        public abstract string ScriptValueAction(IDictionary<TValue, string> dict);


        public void SetCaller(string caller)
        {
            _callerMethod = caller;
        }
        public void SetBuilder(string builder)
        {
            _builderMethod = builder;
        }
        public StaticDynamicCacheBuilder(IDictionary<TKey, TValue> pairs, DyanamicCacheDirection queryDirection =  DyanamicCacheDirection.Both)
        {

            _typeReturnCache = new ConcurrentDictionary<string, string>();

        }




        public TValue this[TKey key]
        {

            get
            {

                return GetValue(key);

            }

        }




        public void Dispose()
        {
            //GetValue = null;
            //GetKeys = null;
            ProxyType.DisposeDomain();
            //ProxyType = null;
        }

    }

}
