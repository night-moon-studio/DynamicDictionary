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

    public class FuzzyCache<TValue> : DynamicDictionaryBuilder<string, TValue>
    {

        public FuzzyCache(IDictionary<string, TValue> pairs) :base(pairs)
        {

           
        }

        public override string ScriptKeyAction(IDictionary<string, string> dict, string paramName)
        {
            return BTFTemplate.GetGroupFuzzyPointBTFScript(dict, paramName);
        }


    }

}
