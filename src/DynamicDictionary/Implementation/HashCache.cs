using BTFindTree;
using DynamicDictionary;
using System.Collections.Generic;

namespace System
{

    public class HashCache<TKey,TValue> : DynamicDictionaryBuilder<TKey, TValue>
    {

        public HashCache(IDictionary<TKey, TValue> pairs) : base(pairs)
        {


        }

        public override string ScriptKeyAction(IDictionary<TKey, string> dict, string paramName)
        {
            return BTFTemplate.GetHashBTFScript(dict, paramName);
        }


    }

}
