using Natasha.CSharp;
using Natasha.RuntimeToDynamic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicCache
{
    public abstract class DynamicCacheBuilder<TKey,TValue> : IDisposable
    {

        public readonly Func<TKey, TValue> GetValue;
        public readonly Func<TValue, TKey[]> GetKeys;
        public abstract string ScriptKeyAction(IDictionary<TKey, string> dict);
        public abstract string ScriptValueAction(IDictionary<TValue, string> dict);

        public DynamicCacheBuilder(IDictionary<TKey, TValue> pairs, DyanamicCacheDirection queryDirection =  DyanamicCacheDirection.Both)
        {

            


            if (queryDirection != DyanamicCacheDirection.ValueToKey)
            {

                AnonymousRTD _r2d_handler = AnonymousRTD.UseDomain(typeof(TKey).GetDomain());
                var key_builder = new Dictionary<TKey, string>();
                foreach (var item in pairs)
                {
                    key_builder[item.Key] = $"return {_r2d_handler.AddValue(item.Value)};";
                }
                

                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(ScriptKeyAction(key_builder));
                keyBuilder.Append("return default;");


                var method = typeof(Func<TKey, TValue>).GetMethod("Invoke");
                _r2d_handler.BodyAppend(FakeMethodOperator.RandomDomain()
                    .UseMethod(method)
                    .Unsafe()
                    .StaticMethodBody(keyBuilder.ToString())
                    .Script);
                var type = _r2d_handler.Complie();


                GetValue = NDelegate
                    .UseDomain(typeof(TKey).GetDomain())
                    .Func<Func<TKey, TValue>>($"return {_r2d_handler.TypeName}.Invoke;", type)();

            }
            


            if (queryDirection != DyanamicCacheDirection.KeyToValue)
            {

                AnonymousRTD _r2d_handler = AnonymousRTD.UseDomain(typeof(TKey).GetDomain());
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


                var method = typeof(Func<TValue, TKey[]>).GetMethod("Invoke");
                _r2d_handler.Body(FakeMethodOperator.RandomDomain()
                    .UseMethod(method)
                    .Unsafe()
                    .StaticMethodBody(valueBuilder.ToString())
                    .Script);
                var type = _r2d_handler.Complie();


                GetKeys = NDelegate
                    .UseDomain(typeof(TValue).GetDomain())
                    .Func<Func<TValue, TKey[]>>($"return {_r2d_handler.TypeName}.Invoke;", type)();

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
            GetKeys.DisposeDomain();
            GetKeys.DisposeDomain();
        }
    }
}
