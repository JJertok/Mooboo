using System;
using ToggleSandbox.Services;

namespace Mooboo.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logService = new LogService("f6483840a2f0906397dd00f8b420fa4f");

            Console.WriteLine("Enter name for task:");
            var description = Console.ReadLine();
            Console.WriteLine("Is good task (y/n):");
            var isGood = Console.ReadLine() == "y" ? true : false;

            logService.Add(description, DateTime.Now, DateTime.Now.AddMinutes(15), isGood);

        }
    }
}
