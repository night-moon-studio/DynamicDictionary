using BTFindTree;
using DynamicCache;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Natasha
{
    public class FDC<P,V>
    {

        public MemberInfo OperatorInfo;
        public MethodInfo BuilderInfo;
        public Func<string, string> DealParameters;
        public string FindContent;



        public static FDC<P,V> operator |(FDC<P,V> template, IDictionary<string, string> dict)
        {
            template.FindContent = BTFTemplate.GetFuzzyPointBTFScript(dict);
            return template;
        }
        public static FDC<P,V> operator |(IDictionary<string, string> dict, FDC<P,V> template)
        {
            template.FindContent = BTFTemplate.GetFuzzyPointBTFScript(dict);
            return template;
        }




        public FDC<P, V> GetDC(Func<string, V> func)
        {
            var tempType = func.Method.DeclaringType.DeclaringType;
            int count = tempType.GetGenericArguments().Length;
            Type type = default;
            if (count == 0)
            {
                type = func.Method.DeclaringType.DeclaringType;

            }
            else if (count == 1)
            {
                type = func.Method.DeclaringType.DeclaringType.With(typeof(V));
            }
            else
            {
                type = func.Method.DeclaringType.DeclaringType.With(typeof(string), typeof(V));
            }

            var typeScript = type.GetDevelopName();


            var getMembers = RFunc<Type, MemberInfo[]>.Delegate($@"
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


            var getMember = RFunc<Func<string, V>, MemberInfo[], MemberInfo>.Delegate(sb.ToString(), type);
            OperatorInfo = getMember(func, members);
            return this;
        }


        public static FDC<P, V> operator |(Func<string, V> func, FDC<P, V> template)
        {

            return template.GetDC(func);

        }
        public static FDC<P, V> operator |(FDC<P, V> template, Func<string, V> func)
        {

            return template.GetDC(func);

        }




        public static FDC<P,V> operator |(Func<P, Func<string, V>> func, FDC<P,V> template)
        {

            template.BuilderInfo = func.Method;
            return template;

        }

        public static FDC<P,V> operator |(FDC<P,V> template, Func<P, Func<string, V>> func)
        {

            template.BuilderInfo = func.Method;
            return template;

        }




        public static FDC<P,V> operator % (Func<string, string> paraFunc, FDC<P,V> template)
        {

            template.DealParameters = paraFunc;
            return template;

        }
        public static FDC<P,V> operator % (FDC<P,V> template, Func<string, string> paraFunc)
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
