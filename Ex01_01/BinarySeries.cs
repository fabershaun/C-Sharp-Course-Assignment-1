using System;
//using System.Collections.Generic;                   //Check!

namespace Ex01_01
{
    internal class BinarySeries
    {

        protected const int k_BinaryNumberLength = 7;

        public static void GetInputFromUser(ref string[] io_numbers)
        {
            Console.WriteLine("Please enter 4 numbers, 7 digits each, in a binary format");

            for (int i = 0; i < io_numbers.Length; i++)
            {
                string inputFromUser = Console.ReadLine();

                while (CheckInputFromUser(ref inputFromUser, ref io_numbers, i) == false)
                {
                    Console.WriteLine(string.Format(
@"Invalid input.
Please enter 4 numbers, 7 digits each, in a binary format"));

                    inputFromUser = Console.ReadLine();
                }
            }
        }

        public static bool CheckInputFromUser(ref string i_input, ref string[] io_numbers, int i_index)
        {
            bool validInput = true;
            int number;
            if (int.TryParse(i_input, out number) == false)      // Check if it's a valid number
            {
                validInput = false;
            }
            else if (CheckIfNumberIsBinary(i_input) == false)    // Check if the number is binary
            {
                validInput = false;
            }
            else if (i_input.Length != k_BinaryNumberLength)    // Check if the length is 7
            {
                validInput = false;
            }

            return validInput;
        }

        public static bool CheckIfNumberIsBinary(string io_numberToCheck)
        {
            bool isBinary = true;
            foreach (char currentCharInString in io_numberToCheck)
            {
                if (currentCharInString != '0' && currentCharInString != '1')
                {
                    isBinary = false;
                    break;
                }
            }
            return isBinary;
        }

        public static void ConvertBinaryToDecimal(ref int[] io_numbers)
        {
            for (int i = 0; i < io_numbers.Length; i++)
            {
                int decimalNumber = 0;

                for (int j = 0; j < k_BinaryNumberLength; j++)
                {
                    int digit = io_numbers[i] % 10;
                    digit = digit * (int)Math.Pow(2, j);

                    decimalNumber += digit;
                    io_numbers[i] /= 10;
                }

                io_numbers[i] = decimalNumber;
            }
        }

        public static void PrintNumbersArray(ref int[] io_numbers)
        {
            Console.WriteLine(Environment.NewLine + "The decimal values of the input numbers is: ");

            foreach (int number in io_numbers)
            {
                Console.WriteLine(number);
            } 
        }
        
        public static void PrintAverageValueDecimal(ref int[] io_numbers)
        {
            float averageOfNumbersArray = CalculateAverageValues(ref io_numbers);
            Console.WriteLine(Environment.NewLine + "The average of the input numbers is: " + averageOfNumbersArray);
        }

        public static float CalculateAverageValues(ref int[] io_numbers)
        {
            float sum = 0;

            foreach (int number in io_numbers)
            {
                sum += number;
            }
            return sum / io_numbers.Length;

        }

        public static void AnalyzeNumbers(ref int[] i_numbers)
        {
            foreach (int number in i_numbers)
            {

            }
        }
    }
}
