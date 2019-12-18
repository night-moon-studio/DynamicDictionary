using System;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("快速查找", "Fuzzy")]
    public class FuzzyTest
    {
        TestModel model;
        FuzzyCache<string> Handler1;

        public FuzzyTest()
        {
            model = new TestModel();
            Handler1 = model.Model1.FuzzyTree();
        }

        [Fact(DisplayName = "模糊查找测试1")]
        public void TestModel1()
        {
            foreach (var item in model.Model1)
            {
                Assert.Equal(item.Value, Handler1[item.Key]);
            }
        }

        [Fact(DisplayName = "空集合测试1")]
        public void TestModel4()
        {
            var model2 = new TestModel();
            model2.Model1.Clear();
            var tempHandler = model2.Model1.FuzzyTree();
            Assert.Equal(default, tempHandler["1"]);
            Assert.Equal(default, tempHandler.GetKey("1"));
            Assert.Equal(default, tempHandler.GetValue("1"));
        }

    }
}
