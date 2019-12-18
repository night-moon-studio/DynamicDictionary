using System;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("快速查找", "Precision")]
    public class PrecisionTest
    {
        TestModel model;
        PrecisionCache<string> Handler1;

        public PrecisionTest()
        {
            model = new TestModel();
            Handler1 = model.Model1.PrecisioTree();
        }

        [Fact(DisplayName = "精确查找测试1")]
        public void TestModel1()
        {
            foreach (var item in model.Model1)
            {
                Assert.Equal(item.Value, Handler1[item.Key]);
            }
        }

        [Fact(DisplayName = "空集合测试1")]
        public void TestModel2()
        {
            var model2= new TestModel();
            model2.Model1.Clear();
            var tempHandler = model2.Model1.PrecisioTree();
            Assert.Equal(default, tempHandler["1"]);
            Assert.Equal(default, tempHandler.GetKey("1"));
            Assert.Equal(default, tempHandler.GetValue("1"));
        }


    }
}
