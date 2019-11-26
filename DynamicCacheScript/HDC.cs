using BTFindTree;
using DynamicCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Natasha
{
    public class HDC<T, S>
    {

        public MemberInfo OperatorInfo;
        public MethodInfo BuilderInfo;
        public Func<string, string> DealParameters;
        public string FindContent;



        public static HDC<T, S> operator |(HDC<T, S> template, IDictionary<T, string> dict)
        {
            template.FindContent = BTFTemplate.GetHashBTFScript(dict);
            return template;
        }
        public static HDC<T, S> operator |(IDictionary<T, string> dict, HDC<T, S> template)
        {
            template.FindContent = BTFTemplate.GetHashBTFScript(dict);
            return template;
        }




        public static HDC<T, S> operator |(Func<T, S> func, HDC<T, S> template)
        {

            var type = func.Method.DeclaringType.DeclaringType.With(typeof(T), typeof(S));
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


            var getMember = RFunc<Func<T, S>, MemberInfo[], MemberInfo>.Delegate(sb.ToString(), type);
            template.OperatorInfo = getMember(func, members);
            return template;

        }
        public static HDC<T, S> operator |(HDC<T, S> template, in Func<T, S> func)
        {

            var type = func.Method.DeclaringType.DeclaringType.With(typeof(T), typeof(S));
            var typeScript = type.GetDevelopName();


            var getMembers= RFunc<Type,MemberInfo[]>.Delegate($@"
            var type = typeof({typeScript});
            return  (
            from val in type.GetFields()
            where val.FieldType == arg
            select val).ToArray();", type, "System.Linq");
           

            var members = getMembers(func.GetType());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < members.Length; i+=1)
            {
                sb.Append($"if({typeScript}.{members[i].Name} == arg1){{ return arg2[{i}];}}");
            }
            sb.Append("return default;");


            var getMember = RFunc<Func<T, S>,MemberInfo[],MemberInfo>.Delegate(sb.ToString(), type);
            template.OperatorInfo = getMember(func, members);
            return template;

        }




        public static HDC<T, S> operator |(Func<T, Func<T, S>> func, HDC<T, S> template)
        {

            template.BuilderInfo = func.Method;
            return template;

        }

        public static HDC<T, S> operator |(HDC<T, S> template, Func<T, Func<T, S>> func)
        {

            template.BuilderInfo = func.Method;
            return template;

        }




        public static HDC<T, S> operator %(Func<string, string> paraFunc, HDC<T, S> template)
        {

            template.DealParameters = paraFunc;
            return template;

        }
        public static HDC<T, S> operator %(HDC<T, S> template, Func<string, string> paraFunc)
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
