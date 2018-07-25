using System;

namespace Sync2Dir
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Let's sync two folders");
            Console.WriteLine("Enter full path to folder 1:");
            var folderA = Console.ReadLine();
            Console.WriteLine("Enter full path to folder 2:");
            var folderB = Console.ReadLine();
        }
    }
}
