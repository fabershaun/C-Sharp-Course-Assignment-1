using System;
using System.Text;
using System.Linq;

namespace Ex01_04
{
    internal class Program
    {
        // Required input length
        private const int k_InputLength = 12;

        // Analysis results
        private static bool s_IsPalindrome = true;
        private static bool s_IsStringOnlyDigits = false;
        private static bool s_IsNumberDividedBy3 = false;
        private static bool s_IsStringOnlyLetter = false;
        private static bool s_AscendingAlphabeticalOrder = false;
        private static int s_NumberOfCapitalLetters = 0;

        public static void Main()
        {
            string input = getInput();
            startAnalyzeInput(input);
            printAnalyzedResult();
        }

        private static string getInput()
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
                    isValid = true; // Input length is correct
                }

            } while (!isValid);

            return userInput;
        }

        private static void startAnalyzeInput(string i_Input)
        {
            isPalindrome(i_Input); // Check if the input is a palindrome

            s_IsStringOnlyDigits = i_Input.All(char.IsDigit); // Check if all characters are digits
            s_IsStringOnlyLetter = i_Input.All(char.IsLetter); // Check if all characters are letters

            if (s_IsStringOnlyDigits == true)
            {
                long stringToNumber = long.Parse(i_Input);
                s_IsNumberDividedBy3 = (stringToNumber % 3 == 0); // Check divisibility by 3
            }
            else if (s_IsStringOnlyLetter)
            {
                s_NumberOfCapitalLetters = i_Input.Count(char.IsUpper); // Count capital letters
                isAscendingAlphabeticalOrder(i_Input); // Check if letters are in ascending order
            }
        }

        private static void printAnalyzedResult()
        {
            StringBuilder outputMessage = new StringBuilder();

            outputMessage.Append("Is palindrome? ");
            handleIfPalindrome(outputMessage);
            handleIfInStringOnlyDigits(outputMessage);
            handleIfInStringOnlyLetters(outputMessage);

            Console.WriteLine(outputMessage.ToString());
        }

        private static void handleIfPalindrome(StringBuilder io_stringoutputMessage)
        {
            if (s_IsPalindrome == true)
            {
                io_stringoutputMessage.AppendLine("Yes");
            }
            else
            {
                io_stringoutputMessage.AppendLine("No");
            }
        }

        private static void handleIfInStringOnlyDigits(StringBuilder io_stringoutputMessage)
        {
            if (s_IsStringOnlyDigits == true)
            {
                io_stringoutputMessage.Append("Is divided by 3? ");

                if (s_IsNumberDividedBy3 == true)
                {
                    io_stringoutputMessage.AppendLine("Yes");
                }
                else
                {
                    io_stringoutputMessage.AppendLine("No");
                }
            }
        }

        private static void handleIfInStringOnlyLetters(StringBuilder io_stringoutputMessage)
        {
            if (s_IsStringOnlyLetter == true)
            {
                io_stringoutputMessage.Append("The number of capital letter in the string is: ");
                io_stringoutputMessage.AppendLine(s_NumberOfCapitalLetters.ToString());
                io_stringoutputMessage.Append("Is sorted in ascending alphabetical order? ");

                if (s_AscendingAlphabeticalOrder == true)
                {
                    io_stringoutputMessage.AppendLine("Yes");
                }
                else
                {
                    io_stringoutputMessage.AppendLine("No");
                }
            }
        }


        private static void isPalindrome(string i_InputString)
        {
            if (i_InputString.Length <= 1)
            {
                s_IsPalindrome = true; // Empty or single character is a palindrome
                return;
            }

            if (isSameLetter(i_InputString[0], i_InputString[i_InputString.Length - 1]) == false)
            {
                s_IsPalindrome = false; // Mismatch found, not a palindrome
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

            return isSameLetter; // Compare letters ignoring case
        }

        private static void isAscendingAlphabeticalOrder(string i_InputString)
        {
            s_AscendingAlphabeticalOrder = true;
            i_InputString = i_InputString.ToLower(); // Ignore case

            for (int i = 1; i < i_InputString.Length; i++)
            {
                if (i_InputString[i] < i_InputString[i - 1])
                {
                    s_AscendingAlphabeticalOrder = false; // Not ascending
                    break;
                }
            }
        }
    }
}
