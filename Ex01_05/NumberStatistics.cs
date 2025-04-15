using System;
using System.Linq;

namespace Ex01_05
{
    internal class NumberStatistics
    {
        protected const int k_InputLength = 8;
        protected static int s_NumberOfDigitsSmallerThanTheFirstDigit = 0;
        protected static int s_NumberOfDigitsDividedBy3 = 0;
        protected static int s_LargestDigit = 0;
        protected static int s_SmallestDigit = 9;
        protected static int s_DifferenceBetweenMaxAndMinDigit = 0;
        protected static int s_MostFrequentDigit;
        protected static int s_MostFrequentDigitCount;


        public static string GetInput()
        {
            bool isValid = true;
            string userInput;

            do
            {
                Console.Write("Please enter an {0} digits number: ", k_InputLength);
                userInput = Console.ReadLine();

                bool isStringOnlyDigits = userInput.All(char.IsDigit);

                if (userInput.Length != k_InputLength || isStringOnlyDigits == false)
                {
                    isValid = false;
                    Console.WriteLine("Invalid input. Please try again.");
                }

                else
                {
                    isValid = true;
                }

            } while (!isValid);

            return userInput;
        }

        public static void PrintStatisticsResult()
        {
            Console.WriteLine();
            Console.WriteLine("The number of digits that smaller than the first digit is: {0}", s_NumberOfDigitsSmallerThanTheFirstDigit);
            Console.WriteLine("The number of digits that divisible by 3 is: {0}", s_NumberOfDigitsDividedBy3);
            Console.WriteLine("The difference between the max and the min digits is: {0}", s_DifferenceBetweenMaxAndMinDigit);
            Console.WriteLine("The most frequent digit is: {0}. It appears {1} times", s_MostFrequentDigit, s_MostFrequentDigitCount);
        }

        public static void CalculateStatistics(string i_input)
        {
            findNumberOfDigitsSmallerThanTheFirstDigit(i_input);
            findNumberOfDigitsDividedBy3(i_input);
            findDifferenceBetweenMaxAndMinDigit(i_input);
            findMostFrequentDigit(i_input);
        }

        private static void findNumberOfDigitsSmallerThanTheFirstDigit(string i_input)
        {
            char firstChar = i_input[0];
            for(int i = 1; i < i_input.Length; i++)
            {
                if (i_input[i] < firstChar)
                {
                    s_NumberOfDigitsSmallerThanTheFirstDigit++;
                }
            }
        }

        private static void findNumberOfDigitsDividedBy3(string i_input)
        {
            foreach (char digitChar in i_input)
            {
                if ((digitChar - '0') % 3 == 0)
                {
                    s_NumberOfDigitsDividedBy3++;
                }
            }
        }

        private static void findDifferenceBetweenMaxAndMinDigit(string i_input)
        {
            findMaxAndMinDigits(i_input);
            s_DifferenceBetweenMaxAndMinDigit = s_LargestDigit - s_SmallestDigit;
        }

        private static void findMaxAndMinDigits(string i_input)
        {
            foreach (char digitChar in i_input)
            {
                int digitInt = digitChar - '0';
                if (digitInt > s_LargestDigit)
                {
                    s_LargestDigit = digitInt;
                }

                if (digitInt < s_SmallestDigit)
                {
                    s_SmallestDigit = digitInt;
                }
            }
        }

        private static void findMostFrequentDigit(string i_Input)
        {
            int maxCount = 0;
            char mostFrequentDigit = '0';

            for (char digit = '0'; digit <= '9'; digit++)
            {
                int currentDigitCount = 0;

                foreach (char currentChar in i_Input)
                {
                    if (currentChar == digit)
                    {
                        currentDigitCount++;
                    }
                }

                if (currentDigitCount > maxCount)
                {
                    maxCount = currentDigitCount;
                    mostFrequentDigit = digit;
                }
            }

            s_MostFrequentDigit = mostFrequentDigit - '0';
            s_MostFrequentDigitCount = maxCount;
        }

    }
}
