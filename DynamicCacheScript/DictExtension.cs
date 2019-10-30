using BTFindTree;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DynamicCacheScript
{
    public static class DictExtension
    {

        public static StringBuilder HashDynamicScript<T>(this IDictionary<T, string> dict, MethodInfo operatorInfo, MethodInfo builderInfo, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorInfo.ReflectedType,
                operatorInfo.Name,
                builderInfo.ReflectedType,
                builderInfo.Name,
                mapping);
            result.Insert(0, BTFTemplate.GetHashBTFScript(dict));
            return result;

        }




        public static StringBuilder HashDynamicScript<T>(this IDictionary<T, string> dict, Type operatorType, string operatorMethod, Type builderType, string builderMethod, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorType,
                operatorMethod,
                builderType,
                builderMethod,
                mapping);
            result.Insert(0, BTFTemplate.GetHashBTFScript(dict));
            return result;

        }




        public static StringBuilder PrecisionDynamicScript(this IDictionary<string,string> dict, MethodInfo operatorInfo,MethodInfo builderInfo, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorInfo.ReflectedType,
                operatorInfo.Name,
                builderInfo.ReflectedType,
                builderInfo.Name,
                mapping);
            result.Insert (0, BTFTemplate.GetPrecisionPointBTFScript(dict));
            return result;

        }




        public static StringBuilder PrecisionDynamicScript(this IDictionary<string, string> dict, Type operatorType, string operatorMethod, Type builderType, string builderMethod, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorType,
                operatorMethod,
                builderType,
                builderMethod,
                mapping);
            result.Insert(0, BTFTemplate.GetPrecisionPointBTFScript(dict));
            return result;

        }




        public static StringBuilder FuzzyDynamicScript(this IDictionary<string, string> dict, MethodInfo operatorInfo, MethodInfo builderInfo, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorInfo.ReflectedType,
                operatorInfo.Name,
                builderInfo.ReflectedType,
                builderInfo.Name,
                mapping);
            result.Insert(0, BTFTemplate.GetFuzzyPointBTFScript(dict));
            return result;

        }




        public static StringBuilder FuzzyDynamicScript(this IDictionary<string, string> dict, Type operatorType, string operatorMethod, Type builderType, string builderMethod, Func<string, string> mapping = null)
        {

            var result = DynamicCacheTemplate.GetFromStaticClass(
                operatorType,
                operatorMethod,
                builderType,
                builderMethod,
                mapping);
            result.Insert(0, BTFTemplate.GetFuzzyPointBTFScript(dict));
            return result;

        }

    }

}
