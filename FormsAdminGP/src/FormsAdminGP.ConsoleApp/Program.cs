using FormsAdminGP.Services;
using System;

namespace FormsAdminGP.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new LoggerService().LogToFileTest();
            Console.ReadKey();
        }
    }
}
