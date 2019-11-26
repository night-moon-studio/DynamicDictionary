using BTFindTree;
using DynamicCache;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Natasha
{
    public class FDC<V>
    {

        public MemberInfo OperatorInfo;
        public MethodInfo BuilderInfo;
        public Func<string, string> DealParameters;
        public string FindContent;



        public static FDC<V> operator |(FDC<V> template, IDictionary<string, string> dict)
        {
            template.FindContent = BTFTemplate.GetFuzzyPointBTFScript(dict);
            return template;
        }
        public static FDC<V> operator |(IDictionary<string, string> dict, FDC<V> template)
        {
            template.FindContent = BTFTemplate.GetFuzzyPointBTFScript(dict);
            return template;
        }




        public static FDC<V> operator |(Func<string, V> func, FDC<V> template)
        {

            var type = func.Method.DeclaringType.DeclaringType.With(typeof(string), typeof(V));
            var typeScript = type.GetDevelopName();


            var getMembers = NFunc<Type, MemberInfo[]>.Delegate($@"
            var type = typeof({typeScript});
            return  (
            from val in type.GetFields()
            where val.FieldType == arg
            select val).ToArray();", type, "System.Linq");


            var members = getMembers(func.GetType());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < members.Length; i += 1)
            {
                sb.Append($"if({typeScript}.{members[i].Name} == arg1){{ return arg2[{i}];}}");
            }
            sb.Append("return default;");


            var getMember = NFunc<Func<string, V>, MemberInfo[], MemberInfo>.Delegate(sb.ToString(), type);
            template.OperatorInfo = getMember(func, members);
            return template;

        }
        public static FDC<V> operator |(FDC<V> template, Func<string, V> func)
        {

            var type = func.Method.DeclaringType.DeclaringType.With(typeof(string), typeof(V));
            var typeScript = type.GetDevelopName();


            var getMembers = NFunc<Type, MemberInfo[]>.Delegate($@"
            var type = typeof({typeScript});
            return  (
            from val in type.GetFields()
            where val.FieldType == arg
            select val).ToArray();", type, "System.Linq");


            var members = getMembers(func.GetType());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < members.Length; i += 1)
            {
                sb.Append($"if({typeScript}.{members[i].Name} == arg1){{ return arg2[{i}];}}");
            }
            sb.Append("return default;");


            var getMember = NFunc<Func<string, V>, MemberInfo[], MemberInfo>.Delegate(sb.ToString(), type);
            template.OperatorInfo = getMember(func, members);
            return template;

        }




        public static FDC<V> operator |(Func<string, Func<string, V>> func, FDC<V> template)
        {

            template.BuilderInfo = func.Method;
            return template;

        }

        public static FDC<V> operator |(FDC<V> template, Func<string, Func<string, V>> func)
        {

            template.BuilderInfo = func.Method;
            return template;

        }




        public static FDC<V> operator %(Func<string, string> paraFunc, FDC<V> template)
        {

            template.DealParameters = paraFunc;
            return template;

        }
        public static FDC<V> operator %(FDC<V> template, Func<string, string> paraFunc)
        {

            template.DealParameters = paraFunc;
            return template;

        }


        public override string ToString()
        {

            var result = new StringBuilder(FindContent);
            result.Append(DynamicCacheTemplate.GetFromStaticClass(
                OperatorInfo.DeclaringType,
                OperatorInfo.Name,
                BuilderInfo.DeclaringType,
                BuilderInfo.Name,
                DealParameters));

            return result.ToString(); 

        }
    }
}
