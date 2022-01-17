using BenchmarkDotNet.Running;
using Natasha;
using System;

namespace BenchmarkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            NatashaInitializer.Preheating();
            BenchmarkRunner.Run<BenchmarkTest>();
            Console.ReadKey();
        }
    }
}
