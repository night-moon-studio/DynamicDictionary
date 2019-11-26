using System;
using System.Collections.Generic;
using System.Text;

namespace UTProject.Model
{
    public static class OperatorModel<T,V>
    {
        public static Func<T, V> Creator;
        public static int Flag;
        public static Func<T, V> Error;

        static OperatorModel()
        {
            Creator = item => default;
        }
    }
}
