using Natasha;
using System;
using System.Text;
namespace DynamicCache
{
    public class DynamicCacheTemplate
    {
        public static StringBuilder GetFromStaticClass(Type operatorType, string operatorMethod, Type builderType, string builderMethod,  Func<string,string> builderParameter = null)
        {
            StringBuilder builder = new StringBuilder();
            string operatorTypeName = operatorType.GetDevelopName();
            builder.Append($"{operatorTypeName}.{operatorMethod} = {builderType.GetDevelopName()}.{builderMethod}({builderParameter?.Invoke("arg")??"arg"});");
            builder.Append($"return {operatorTypeName}.{operatorMethod}(arg);");
            return builder;
        }
    }
}
