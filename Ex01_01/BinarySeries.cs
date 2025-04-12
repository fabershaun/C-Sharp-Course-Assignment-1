using System;
using System.Collections;
using System.Collections.Generic;                   //Check!

namespace Ex01_01
{
    internal class BinarySeries
    {
        public static Dictionary<string, NumberDetails> m_NumbersDictionary = new Dictionary<string, NumberDetails>();

        public static int m_LongestSeriesOfOnesInARow = 0;
        public static string m_NumberWithTheLongestSeriesOfOnesInRow;
        public static int m_TheBiggestTotalNumberOfOnes = 0;
        public static string m_NumberWithTheBiggestTotalNumberOfOnes;
        public static int m_TotalOnesInAllInputs = 0;


        protected const int k_BinaryNumberLength = 7;



        public static void Run(int i_numOfInputs)
        {
            Console.WriteLine("Please enter 4 numbers, 7 digits each, in a binary format");
            GetInputFromUser(i_numOfInputs);
            AnalyzeNumbers();
            PrintSortedNumbers();
            PrintStatistics();
        }

        public static void GetInputFromUser(int i_numOfInputs)
        {
            for (int i = 0; i < i_numOfInputs; i++)
            {
                string inputFromUser = Console.ReadLine();

                while (CheckInputFromUser(ref inputFromUser) == false)
                {
                    Console.WriteLine(string.Format(@"
Invalid input. Try again.
Please enter 7 digits in a binary format"));

                    inputFromUser = Console.ReadLine();
                }
                m_NumbersDictionary[inputFromUser] = new NumberDetails();
            }
        }

        public static bool CheckInputFromUser(ref string i_input)
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

        public static void AnalyzeNumbers()
        {
            foreach (string binaryNumberString in m_NumbersDictionary.Keys)
            {
                NumberDetails numberDetails = m_NumbersDictionary[binaryNumberString];

                int binaryNumberInt = int.Parse(binaryNumberString);
                numberDetails.binaryFormat = binaryNumberInt;

                int decimalNumber = 0;
                int numberOfOnesInRow = 0;
                int totalNumOfOnesInNumber = 0;
                int currentTransitionCount = 0;
                int maxTransitionCount = 0;
                int previousDigit = -1;

                for (int i = 0; i < k_BinaryNumberLength; i++)
                {
                    int currentBinaryDigit = binaryNumberInt % 10;
                    decimalNumber += ConvertBinaryDigitToDecimal(currentBinaryDigit, i);

                    if (currentBinaryDigit == 1)
                    {
                        numberOfOnesInRow++;
                        totalNumOfOnesInNumber++;
                        m_TotalOnesInAllInputs++;
                        handleTransitionCount(ref currentTransitionCount, ref maxTransitionCount, ref previousDigit, currentBinaryDigit);
                        
                    }

                    else // currentBinaryDigit == 0
                    {
                        numberOfOnesInRow = 0;
                        handleTransitionCount(ref currentTransitionCount, ref maxTransitionCount, ref previousDigit, currentBinaryDigit);
                    }

                    if (numberOfOnesInRow > m_LongestSeriesOfOnesInARow)
                    {
                        m_LongestSeriesOfOnesInARow = numberOfOnesInRow;
                        m_NumberWithTheLongestSeriesOfOnesInRow = binaryNumberString;
                    }

                    if (totalNumOfOnesInNumber > m_TheBiggestTotalNumberOfOnes)
                    {
                        m_TheBiggestTotalNumberOfOnes = totalNumOfOnesInNumber;
                        m_NumberWithTheBiggestTotalNumberOfOnes = binaryNumberString;
                    }

                    previousDigit = currentBinaryDigit;
                    binaryNumberInt /= 10;
                }

                numberDetails.decimalFormat = decimalNumber;
                numberDetails.numOfTransitions = maxTransitionCount;
            }
        }

        public static void handleTransitionCount(ref int currentTransitionCount, ref int maxTransitionCount, ref int previousDigit, int currentBinaryDigit)
        {
            if (currentBinaryDigit != previousDigit && previousDigit != -1)
            {
                currentTransitionCount++;
                if (currentTransitionCount > maxTransitionCount)
                {
                    maxTransitionCount = currentTransitionCount;
                }
            }
            else
            {
                currentTransitionCount = 0;
            }
        }


        public static int ConvertBinaryDigitToDecimal(int i_BinaryDigit, int i_power)
        {
            int decimalDigit = 0;

            if (i_BinaryDigit == 1)
            {
                i_BinaryDigit = (int)Math.Pow(2, i_power);
                decimalDigit = i_BinaryDigit;
            }

            return decimalDigit;
        }

        public static void PrintSortedNumbers()
        {
            List<KeyValuePair<string, NumberDetails>> sortedList = SortDictionary();

            Console.WriteLine(Environment.NewLine + "The decimal values of the input numbers are: ");

            foreach (var pair in sortedList)
            {
                Console.WriteLine(pair.Value.decimalFormat);
            } 
        }

        public static List<KeyValuePair<string, NumberDetails>>  SortDictionary()
        {
            List<KeyValuePair<string, NumberDetails>> sortedList = new List<KeyValuePair<string, NumberDetails>>(m_NumbersDictionary);
            sortedList.Sort((pair1, pair2) =>
                pair2.Value.decimalFormat.CompareTo(pair1.Value.decimalFormat));

            return sortedList;
        }


        private static void PrintStatistics()
        {
            PrintAverageValueDecimal();

            Console.WriteLine("The number with the longest series of ones in a row is: {0}. The length of its series is {1}",
                m_NumberWithTheLongestSeriesOfOnesInRow, m_LongestSeriesOfOnesInARow);

            PrintTransitionsPerNumber();
            
            Console.WriteLine("The number with the most ones: " + m_NumberWithTheBiggestTotalNumberOfOnes);
            Console.WriteLine("Total amount of ones in the input is: " + m_TotalOnesInAllInputs);
        }


        public static void PrintAverageValueDecimal()
        {
            float averageOfNumbersArray = CalculateAverageValues();
            Console.WriteLine(Environment.NewLine + "The average of the input numbers is: " + averageOfNumbersArray);
        }

        public static float CalculateAverageValues()
        {
            float sum = 0;

            foreach (string key in m_NumbersDictionary.Keys)
            {
                sum += m_NumbersDictionary[key].decimalFormat;
            }

            return sum / m_NumbersDictionary.Count;
        }

        private static void PrintTransitionsPerNumber()
        {
            foreach (string key in m_NumbersDictionary.Keys)
            {
                Console.WriteLine("The number of transitions for the number {0} is: {1}", key, m_NumbersDictionary[key].numOfTransitions);
            }
        }
    }
}
