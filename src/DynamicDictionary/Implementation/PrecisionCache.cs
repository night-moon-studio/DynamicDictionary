using BTFindTree;
using DynamicDictionary;
using System.Collections.Generic;

namespace System
{

    public class PrecisionCache<TValue> : DynamicDictionaryBuilder<string, TValue>
    {

        public PrecisionCache(IDictionary<string, TValue> pairs, bool useDefault = false) : base(pairs, useDefault)
        {

        }

        public override string ScriptKeyAction(IDictionary<string, string> dict, string paramName)
        {
            return BTFTemplate.GetGroupPrecisionPointBTFScript(dict, paramName);
        }
    }

}
