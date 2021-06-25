using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicDictionary.Api
{
    public class FuzzyDictionary<TValue> : FastStringDictionary<TValue>
    {
        public FuzzyDictionary()
        {
            this.SaveFastCache = () => { this._fast_cache = this._dict_cache.FuzzyTree(); };
        }
    }
}
