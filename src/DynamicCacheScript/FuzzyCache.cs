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

    public class FuzzyCache<TValue> : DynamicCacheBuilder<string, TValue>
    {

        public FuzzyCache(IDictionary<string, TValue> pairs, DyanamicCacheDirection queryDirection = DyanamicCacheDirection.Both) :base(pairs, queryDirection)
        {

           
        }

        public override string ScriptValueAction(IDictionary<TValue, string> dict)
        {
            return BTFTemplate.GetHashBTFScript(dict);
        }
        public override string ScriptKeyAction(IDictionary<string, string> dict)
        {
            return BTFTemplate.GetFuzzyPointBTFScript(dict);
        }

    }

}
