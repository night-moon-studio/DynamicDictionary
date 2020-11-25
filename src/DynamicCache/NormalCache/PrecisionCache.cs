using BTFindTree;
using DynamicCache;
using System.Collections.Generic;

namespace System
{

    public class PrecisionCache<TValue> : DynamicDictionaryBuilder<string, TValue>
    {

        public PrecisionCache(IDictionary<string, TValue> pairs) : base(pairs)
        {

        }

        public override string ScriptKeyAction(IDictionary<string, string> dict, string paramName)
        {
            return BTFTemplate.GetGroupPrecisionPointBTFScript(dict, paramName);
        }
    }

}
