using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkProject
{
    public class TestModel
    {
        public Dictionary<string, string> Model1;


        public TestModel()
        {
            Model1 = new Dictionary<string, string>();
            for (int i = 0; i < 100; i++)
            {
                string value = i.ToString();
                Model1[value] = value;
            }
        }
    }
}
