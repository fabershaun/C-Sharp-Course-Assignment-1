using System;
using System.Linq;
using System.Text;

namespace Ex01_05
{
    internal class NumberStatistics
    {
        private const int k_InputLength = 8;
        protected static int s_FirstDigit = -1;
        protected static int s_NumberOfDigitsSmallerThanTheFirstDigit = 0;
        protected static int s_NumberOfDigitsDividedBy3 = 0;
        protected static int s_LargestDigit = 0;
        protected static int s_SmallestDigit = 9;
        protected static int s_DifferenceBetweenMaxAndMinDigit = 0;
        protected static int s_MostFrequentDigit;
        protected static int s_MostFrequentDigitCount;

        protected static StringBuilder s_outputMessage = new StringBuilder();


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
            Console.WriteLine(s_outputMessage);
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
            s_FirstDigit = i_input[0] - '0';
            
            s_outputMessage.Append("The first digit is: ");
            s_outputMessage.Append(s_FirstDigit.ToString());
            s_outputMessage.Append(". The digits that smaller than the first digit are: ");

            for (int i = 1; i < i_input.Length; i++)
            {
                if (i_input[i] - '0' < s_FirstDigit)
                {
                    s_NumberOfDigitsSmallerThanTheFirstDigit++;
                    s_outputMessage.Append(i_input[i]);
                    s_outputMessage.Append(", ");
                }
            }

            if (s_NumberOfDigitsSmallerThanTheFirstDigit == 0)
            {
                s_outputMessage.Append("None");
            }
            else
            {
                s_outputMessage.Length -= 2; // Remove the last comma and space
            }

            s_outputMessage.Append(". Total: ");
            s_outputMessage.AppendLine(s_NumberOfDigitsSmallerThanTheFirstDigit.ToString());
        }

        private static void findNumberOfDigitsDividedBy3(string i_input)
        {
            s_outputMessage.Append("The digits which divisible by 3 are: ");

            foreach (char digitChar in i_input)
            {
                if ((digitChar - '0') % 3 == 0)
                {
                    s_NumberOfDigitsDividedBy3++;
                    s_outputMessage.Append(digitChar);
                    s_outputMessage.Append(", ");
                }
            }

            if(s_NumberOfDigitsDividedBy3 == 0)
            {
                s_outputMessage.Append("None");
            }
            else
            {
                s_outputMessage.Length -= 2;
            }

            s_outputMessage.Append(". ");
            s_outputMessage.Append("Total: ");
            s_outputMessage.AppendLine(s_NumberOfDigitsDividedBy3.ToString());
        }

        private static void findDifferenceBetweenMaxAndMinDigit(string i_input)
        {
            s_outputMessage.Append("The difference between the max and the min digits is: ");
            findMaxAndMinDigits(i_input);
            s_DifferenceBetweenMaxAndMinDigit = s_LargestDigit - s_SmallestDigit;
            s_outputMessage.AppendLine(s_DifferenceBetweenMaxAndMinDigit.ToString());
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

            s_outputMessage.Append("The most frequent digit is: ");

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

            s_outputMessage.Append(s_MostFrequentDigit.ToString());
            s_outputMessage.Append(" (Appears ");
            s_outputMessage.Append(s_MostFrequentDigitCount.ToString());
            s_outputMessage.Append(" times).");
        }

    }
}
