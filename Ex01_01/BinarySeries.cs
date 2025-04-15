using System;
using System.Text;

namespace Ex01_01
{
    internal class BinarySeries
    {
        private static Number[] s_InputNumbers = new Number[Program.k_NumberOfInputs];
        private static int s_TotalOnesAcrossAllNumbers = 0;
        private static Number s_NumberWithLongestOnesSequence = null;
        private static Number s_NumberWithMostOnes = null;

        
        public static void Run()
        {
            string startingMessage = string.Format("Please enter {0} binary numbers, each with exactly {1} digits:", Program.k_NumberOfInputs, Program.k_BinaryNumberLength);

            Console.WriteLine(startingMessage);

            readUserInputs();
            analyzeAllNumbers();
            printDecimalValuesDescending();
            printStatistics();
        }

        /// The method reads the user input and checks if it is valid.
        private static void readUserInputs()
        {
            for (int i = 0; i < Program.k_NumberOfInputs; i++)
            {
                bool isValid = true;
                string userInput;
                string invalidInputMessage = string.Format("Invalid input. Please enter exactly {0} binary digits (0 or 1). ", Program.k_BinaryNumberLength);

                do
                {
                    Console.Write("Enter binary number #{0}: ", i + 1);
                    userInput = Console.ReadLine();
                    isValid = true;

                    if (isBinaryInputValid(userInput) == false)
                    {
                        Console.WriteLine(invalidInputMessage);
                        isValid = false;
                    }
                } while (isValid == false);

                Number parsedNumber = new Number();
                parsedNumber.m_BinaryNumberString = userInput;
                parsedNumber.m_binaryFormat = int.Parse(userInput);

                s_InputNumbers[i] = parsedNumber;
            }
        }

        /// The method checks if the input is valid.
        private static bool isBinaryInputValid(string i_BinaryString)
        {
            bool isValid = true;

            if (i_BinaryString.Length != Program.k_BinaryNumberLength)
            {
                isValid = false;
            }

            for (int i = 0; i < i_BinaryString.Length; i++)
            {
                if (i_BinaryString[i] != '0' && i_BinaryString[i] != '1')
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        /// The method analyzes all the numbers and calculates their statistics.
        private static void analyzeAllNumbers()
        {
            for (int i = 0; i < s_InputNumbers.Length; i++)
            {
                analyzeSingleNumber(s_InputNumbers[i]);
            }
        }

        /// The method analyzes a single number and calculates its statistics.
        private static void analyzeSingleNumber(Number io_Number)
        {
            int remainingBinaryValue = io_Number.m_binaryFormat;
            int decimalValue = 0;
            int maxConsecutiveOnes = 0;
            int currentConsecutiveOnes = 0;
            int totalOnes = 0;
            int transitionCount = 0;
            int previousBit = -1;
            int currentPower = 0;

            for (int i = 0; i < Program.k_BinaryNumberLength; i++)
            {
                int currentBit = remainingBinaryValue % 10;

                if (currentBit == 1)
                {
                    decimalValue += (int)Math.Pow(2, currentPower);
                    currentConsecutiveOnes++;
                    totalOnes++;

                    maxConsecutiveOnes = Math.Max(maxConsecutiveOnes, currentConsecutiveOnes);
                }

                else
                {
                    currentConsecutiveOnes = 0;
                }
                if (previousBit != -1 && previousBit != currentBit)
                {
                    transitionCount++;
                }

                previousBit = currentBit;
                remainingBinaryValue /= 10;
                currentPower++;
            }

            io_Number.m_decimalFormat = decimalValue;
            io_Number.m_numOfOnes = totalOnes;
            io_Number.m_numOfOnesSeries = maxConsecutiveOnes;
            io_Number.m_numOfTransitions = transitionCount;

            s_TotalOnesAcrossAllNumbers += totalOnes;

            if (s_NumberWithLongestOnesSequence == null ||
                maxConsecutiveOnes > s_NumberWithLongestOnesSequence.m_numOfOnesSeries)
            {
                s_NumberWithLongestOnesSequence = io_Number;
            }

            if (s_NumberWithMostOnes == null ||
                totalOnes > s_NumberWithMostOnes.m_numOfOnes)
            {
                s_NumberWithMostOnes = io_Number;
            }
        }

        /// The method prints the decimal values of the binary numbers in descending order.
        private static void printDecimalValuesDescending()
        {
            Number[] sortedNumbers = (Number[])s_InputNumbers.Clone();

            for (int i = 0; i < sortedNumbers.Length - 1; i++)
            {
                int maxIndex = i;

                for (int j = i + 1; j < sortedNumbers.Length; j++)
                {
                    if (sortedNumbers[j].m_decimalFormat > sortedNumbers[maxIndex].m_decimalFormat)
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    Number temp = sortedNumbers[i];
                    sortedNumbers[i] = sortedNumbers[maxIndex];
                    sortedNumbers[maxIndex] = temp;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Decimal values of the binary numbers (sorted descending):");

            for (int i = 0; i < sortedNumbers.Length; i++)
            {
                Console.WriteLine("{0} => {1}", sortedNumbers[i].m_BinaryNumberString, sortedNumbers[i].m_decimalFormat);
            }
        }

        /// The method prints the statistics of the binary numbers.
        private static void printStatistics()
        {
            printAverageDecimalValue();
            printNumberWithLongestOnesSeries();
            printNumberWithMostOnes();
            printTotalOnesInAllNumbers();
            printTransitionsPerNumber();
        }

        /// The method calculates and prints the average decimal value of the binary numbers.
        private static void printAverageDecimalValue()
        {
            int sumOfDecimals = 0;

            for (int i = 0; i < s_InputNumbers.Length; i++)
            {
                sumOfDecimals += s_InputNumbers[i].m_decimalFormat;
            }

            float average = (float)sumOfDecimals / s_InputNumbers.Length;

            Console.WriteLine();
            Console.WriteLine("Average decimal value: {0}", average);
        }

        /// The method prints the number with the longest sequence of ones and its length.
        private static void printNumberWithLongestOnesSeries()
        {
            Console.WriteLine("Number with the longest sequence of ones: {0} (length: {1})",
                s_NumberWithLongestOnesSequence.m_BinaryNumberString,
                s_NumberWithLongestOnesSequence.m_numOfOnesSeries);
        }

        /// The method prints the number with the most ones and its count.
        private static void printNumberWithMostOnes()
        {
            Console.WriteLine("Number with the most ones: {0} (count: {1})",
                s_NumberWithMostOnes.m_BinaryNumberString,
                s_NumberWithMostOnes.m_numOfOnes);
        }

        /// The method prints the total number of ones in all binary numbers.
        private static void printTotalOnesInAllNumbers()
        {
            Console.WriteLine("Total number of ones in all inputs: {0}", s_TotalOnesAcrossAllNumbers);
        }

        /// The method prints the number of transitions in each binary number.
        private static void printTransitionsPerNumber()
        {
            StringBuilder outputMessage = new StringBuilder();

            outputMessage.AppendLine();
            outputMessage.AppendLine("Bit transitions per number:");

            for (int i = 0; i < s_InputNumbers.Length; i++)
            {
                string binaryString = s_InputNumbers[i].m_BinaryNumberString;
                int transitions = s_InputNumbers[i].m_numOfTransitions;

                string line = string.Format("{0} => {1} transitions", binaryString, transitions);

                outputMessage.AppendLine(line);
            }

            Console.WriteLine(outputMessage.ToString());
        }
    }
}
