using BTFindTree;
using DynamicCache;
using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Natasha
{
    public class HDC<P,T,S>
    {

        public MemberInfo OperatorInfo;
        public MethodInfo BuilderInfo;
        public Func<string, string> DealParameters;
        public string FindContent;



        public static HDC<P,T,S> operator |(HDC<P,T,S> template, IDictionary<T,String> dict)
        {
            template.FindContent = BTFTemplate.GetHashBTFScript(dict);
            return template;
        }
        public static HDC<P,T,S> operator |(IDictionary<T,String> dict, HDC<P,T,S> template)
        {
            template.FindContent = BTFTemplate.GetHashBTFScript(dict);
            return template;
        }



        public HDC<P, T, S> GetDC(Func<T, S> func)
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
                type = func.Method.DeclaringType.DeclaringType.With(typeof(T));
            }
            else
            {
                type = func.Method.DeclaringType.DeclaringType.With(typeof(string), typeof(T));
            }

            var typeScript = type.GetDevelopName();


            var getMembers = NDelegate.Random().Func<Type, MemberInfo[]>($@"
            var type = typeof({typeScript});
            return  (
            from val in type.GetFields()
            where val.FieldType == arg
            select val).ToArray();", type, "System.Linq");


            var members = getMembers(func.GetType());
            getMembers.DisposeDomain();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < members.Length; i += 1)
            {
                sb.Append($"if({typeScript}.{members[i].Name} == arg1){{ return arg2[{i}];}}");
            }
            sb.Append("return default;");


            var getMember = NDelegate.Random().Func<Func<T, S>, MemberInfo[], MemberInfo>(sb.ToString(), type);
            OperatorInfo = getMember(func, members);
            getMember.DisposeDomain();
            return this;
        }
        public static HDC<P,T,S> operator |(Func<T,S> func, HDC<P,T,S> template)
        {

            return template.GetDC(func);

        }
        public static HDC<P,T,S> operator |(HDC<P,T,S> template, Func<T,S> func)
        {

            return template.GetDC(func);

        }




        public static HDC<P,T,S> operator |(Func<P, Func<T,S>> func, HDC<P,T,S> template)
        {

            template.BuilderInfo = func.Method;
            return template;

        }

        public static HDC<P,T,S> operator |(HDC<P,T,S> template, Func<P, Func<T,S>> func)
        {

            template.BuilderInfo = func.Method;
            return template;

        }




        public static HDC<P,T,S> operator %(Func<string, string> paraFunc, HDC<P,T,S> template)
        {

            template.DealParameters = paraFunc;
            return template;

        }
        public static HDC<P,T,S> operator %(HDC<P,T,S> template, Func<string, string> paraFunc)
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
