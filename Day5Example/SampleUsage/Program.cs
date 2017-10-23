using Day5Example;
using System;
namespace SampleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please provide a radius for the circle");
            uint radius = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine();

            Circle newCircle = new Circle(radius);
            Console.WriteLine("Radius: {0}", newCircle.Radius);
            Console.WriteLine("Area: {0}", newCircle.Area);
            Console.WriteLine("Circumference: {0}", newCircle.Circumference);

            // Keep console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}