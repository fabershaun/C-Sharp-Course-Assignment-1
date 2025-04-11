using System;

namespace Ex01_01
{
    internal class BinarySeries
    {
        protected const int k_NumOfInputs = 4;
        protected const int k_BinaryNumberLength = 7;
        public static void GetInputFromUser(ref int[] numbers)
        {
            String newLine = Environment.NewLine;
            Console.WriteLine("Please enter 4 numbers, 7 digits each, in a binary format");

            for (int i = 0; i < k_NumOfInputs; i++)
            {
                // Check if the input is: 
                // 1- a number
                // 2- binary
                // 3- length of 7 digits
                String inputFromUser = Console.ReadLine();

                while (int.TryParse(inputFromUser, out numbers[i]) == false)
                {
                    Console.WriteLine(string.Format(
@"Invalid input.
Please enter 4 numbers, 7 digits each, in a binary format"));
                }
            }
        }

        public static void ConvertBinaryToDecimal(ref int[] numbers)
        {

            for (int i = 0; i < k_NumOfInputs; i++)
            {
                int decimalNumber = 0;

                for (int j = 0; j < k_BinaryNumberLength; j++)
                {
                    int digit = numbers[i] % 10;
                    digit = digit * (int)Math.Pow(2, j);

                    decimalNumber += digit;
                    numbers[i] /= 10;
                }

                numbers[i] = decimalNumber;
            }
        }

        public static void PrintNumbers(ref int[] numbers)
        {
            for (int i = 0; i < k_NumOfInputs; i++)
            {
                Console.WriteLine(numbers[i]);
            }
        }
    }
}
