using System.Collections.Generic;

namespace UTProject.Model
{
    public class TestStaticModel
    {
        public Dictionary<string, string> Model1;
        public Dictionary<A, string> Model2;
        public TestStaticModel()
        {
            Model1 = new Dictionary<string, string>();
            Model2 = new Dictionary<A, string>();
            for (int i = 0; i < 10; i++)
            {
                string value = i.ToString();
                Model1[value] = value;
                A model = new A();
                Model2[model] = value;
            }
        }
    }

}
