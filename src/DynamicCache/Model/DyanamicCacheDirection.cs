using System;

namespace DynamicCache
{
    [Flags]
    public enum DyanamicCacheDirection
    {
        KeyToValue,
        ValueToKey,
        Both
    }
}
