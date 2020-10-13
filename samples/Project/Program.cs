using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            NatashaInitializer.InitializeAndPreheating();
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


        [MethodImpl(MethodImplOptions.NoInlining)]
        public unsafe static string Test()
        {
            var dict = new Dictionary<string, int>();
            for (int i = 0; i < 10; i++)
            {

                dict[i.ToString()] = i;

            }
            var temp = dict.HashTree();
            var result = temp["1"];
            var a = temp.GetKeys(1);
            var domainName = temp.ProxyType.GetDomain().Name;
            Console.WriteLine(DomainManagement.IsDeleted(domainName));
            temp.Dispose();
            for (int i = 0; i < 6; i++)
            {
                GC.Collect();
            }
            return domainName;
        }
    }
}
