using System;
using System.Collections.Generic;
using System.Text;

namespace UTProject.Model
{
    public static class BuilderModel<T,V>
    {
        public static Func<T, V> Creator(T arg)
        {
            return null;
        }
    }
}
