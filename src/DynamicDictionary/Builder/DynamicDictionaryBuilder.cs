using Natasha.CSharp;
using System;
using System.Collections.Generic;

namespace DynamicDictionary
{

    public abstract class DynamicDictionaryBuilder<TKey, TValue>
    {

        private static readonly string _prefix = "_anonymous_";
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict, string paramName);

        public readonly DynamicDictionaryBase<TKey, TValue> Instance;

        public DynamicDictionaryBuilder(IDictionary<TKey, TValue> pairs)
        {

            int count = 0;
            var nClass = NClass
#if NETCOREAPP3_1_OR_GREATER
                 .RandomDomain()
#else
                .DefaultDomain()
#endif

                        .Access("public sealed")
                        .InheritanceAppend<DynamicDictionaryBase<TKey, TValue>>()
                        .Unsafe();

#if NET5_0_OR_GREATER
            nClass.SkipInit();
#endif




            //构建快查字典 给BTF使用
            var getValueMethodScript = new Dictionary<TKey, string>();
            var tryGetValueMethodScript = new Dictionary<TKey, string>();
            var setValueMethodSciprt = new Dictionary<TKey, string>();
            foreach (var item in pairs)
            {
                count += 1;
                string field = _prefix + count;
                nClass.PrivateReadonlyField<TValue>(field);

                getValueMethodScript[item.Key] = $"return {field};";
                tryGetValueMethodScript[item.Key] = $"value = {field};return true;";
                setValueMethodSciprt[item.Key] = $"{field.ToReadonlyScript()} = value;return;";
            }

            //根据快查字典生成快查代码
            //value GetValue(key)
            nClass.Method(method =>
            {
                method
                .Param<TKey>("key")
                .Override()
                .Return<TValue>()
                .Name("GetValue")
                .Public()
                .BodyAppend(ScriptKeyAction(getValueMethodScript, "key"))
                .BodyAppend("return default;");
            });

            //bool TryGetValue(key,out value)
            nClass.Method(method =>
                {
                    method
                    .Param<TKey>("key")
                    .Param<TValue>("value", "out ")
                    .Override()
                    .Return<bool>()
                    .Name("TryGetValue")
                    .Public()
                    .BodyAppend(ScriptKeyAction(tryGetValueMethodScript, "key"))
                    .BodyAppend("value=default; return false;");
                });

            //Change(key,value)
            nClass.Method(method =>
                {
                    method
                    .Param<TKey>("key")
                    .Param<TValue>("value")
                    .Override()
                    .Name("Change")
                    .Public()
                    .BodyAppend(ScriptKeyAction(setValueMethodSciprt, "key"))
                    .BodyAppend("throw new Exception(\"Can't find key!\");");
                });


            var ProxyType = nClass.GetType();

            Instance = nClass
                .DelegateHandler
                .Func<DynamicDictionaryBase<TKey, TValue>>($"return new {ProxyType.GetDevelopName()}();")();


            foreach (var item in pairs)
            {
                Instance.Change(item.Key, item.Value);
            }

        }

    }

}
