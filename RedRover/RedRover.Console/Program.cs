using RedRover.Services.Services;
using System;

namespace RedRover.Client.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Ready...");

        var service = new NodeService();
        string rawInput = "";

        while (true) {

            System.Console.Write("Use sample input? (y/n): ");
            string? defaultChoice = System.Console.ReadLine()?.Trim().ToLower();
            if (defaultChoice != "y")
            {
                System.Console.Write("Please Input Alternative (Invalid Input Reverts to Default): ");
                string? input = System.Console.ReadLine()?.Trim().ToLower();
            }
            else
                rawInput = NodeService.DEFAULT_INPUT;

            System.Console.WriteLine("");
            System.Console.WriteLine("Unsorted");
            System.Console.WriteLine("----------------------------------------------------------");
            System.Console.WriteLine(service.ReformatString(rawInput));
            System.Console.WriteLine("");
            System.Console.WriteLine("Alphabetical");
            System.Console.WriteLine("----------------------------------------------------------");
            System.Console.WriteLine(service.ReformatString(rawInput, true));


            System.Console.Write("Test another? (y/n): ");
            string? continueChoice = System.Console.ReadLine()?.Trim().ToLower();
            if (continueChoice != "y")
            {
                System.Console.WriteLine("Goodbye!");
                break; 
            }
        }
    }
}

