using BTFindTree;
using DynamicCache;
using Natasha;
using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
