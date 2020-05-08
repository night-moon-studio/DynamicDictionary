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

    public class CustomerCache<TKey,TValue> : DynamicCacheBuilder<TKey, TValue>
    {

        private readonly string _keySwitchCode;
        private readonly string _valueSwitchCode;
        private readonly Func<TKey, string> _keyCaseCode;
        private readonly Func<TValue, string> _valueCaseCode;
        public CustomerCache(IDictionary<TKey, TValue> pairs,string keySwitchCode, Func<TKey, string> keyFunc, string valueSwitchCode = null, Func<TValue, string> valueFunc = null) 
            : base(pairs, (keyFunc != null && valueFunc != null) ? DyanamicCacheDirection.Both : keyFunc == null ? DyanamicCacheDirection.ValueToKey : DyanamicCacheDirection.KeyToValue)
        {

            _keySwitchCode = keySwitchCode;
            _valueSwitchCode = valueSwitchCode;
            _keyCaseCode = keyFunc;
            _valueCaseCode = valueFunc;

        }

        public override string ScriptKeyAction(IDictionary<TKey, string> dict)
        {
            return BTFTemplate.GetCustomerBTFScript(dict, _keySwitchCode, _keyCaseCode);
        }
        public override string ScriptValueAction(IDictionary<TValue, string> dict)
        {
            return BTFTemplate.GetCustomerBTFScript(dict, _valueSwitchCode, _valueCaseCode);
        }

    }

}
