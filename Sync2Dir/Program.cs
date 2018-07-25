using Sync;
using System;
using System.Diagnostics;

namespace Sync2Dir
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            Console.WriteLine("Let's sync two folders");
            Console.WriteLine("Enter full path to folder 1:");
            var folderA = Console.ReadLine();
            Console.WriteLine("Enter full path to folder 2:");
            var folderB = Console.ReadLine();

            var synchronizer = new Synchronizer(folderA, folderB);   
            sw.Start();
            synchronizer.Sync();
            Console.WriteLine($"Sync in {sw.Elapsed.ToString()}\nRe-run?");

            while(Console.ReadLine().ToLower() == "y")
            {
                sw.Reset();
                synchronizer.Sync();
                Console.WriteLine($"Sync in {sw.Elapsed.ToString()}\nRe-run?");
            }

            Console.WriteLine("ok let's finish it...");
            Console.ReadLine();
        }
    }
}
