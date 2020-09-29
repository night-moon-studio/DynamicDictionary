using Natasha.CSharp;
using RuntimeToDynamic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCache
{

    public unsafe abstract class DynamicCacheBuilder<TKey,TValue> : IDisposable
    {

        public Type ProxyType;
        public delegate* managed<TKey, TValue> GetValue;
        public delegate* managed<TValue, TKey[]> GetKeys;
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict);
        public abstract string ScriptValueAction(IDictionary<TValue, string> dict);

        public DynamicCacheBuilder(IDictionary<TKey, TValue> pairs, DyanamicCacheDirection queryDirection =  DyanamicCacheDirection.Both)
        {

            Func<Type,IntPtr> InitGetValuePtr = default;
            Func<Type, IntPtr> InitGetKeysPtr = default;

            var nClass = NClass.UseDomain(typeof(TKey).GetDomain())
                    .Public()
                    .Static()
                    .Unsafe();

            AnonymousRTD _r2d_handler = new AnonymousRTD();
            _r2d_handler.UseStaticReadonlyFields();

            if (queryDirection != DyanamicCacheDirection.ValueToKey)
            {

                var key_builder = new Dictionary<TKey, string>();
                foreach (var item in pairs)
                {
                    key_builder[item.Key] = $"return {_r2d_handler.AddValue(item.Value)};";
                }

                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(ScriptKeyAction(key_builder));
                keyBuilder.Append("return default;");

                
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

                InitGetValuePtr = (type) => {  return type.GetMethod("GetValueFromKey").MethodHandle.GetFunctionPointer(); };

            }
            


            if (queryDirection != DyanamicCacheDirection.KeyToValue)
            {

                var value_builder = new Dictionary<TValue, string>();                
                foreach (var item in pairs)
                {
                    if (!value_builder.ContainsKey(item.Value))
                    {
                        value_builder[item.Value] = $"return new {typeof(TKey).GetDevelopName()}[]{{{_r2d_handler.AddValue(item.Key)}";
                    }
                    else
                    {
                        value_builder[item.Value] += $",{_r2d_handler.AddValue(item.Key)}";
                    }
                }

                var temp_value_buidler = new Dictionary<TValue, string>();
                foreach (var item in value_builder)
                {

                    temp_value_buidler[item.Key] = item.Value + "};";

                }


                StringBuilder valueBuilder = new StringBuilder();
                valueBuilder.Append(ScriptValueAction(temp_value_buidler));
                valueBuilder.Append("return null;");


                nClass.Method(method =>
                {
                    method
                    .Param<TValue>("arg")
                    .Return<TKey[]>()
                    .Name("GetKeysFromValue")
                    .Public()
                    .Static()
                    .Body(valueBuilder.ToString());
                });

                InitGetKeysPtr = (type) => { return type.GetMethod("GetKeysFromValue").MethodHandle.GetFunctionPointer(); };

            }

            nClass
                    .BodyAppendLine(_r2d_handler.FieldsScript)
                    .BodyAppendLine(_r2d_handler.MethodScript);


            ProxyType = nClass.GetType();
            _r2d_handler.GetInitMethod(nClass)();

            if (InitGetValuePtr!=default)
	        {
                GetValue = (delegate* managed<TKey, TValue>)InitGetValuePtr(ProxyType);
            }
            if (InitGetKeysPtr != default)
            {
                GetKeys = (delegate* managed<TValue,TKey[]>)InitGetKeysPtr(ProxyType);
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

            ProxyType.DisposeDomain();
            ProxyType = null;
        }

    }

}
