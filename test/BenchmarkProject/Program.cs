using BenchmarkDotNet.Running;
using Natasha;
using System;

namespace BenchmarkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            NatashaInitializer.InitializeAndPreheating();
            BenchmarkRunner.Run<BenchmarkTest>();
            Console.ReadKey();
        }
    }
}
