using Natasha.CSharp;
using RuntimeToDynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCache
{

    public unsafe abstract class DynamicCacheBuilder<TKey, TValue> : IDisposable
    {

        public readonly Type ProxyType;
        public readonly delegate* managed<TKey, TValue> GetValue;
        public readonly delegate* managed<TValue, TKey[]> GetKeys;
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict);
        public abstract string ScriptValueAction(IDictionary<TValue, string> dict);

        public DynamicCacheBuilder(IDictionary<TKey, TValue> pairs, DyanamicCacheDirection queryDirection = DyanamicCacheDirection.Both)
        {

            if (queryDirection != DyanamicCacheDirection.KeyToValue)
            {

                //构建快查字典 给BTF使用
                var value_builder = new Dictionary<TValue, List<TKey>>();
                foreach (var item in pairs)
                {

                    if (!value_builder.ContainsKey(item.Value))
                    {
                        value_builder[item.Value] = new List<TKey>();
                    }

                    value_builder[item.Value].Add(item.Key);

                }
                var tempDict = value_builder.ToDictionary(item => item.Key, item => item.Value.ToArray());
                GetKeys = tempDict.HashTree(DyanamicCacheDirection.KeyToValue).GetValue;
            }

            

            if (queryDirection != DyanamicCacheDirection.ValueToKey)
            {

                var nClass = NClass.RandomDomain()
                        .Public()
                        .Static()
                        .Unsafe();
#if NET5_0
                nClass.SkipInit();
#endif
                AnonymousRTD _r2d_handler = new AnonymousRTD();
                _r2d_handler.UseStaticReadonlyFields();

                //构建快查字典 给BTF使用
                var key_builder = new Dictionary<TKey, string>();
                foreach (var item in pairs)
                {
                    key_builder[item.Key] = $"return {_r2d_handler.AddValue(item.Value)};";
                }

                //根据快查字典生成快查代码
                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(ScriptKeyAction(key_builder));
                keyBuilder.Append("return default;");

                //创建快查方法
                nClass.Method(method =>
                {
                    method
                    .Param<TKey>("arg")
                    .Return<TValue>()
                    .Name("GetValueFromKey")
                    .Public()
                    .Static()
                    .Body(keyBuilder.ToString());
                });


                nClass
                   .BodyAppendLine(_r2d_handler.FieldsScript)
                   .BodyAppendLine(_r2d_handler.MethodScript);


                ProxyType = nClass.GetType();
                _r2d_handler.GetInitMethod(nClass)();
                GetValue = (delegate* managed<TKey, TValue>)(ProxyType.GetMethod("GetValueFromKey").MethodHandle.GetFunctionPointer());

            }

        }


        public TValue this[TKey key]
        {

            get
            {

                return GetValue(key);

            }

        }




        public void Dispose()
        {
            //GetValue = null;
            //GetKeys = null;
            ProxyType.DisposeDomain();
            //ProxyType = null;
        }

    }

}
