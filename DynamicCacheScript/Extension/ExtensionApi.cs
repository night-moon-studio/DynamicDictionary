using DynamicCache;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System
{

    public static class ExtensionApi
    {

        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return new HashCache<TKey, TValue>(dict);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this IDictionary<string, TValue> dict)
        {
            return new FuzzyCache<TValue>(dict);
        }



        public static PrecisionCache<TValue> PrecisioTree<TValue>(this IDictionary<string, TValue> dict)
        {
            return new PrecisionCache<TValue>(dict);
        }


        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return new HashCache<TKey, TValue>(dict);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this Dictionary<string, TValue> dict)
        {
            return new FuzzyCache<TValue>(dict);
        }



        public static PrecisionCache<TValue> PrecisioTree<TValue>(this Dictionary<string, TValue> dict)
        {
            return new PrecisionCache<TValue>(dict);
        }



        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict)
        {
            return new HashCache<TKey, TValue>(dict);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this ConcurrentDictionary<string, TValue> dict)
        {
            return new FuzzyCache<TValue>(dict);
        }



        public static PrecisionCache<TValue> PrecisioTree<TValue>(this ConcurrentDictionary<string, TValue> dict)
        {
            return new PrecisionCache<TValue>(dict);
        }

    }

}
