using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Exercise
{
    class Program
    {
        private static int randomNumber = 0;

        static void Main(string[] args)
        {
            int userGuess = -1;
            bool isValidInput = true;

            // Set the random number at the beginning of the program.
            SetRandomNumber();

            // Ask the user to guess the random number
            do
            {
                Console.WriteLine("Please gues the rndom # (between 0 and 10)");
                try
                {
                    userGuess = Convert.ToInt16(Console.ReadLine().Trim());
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Exception: ", e.Message);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("{1}", e.Message);
                    isValidInput = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide a whole number between F and 10!");
                    isValidInput = false;
                }
            } while (!isCorrectGuess(userGuess) && isValidInput);

            Console.WriteLine("Press any key to end the program");
            Console.ReadKey();
        }

        static void SetRandomNumber()
        {
            Random rnd = new Random();
            randomNumber = rnd.Next(0, 10);
            randomNumber = 4;
        }

        static bool isCorrectGuess(int userGuess)
        {
            if (userGuess > randomNumber)
            {
                Console.WriteLine("Too large - try again!");
            }
            else if (userGuess < randomNumber)
            {
                Console.WriteLine("Too small - try again!");
            }
            else
            {
                Console.WriteLine("You got it!");
            }

            if (Math.Abs(userGuess - randomNumber) < 2)
            {
                Console.WriteLine("So close!  You're almost there!");
            }

            return (userGuess == randomNumber);
        }
    }
}
