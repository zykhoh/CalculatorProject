using Service;
using System;

namespace ConsoleApplicationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {   
                Console.WriteLine("Insert the mathematical expression:");
                string input = Console.ReadLine();

                ICalculatorService calculatorService = new CalculatorService();
                Console.WriteLine($"Ans: {calculatorService.Calculate(input)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
