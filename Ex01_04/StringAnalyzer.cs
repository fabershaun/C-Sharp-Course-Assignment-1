using System;
using System.Linq;

namespace Ex01_04
{
    internal class StringAnalyzer
    {
        protected const int k_InputLength = 12;
        protected static bool s_IsPalindrome = true;
        protected static bool s_IsStringOnlyDigits = false;
        protected static bool s_IsNumberDividedBy3 = false;
        protected static bool s_IsStringOnlyLetter = false;
        protected static bool s_AscendingAlphabeticalOrder = false;
        protected static int s_NumberOfCapitalLetters = 0;

        public static string GetInput()
        {
            bool isValid = false;
            string userInput;
            string startingMessage = string.Format("Please enter string in length of {0}: ", k_InputLength);

            do
            {
                Console.Write(startingMessage);
                userInput = Console.ReadLine();

                if (userInput.Length != k_InputLength)
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

        public static void StartAnalyzeInput(string i_Input)
        {
            isPalindrome(i_Input);

            s_IsStringOnlyDigits = i_Input.All(char.IsDigit);
            s_IsStringOnlyLetter = i_Input.All(char.IsLetter);

            if (s_IsStringOnlyDigits == true)
            {
                long stringToNumber = long.Parse(i_Input);
                s_IsNumberDividedBy3 = (stringToNumber % 3 == 0);
            }
            else if (s_IsStringOnlyLetter)
            {
                s_NumberOfCapitalLetters = i_Input.Count(char.IsUpper);
                isAscendingAlphabeticalOrder(i_Input);
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

        private static void isPalindrome(string i_InputString)
        {
            if (i_InputString.Length <= 1)
            {
                s_IsPalindrome = true;
                return;
            }

            if (isSameLetter(i_InputString[0], i_InputString[i_InputString.Length - 1]) == false)
            {
                s_IsPalindrome = false;
                return;
            }

            string trimmedString = i_InputString.Substring(1, i_InputString.Length - 2);
            isPalindrome(trimmedString);
        }

        private static bool isSameLetter(char i_Letter1, char i_Letter2)
        {
            bool isSameLetter = true;
            char lowerChar1, lowerChar2;

            lowerChar1 = char.ToLower(i_Letter1);
            lowerChar2 = char.ToLower(i_Letter2);

            if (lowerChar1 != lowerChar2)
            {
                isSameLetter = false;
            }

            return isSameLetter;
        }

        private static void isAscendingAlphabeticalOrder(string i_InputString)
        {
            s_AscendingAlphabeticalOrder = true;
            i_InputString = i_InputString.ToLower();

            for (int i = 1; i < i_InputString.Length; i++)
            {
                if (i_InputString[i] < i_InputString[i - 1])
                {
                    s_AscendingAlphabeticalOrder = false;
                    break;
                }
            }
        }
    }
}
