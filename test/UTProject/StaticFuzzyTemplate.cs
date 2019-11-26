using Natasha;
using System.IO;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("静态模板构造", "Fuzzy")]
    public class StaticFuzzyTemplate
    {

        TestStaticModel model;

        public StaticFuzzyTemplate()
        {
            model = new TestStaticModel();

        }

        [Fact(DisplayName = "模糊静态缓存测试1")]
        public void Test()
        {
            FDC<string> fdc = new FDC<string>();
            fdc = fdc | model.Model1 | BuilderModel<string, string>.Creator | OperatorModel<string, string>.Creator;
            Assert.Equal(@"fixed (char* c =  arg){
switch (*(ushort*)(c+0)){case 48:
0
break;
case 49:
1
break;
case 50:
2
break;
case 51:
3
break;
case 52:
4
break;
case 53:
5
break;
case 54:
6
break;
case 55:
7
break;
case 56:
8
break;
case 57:
9
break;
}}OperatorModel<String,String>.Creator =  BuilderModel<String,String>.Creator(arg);
return OperatorModel<String,String>.Creator(arg);
", fdc.ToString());
        }
    }
}
