using System;
using Ex01_02;

namespace Ex01_03
{
    internal class Program
    {
        public static void Main()
        {
            Ex01_02.Program.PrintTree(getInputFromUser());
        }

        private static int getInputFromUser()
        {
            int input;
            bool isValid = false;

            do
            {
                Console.Write("Please enter the tree height: ");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out input) == true && checkInput(input) == true)
                {
                    isValid = true; // Input is valid number and in range
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            while (isValid == false);

            return input; // Return the valid input
        }

        private static bool checkInput(int i_input)
        {
            return i_input >= 4 && i_input <= 15; // Valid height must be between 4 and 15
        }
    }
}