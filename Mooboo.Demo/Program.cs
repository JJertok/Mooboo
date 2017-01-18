using Mooboo.ToggleWrapper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
