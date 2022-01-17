using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using ImTools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkProject
{

    [MemoryDiagnoser, MarkdownExporter, RPlotExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [CategoriesColumn]
    public class BenchmarkTest
    {
        public readonly TestModel model;
        public readonly DynamicDictionaryBase<string, string> FuzzyHandler;
        public readonly DynamicDictionaryBase<string, string> HashHandler;
        public readonly DynamicDictionaryBase<string, string> PrecisionHandler;
        public readonly DynamicDictionaryBase<int, string> HashCodePrecisionHandler;
        public readonly Dictionary<string, string> DictHandler;
        public readonly ConcurrentDictionary<string, string> ConDictHandler;
        public readonly ImmutableDictionary<string, string> ReadonlyDictHandler;
        public readonly ImMap<string> _imMap;
        public readonly ImHashMap<string, string> _imHashMap;
        public BenchmarkTest()
        {
            NatashaInitializer.Preheating();
            model = new TestModel();
            _imMap = ImMap<string>.Empty;
            _imHashMap = ImHashMap<string, string>.Empty;
            FuzzyHandler = model.Model1.FuzzyTree();
            HashHandler = model.Model1.HashTree();
            PrecisionHandler = model.Model1.PrecisioTree();
            DictHandler = model.Model1;
            Dictionary<int, string> hashDict = new();
            foreach (var item in model.Model1)
            {
                _imHashMap = _imHashMap.AddOrUpdate(item.Key, item.Value);
                _imMap = _imMap.AddOrUpdate(Int32.Parse(item.Key), item.Value);
                hashDict[Int32.Parse(item.Key)] = item.Value;
            }
            HashCodePrecisionHandler = hashDict.CustomerTree(item=>item.ToString());
            ConDictHandler = new ConcurrentDictionary<string, string>(model.Model1);
            ReadonlyDictHandler = ImmutableDictionary.CreateRange(DictHandler);
        }

        //[Benchmark(Description = "哈希查找树")]
        //public void TestHash()
        //{
        //    var result = HashHandler["11"];
        //    result = HashHandler["2"];
        //}

        //[Benchmark(Description = "模糊查找树")]
        //public void TestFuzzy()
        //{
        //    var result = FuzzyHandler["11"];
        //    result = FuzzyHandler["2"];
        //}

        [Benchmark(Description = "精确查找树")]
        public void TestPrecision()
        {
            var result = PrecisionHandler["11"];
            result = PrecisionHandler["2"];
        }

        [Benchmark(Description = "普通字典")]
        public void TestDict()
        {
            var result = DictHandler["11"];
            result = DictHandler["2"];
        }

        [Benchmark(Description = "CustomerSwitch")]
        public void TestCustomer()
        {
            var result = HashCodePrecisionHandler[11];
            result = HashCodePrecisionHandler[2];
        }


        [Benchmark(Description = "imMap")]
        public void TestImp()
        {
            _imMap.TryFind(11, out var result);
            _imMap.TryFind(2, out result);
        }
        [Benchmark(Description = "imHashMap")]
        public void TestHashImp()
        {
            _imHashMap.TryFind("11", out var result);
            _imHashMap.TryFind("2", out result);
        }

        //[Benchmark(Description = "并发字典")]
        //public void TestConDict()
        //{
        //    var result = ConDictHandler["11"];
        //    result = ConDictHandler["2"];
        //}

        //[Benchmark(Description = "只读字典")]
        //public void TestImmDict()
        //{
        //    var result = ReadonlyDictHandler["11"];
        //    result = ReadonlyDictHandler["2"];
        //}

    }
}
