using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        public static event Func<int, Task> a;
        static void Main(string[] args)
        {
            a += Program_a;
            NatashaInitializer.Preheating();
            var domainName = Test();
            for (int i = 0; i < 6; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            Console.WriteLine(domainName);
            Console.WriteLine(DomainManagement.IsDeleted(domainName));
            Console.ReadKey();
        }
        public static async Task b()
        {
            await a(1);
        }
        private static async Task Program_a(int arg)
        {
            await Task.Delay(100);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public unsafe static string Test()
        {
            var dict2 = new Dictionary<int,string>();
            var dict = new Dictionary<string, int>();
            for (int i = 0; i < 10; i++)
            {

                dict[i.ToString()] = i;
                dict2[i] = i.ToString();

            }
            var test = dict2.CustomerTree(item => item.ToString());


            var temp = dict.HashTree();
            var name = temp.GetType().Name;
            var result = temp["1"];
            //var a = temp.GetKeys(1);
            //var domainName = temp.ProxyType.GetDomain().Name;
            Console.WriteLine(DomainManagement.IsDeleted(name));
           // temp.Dispose();
            for (int i = 0; i < 6; i++)
            {
                GC.Collect();
            }
            return name;
        }
    }
}
