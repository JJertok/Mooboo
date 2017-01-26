using MooBoo.Utilities;
using System;
using ToggleSandbox.Services;

namespace Mooboo.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logService = new LogService("f6483840a2f0906397dd00f8b420fa4f");

            //Console.WriteLine("Enter name for task:");
            //var description = Console.ReadLine();
            //Console.WriteLine("Is good task (y/n):");
            //var isGood = Console.ReadLine() == "y" ? true : false;

            //logService.Add(description, DateTime.Now, DateTime.Now.AddMinutes(15), isGood);
            //    SchedulerDemo();

            //  Console.ReadKey();
            LogServiceDemo();
            Console.ReadLine();
        }


        static void LogServiceDemo()
        {
            var logService = new LogService("f6483840a2f0906397dd00f8b420fa4f");

            logService.Add(new Log
            {
                Name = "chrome.exe",
                Start = DateTime.Now,
                Stop = DateTime.Now.AddMinutes(5)
            });

            logService.Add(new Log
            {
                Name = "calc.exe",
                Start = DateTime.Now.AddMinutes(5),
                Stop = DateTime.Now.AddMinutes(7)
            });



            logService.Add(new Log
            {
                Name = "chrome.exe",
                Start = DateTime.Now.AddMinutes(7),
                Stop = DateTime.Now.AddMinutes(10)
            });
        }
    

        static void SchedulerDemo()
        {
            var a = 0;
            Scheduler.Instance.Schedule("demo", TimeSpan.FromSeconds(1), () =>
            {
                Console.WriteLine("h");
            });
        }

    }
}
