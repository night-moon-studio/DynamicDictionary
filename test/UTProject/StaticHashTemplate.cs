using Natasha;
using System.IO;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("静态模板构造", "Fuzzy")]
    public class StaticHashTemplate
    {

        TestStaticModel model;

        public StaticHashTemplate()
        {
            model = new TestStaticModel();

        }

        [Fact(DisplayName = "Hash静态缓存测试1")]
        public void Test()
        {
            HDC<A, string> hdc = new HDC<A, string>();

            hdc = hdc | model.Model2 | BuilderModel<A, string>.Creator | OperatorModel<A, string>.Creator;

            //File.WriteAllText("1.txt", hdc.ToString());
            //Assert.Equal("", hdc.ToString());
        }
    }
}
