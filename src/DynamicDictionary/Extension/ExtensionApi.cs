using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System
{

    public static class ExtensionApi
    {

        public static DynamicDictionaryBase<TKey, TValue> HashTree<TKey, TValue>(this IDictionary<TKey, TValue> dict, bool useDefault = false)
        {
            return new HashCache<TKey, TValue>(dict, useDefault).Instance;
        }
        public static DynamicDictionaryBase<TKey, TValue> CustomerTree<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<TKey, string> keyFunc,bool useDefault=false)
        {
            return new CustomerCache<TKey, TValue>(dict, keyFunc,useDefault).Instance;
        }




        public static DynamicDictionaryBase<string,TValue> FuzzyTree<TValue>(this IDictionary<string, TValue> dict, bool useDefault = false)
        {
            return new FuzzyCache<TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<string, TValue> PrecisioTree<TValue>(this IDictionary<string, TValue> dict, bool useDefault = false)
        {
            return new PrecisionCache<TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<TKey, TValue> HashTree<TKey, TValue>(this Dictionary<TKey, TValue> dict, bool useDefault = false)
        {
            return new HashCache<TKey, TValue>(dict).Instance;
        }




        public static DynamicDictionaryBase<string, TValue> FuzzyTree<TValue>(this Dictionary<string, TValue> dict, bool useDefault = false)
        {
            return new FuzzyCache<TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<string, TValue> PrecisioTree<TValue>(this Dictionary<string, TValue> dict, bool useDefault = false)
        {
            return new PrecisionCache<TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<TKey, TValue> HashTree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, bool useDefault = false)
        {
            return new HashCache<TKey, TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<string, TValue> FuzzyTree<TValue>(this ConcurrentDictionary<string, TValue> dict, bool useDefault = false)
        {
            return new FuzzyCache<TValue>(dict, useDefault).Instance;
        }




        public static DynamicDictionaryBase<string, TValue> PrecisioTree<TValue>(this ConcurrentDictionary<string, TValue> dict, bool useDefault = false)
        {
            return new PrecisionCache<TValue>(dict, useDefault).Instance;
        }

    }

}
