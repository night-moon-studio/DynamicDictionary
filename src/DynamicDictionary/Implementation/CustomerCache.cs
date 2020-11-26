using BTFindTree;
using DynamicDictionary;
using System.Collections.Generic;

namespace System
{

    public class CustomerCache<TKey,TValue> : DynamicDictionaryBuilder<TKey, TValue>
    {

        private readonly string _keySwitchCode;
        private readonly Func<TKey, string> _keyCaseCode;
        public CustomerCache(IDictionary<TKey, TValue> pairs,string keySwitchCode, Func<TKey, string> keyFunc) 
            : base(pairs)
        {

            _keySwitchCode = keySwitchCode;
            _keyCaseCode = keyFunc;

        }

        public override string ScriptKeyAction(IDictionary<TKey, string> dict, string paramName)
        {
            return BTFTemplate.GetCustomerBTFScript(dict, _keySwitchCode, _keyCaseCode);
        }
    }

}
