using System;
using System.Collections.Generic;
using System.Text;

namespace UTProject.Model
{
    public class TestModel
    {

        public Dictionary<string, string> Model1;
        public Dictionary<A, int> Model2;
        public Dictionary<int, A> Model3;


        public TestModel()
        {
            Model1 = new Dictionary<string, string>();
            Model2 = new Dictionary<A, int>();
            Model3 = new Dictionary<int, A>();
            for (int i = 0; i < 100; i++)
            {
                string value = i.ToString();
                Model1[value] = value;
                A model = new A();
                Model2[model] = i;
                Model3[i] = model;
            }
        }
    }

    public class A { }
}
