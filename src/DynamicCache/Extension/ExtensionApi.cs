using DynamicCache;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System
{

    public static class ExtensionApi
    {

        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this IDictionary<TKey, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new HashCache<TKey, TValue>(dict, direction);
        }
        public static CustomerCache<TKey, TValue> CustomerTree<TKey, TValue>(this IDictionary<TKey, TValue> dict, string keySwitchCode, Func<TKey, string> keyFunc, string valueSwitchCode = null, Func<TValue, string> valueFunc = null)
        {
            return new CustomerCache<TKey, TValue>(dict, keySwitchCode, keyFunc, valueSwitchCode, valueFunc);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this IDictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new FuzzyCache<TValue>(dict, direction);
        }




        public static PrecisionCache<TValue> PrecisioTree<TValue>(this IDictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new PrecisionCache<TValue>(dict, direction);
        }




        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this Dictionary<TKey, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new HashCache<TKey, TValue>(dict, direction);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this Dictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new FuzzyCache<TValue>(dict, direction);
        }




        public static PrecisionCache<TValue> PrecisioTree<TValue>(this Dictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new PrecisionCache<TValue>(dict, direction);
        }




        public static HashCache<TKey, TValue> HashTree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new HashCache<TKey, TValue>(dict, direction);
        }




        public static FuzzyCache<TValue> FuzzyTree<TValue>(this ConcurrentDictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new FuzzyCache<TValue>(dict, direction);
        }




        public static PrecisionCache<TValue> PrecisioTree<TValue>(this ConcurrentDictionary<string, TValue> dict, DyanamicCacheDirection direction = DyanamicCacheDirection.Both)
        {
            return new PrecisionCache<TValue>(dict, direction);
        }

    }

}
