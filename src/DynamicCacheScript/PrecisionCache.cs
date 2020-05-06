using BTFindTree;
using DynamicCache;
using System.Collections.Generic;

namespace System
{

    public class PrecisionCache<TValue> : DynamicCacheBuilder<string, TValue>
    {

        public PrecisionCache(IDictionary<string, TValue> pairs, DyanamicCacheDirection queryDirection = DyanamicCacheDirection.Both) : base(pairs, queryDirection)
        {

        }

        public override string ScriptValueAction(IDictionary<TValue, string> dict)
        {
            return BTFTemplate.GetHashBTFScript(dict);
        }
        public override string ScriptKeyAction(IDictionary<string, string> dict)
        {
            return BTFTemplate.GetGroupPrecisionPointBTFScript(dict);
        }


    }

}
