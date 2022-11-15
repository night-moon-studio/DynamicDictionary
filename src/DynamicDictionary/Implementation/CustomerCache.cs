using BTFindTree;
using DynamicDictionary;
using System.Collections.Generic;

namespace System
{

    public class CustomerCache<TKey,TValue> : DynamicSwitchBuilder<TKey, TValue>
    {

        public CustomerCache(IDictionary<TKey, TValue> pairs, Func<TKey, string> keyToCase, bool useDefault = false) : base(pairs, keyToCase, useDefault)
        {
           

        }

        public override string ScriptKeyAction(IDictionary<TKey, string> dict, string paramName, Func<TKey, string> func = null)
        {
            return BTFTemplate.GetCustomerBTFScript(dict, paramName, func);
        }
    }

}
