using Natasha;
using System;
using System.Text;
namespace DynamicCache
{
    public class DynamicCacheTemplate
    {
        public static StringBuilder GetFromStaticClass(Type operatorType, string operatorMethod, Type builderType, string builderMethod,  Func<string,string> builderParameter = null)
        {

            return GetFromStaticClass(operatorType.GetDevelopName(), operatorMethod, builderType.GetDevelopName(), builderMethod, builderParameter);

        }


        public static StringBuilder GetFromStaticClass(string operatorType, string operatorMethod, string builderType, string builderMethod, Func<string, string> builderParameter = null)
        {

            return GetFromStaticClass($"{operatorType}.{operatorMethod}", $" {builderType}.{builderMethod}", builderParameter);

        }


        public static StringBuilder GetFromStaticClass(string operatorString, string builderString, Func<string, string> builderParameter = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{operatorString} = {builderString}({builderParameter?.Invoke("arg") ?? "arg"});");
            builder.AppendLine($"return {operatorString}(arg);");
            return builder;
        }
    }
}
