using System;
using System.Collections.Generic;
using System.Text;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("快速查找", "Hash")]
    public class HashTest
    {
        TestModel model;
        HashCache<string,string> HashHandler1;
        HashCache<A, int> HashHandler2;
        HashCache<int, A> HashHandler3;
        //HashCache<string,string>

        public HashTest()
        {
            model = new TestModel();
            HashHandler1 = model.Model1.HashTree();
            HashHandler2 = model.Model2.HashTree();
            HashHandler3 = model.Model3.HashTree();
        }

        [Fact(DisplayName = "Hash查找测试1")]
        public void TestModel1()
        {
            foreach (var item in model.Model1)
            {
                Assert.Equal(item.Value, HashHandler1[item.Key]);
            }
        }
        [Fact(DisplayName = "Hash查找测试2")]
        public void TestModel2()
        {
            foreach (var item in model.Model2)
            {
                Assert.Equal(item.Value, HashHandler2[item.Key]);
            }
        }
        [Fact(DisplayName = "Hash查找测试3")]
        public void TestModel3()
        {
            foreach (var item in model.Model3)
            {
                Assert.Equal(item.Value, HashHandler3[item.Key]);
            }
        }


        [Fact(DisplayName = "空集合测试1")]
        public void TestModel4()
        {
            var model2 = new TestModel();
            model2.Model1.Clear();
            var tempHandler = model2.Model1.HashTree();
            Assert.Equal(default, tempHandler["1"]);
            Assert.Equal(default, tempHandler.GetKey("1"));
            Assert.Equal(default, tempHandler.GetValue("1"));
        }
    }
}
