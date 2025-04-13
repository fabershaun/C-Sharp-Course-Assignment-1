using System;

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
            Console.WriteLine("Please enter {0} binary numbers, each with exactly {1} digits:",
                Program.k_NumberOfInputs, Program.k_BinaryNumberLength);

            readUserInputs();
            analyzeAllNumbers();
            printDecimalValuesDescending();
            printStatistics();
        }

        private static void readUserInputs()
        {
            for (int i = 0; i < Program.k_NumberOfInputs; i++)
            {
                Console.Write("Enter binary number #{0}: ", i + 1);
                string userInput = Console.ReadLine();

                while (!isBinaryInputValid(userInput))
                {
                    Console.WriteLine("Invalid input. Please enter exactly {0} binary digits (0 or 1):", Program.k_BinaryNumberLength);
                    userInput = Console.ReadLine();
                }

                Number parsedNumber = new Number();
                parsedNumber.m_BinaryNumberString = userInput;
                parsedNumber.m_binaryFormat = int.Parse(userInput);

                s_InputNumbers[i] = parsedNumber;
            }
        }

        private static bool isBinaryInputValid(string i_BinaryString)
        {
            if (i_BinaryString.Length != Program.k_BinaryNumberLength)
            {
                return false;
            }

            for (int i = 0; i < i_BinaryString.Length; i++)
            {
                if (i_BinaryString[i] != '0' && i_BinaryString[i] != '1')
                {
                    return false;
                }
            }

            return true;
        }

        private static void analyzeAllNumbers()
        {
            for (int i = 0; i < s_InputNumbers.Length; i++)
            {
                analyzeSingleNumber(s_InputNumbers[i]);
            }
        }

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

                    if (currentConsecutiveOnes > maxConsecutiveOnes)
                    {
                        maxConsecutiveOnes = currentConsecutiveOnes;
                    }
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

        private static void printStatistics()
        {
            printAverageDecimalValue();
            printNumberWithLongestOnesSeries();
            printNumberWithMostOnes();
            printTotalOnesInAllNumbers();
            printTransitionsPerNumber();
        }

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

        private static void printNumberWithLongestOnesSeries()
        {
            Console.WriteLine("Number with the longest sequence of ones: {0} (length: {1})",
                s_NumberWithLongestOnesSequence.m_BinaryNumberString,
                s_NumberWithLongestOnesSequence.m_numOfOnesSeries);
        }

        private static void printNumberWithMostOnes()
        {
            Console.WriteLine("Number with the most ones: {0} (count: {1})",
                s_NumberWithMostOnes.m_BinaryNumberString,
                s_NumberWithMostOnes.m_numOfOnes);
        }

        private static void printTotalOnesInAllNumbers()
        {
            Console.WriteLine("Total number of ones in all inputs: {0}", s_TotalOnesAcrossAllNumbers);
        }

        private static void printTransitionsPerNumber()
        {
            Console.WriteLine();
            Console.WriteLine("Bit transitions per number:");

            for (int i = 0; i < s_InputNumbers.Length; i++)
            {
                Console.WriteLine("{0} => {1} transitions",
                    s_InputNumbers[i].m_BinaryNumberString,
                    s_InputNumbers[i].m_numOfTransitions);
            }
        }
    }
}
