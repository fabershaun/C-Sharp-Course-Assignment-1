using System;
using System.Linq;

namespace Ex01_04
{
    internal class StringAnalyzer
    {
        protected static bool s_IsPalindrome = true;
        protected static bool s_IsStringOnlyDigits = false;
        protected static bool s_IsNumberDividedBy3 = false;
        protected static bool s_IsStringOnlyLetter = false;
        protected static int s_NumberOfCapitalLetters = 0;
        protected static bool s_AscendingAlphabeticalOrder = false;

        public static string GetInput()
        {
            bool isValid = false;
            string userInput;

            do
            {
                Console.Write("Please enter string in length of 12: ");
                userInput = Console.ReadLine();

                if (userInput.Length != 12)
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

        public static void StartAnalyzeInput(string input)
        {
            isPalindrome(input);

            s_IsStringOnlyDigits = input.All(char.IsDigit);
            s_IsStringOnlyLetter = input.All(char.IsLetter);

            if (s_IsStringOnlyDigits == true)
            {
                isNumberDividedBy3(input);
            }
            else if (s_IsStringOnlyLetter)
            {
                s_NumberOfCapitalLetters = input.Count(char.IsUpper);
                isAscendingAlphabeticalOrder(input);
            }
        }

        public static void PrintAnalyzedResult()
        {
            Console.WriteLine(s_IsPalindrome
                ? "The string is a palindrome."
                : "The string is not a palindrome.");

            if (s_IsStringOnlyDigits == true)
            {
                Console.WriteLine(s_IsNumberDividedBy3 == true
                    ? "The number is divided by 3."
                    : "The number is not divided by 3.");
            }
            else if (s_IsStringOnlyLetter == true)
            {
                Console.WriteLine("The number of capital letter in the string is: {0}", s_NumberOfCapitalLetters);
                Console.WriteLine(s_AscendingAlphabeticalOrder == true
                    ? "The string is sorted in ascending alphabetical order."
                    : "The string is not sorted in ascending alphabetical order.");
            }
        }

        private static void isPalindrome(string io_InputString)
        {
            if (io_InputString.Length <= 1)
            {
                s_IsPalindrome = true;
                return;
            }

            if (isSameLetter(io_InputString[0], io_InputString[io_InputString.Length - 1]) == false)
            {
                s_IsPalindrome = false;
                return;
            }

            string trimmedString = io_InputString.Substring(1, io_InputString.Length - 2);
            isPalindrome(trimmedString);
        }

        private static bool isSameLetter(char io_Letter1, char io_Letter2)
        {
            bool isSameLetter = true;
            char lowerChar1, lowerChar2;

            lowerChar1 = char.ToLower(io_Letter1);
            lowerChar2 = char.ToLower(io_Letter2);

            if (lowerChar1 != lowerChar2)
            {
                isSameLetter = false;
            }

            return isSameLetter;
        }

        private static void isNumberDividedBy3(string io_InputString)
        {
            int sum = 0;
            foreach (char digit in io_InputString)
            {
                sum += digit - '0';
            }

            s_IsNumberDividedBy3 = sum % 3 == 0;
        }

        private static void isAscendingAlphabeticalOrder(string io_InputString)
        {
            s_AscendingAlphabeticalOrder = true;
            io_InputString = io_InputString.ToLower();

            for (int i = 1; i < io_InputString.Length; i++)
            {
                if (io_InputString[i] < io_InputString[i - 1])
                {
                    s_AscendingAlphabeticalOrder = false;
                    break;
                }
            }
        }
    }
}
