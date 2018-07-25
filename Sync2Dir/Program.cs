using Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sync2Dir
{
    class Program
    {
         private static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {
           
            Console.WriteLine("Let's sync two folders");
            Console.WriteLine("Enter full path to folder 1:");
            var folderA = Console.ReadLine();
            Console.WriteLine("Enter full path to folder 2:");
            var folderB = Console.ReadLine();

            var synchronizer = new Synchronizer(folderA, folderB);
            PathInfo.FileProcessed += Synchronizer_FileProcessed;
            sw.Start();

            Task.Run(async () =>
            {
                await synchronizer.Sync();
                Console.WriteLine($"Sync in {sw.Elapsed.ToString()}\nRe-run?");
            });


            while (Console.ReadLine().ToLower() == "y")
            {
                sw.Reset();
                Task.Run(async () =>
              {
                  await synchronizer.Sync();
                  Console.WriteLine($"Sync in {sw.Elapsed.ToString()}\nRe-run?");
              });
            }

            Console.WriteLine("ok let's finish it...");
            Console.ReadLine();
        }

        private static void Synchronizer_FileProcessed(object sender, EventArgs e)
        {

            Console.Write($"\rElapsed {sw.Elapsed.ToString()}");

        }
    }
}
