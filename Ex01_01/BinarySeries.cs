using System;
using System.Text;

namespace Ex01_01
{

    internal enum InputValidationResult
    {
        Valid,
        InvalidLength,
        InvalidCharacter
    }

    internal class BinarySeries
    {
        private static Number[] s_InputNumbers = new Number[Program.k_NumberOfInputs]; // Stores user input numbers
        private static int s_TotalOnesAcrossAllNumbers = 0; // Sum of all '1' bits across all numbers
        private static Number s_NumberWithLongestOnesSequence = null; // Number with the longest sequence of consecutive '1's
        private static Number s_NumberWithMostOnes = null; // Number with the highest total '1's


        public static void startProcess()
        {
            string startingMessage = string.Format(
                "Please enter {0} binary numbers, each with exactly {1} digits:",
                Program.k_NumberOfInputs,
                Program.k_BinaryNumberLength);

            Console.WriteLine(startingMessage);

            readUserInputs();
            analyzeAllNumbers();
            printDecimalValuesDescending();
            printStatistics();
        }

        private static void readUserInputs()
        {
            for (int i = 0; i < Program.k_NumberOfInputs; i++)
            {
                string userInput = getValidBinaryInput(i);

                Number parsedNumber = new Number();
                parsedNumber.m_BinaryNumberString = userInput;
                parsedNumber.m_binaryFormat = int.Parse(userInput);

                s_InputNumbers[i] = parsedNumber;
            }
        }

        private static string getValidBinaryInput(int i_InputIndex)
        {
            string userInput;
            bool isValid;

            do
            {
                Console.Write("Enter binary number #{0}: ", i_InputIndex + 1);
                userInput = Console.ReadLine();

                InputValidationResult validationResult = validateBinaryInput(userInput);

                if (validationResult != InputValidationResult.Valid)
                {
                    printValidationError(validationResult); // Print the specific validation error
                    isValid = false;
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);

            return userInput;
        }

        private static InputValidationResult validateBinaryInput(string i_BinaryString)
        {
            if (i_BinaryString.Length != Program.k_BinaryNumberLength)
            {
                return InputValidationResult.InvalidLength;
            }

            for (int i = 0; i < i_BinaryString.Length; i++)
            {
                if (i_BinaryString[i] != '0' && i_BinaryString[i] != '1')
                {
                    return InputValidationResult.InvalidCharacter;
                }
            }

            return InputValidationResult.Valid;
        }

        private static void printValidationError(InputValidationResult i_ValidationResult)
        {
            switch (i_ValidationResult)
            {
                case InputValidationResult.InvalidLength:
                    Console.WriteLine(
                        "Invalid input length. Please enter exactly {0} binary digits.",
                        Program.k_BinaryNumberLength);
                    break;
                case InputValidationResult.InvalidCharacter:
                    Console.WriteLine("Invalid input. Only '0' and '1' are allowed.");
                    break;
            }
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
            int previousBit = -1; // -1 indicates no previous bit yet
            int currentPower = 0; // Exponent used for calculating decimal value

            for (int i = 0; i < Program.k_BinaryNumberLength; i++)
            {
                int currentBit = remainingBinaryValue % 10; // Extract the least significant digit (bit)

                if (currentBit == 1)
                {
                    decimalValue += (int)Math.Pow(2, currentPower); // Update decimal value by adding 2^currentPower
                    currentConsecutiveOnes++;
                    totalOnes++;

                    maxConsecutiveOnes = Math.Max(maxConsecutiveOnes, currentConsecutiveOnes); // Update maximum consecutive '1's if needed
                }
                else
                {
                    currentConsecutiveOnes = 0; // Reset counter if the bit is '0'
                }

                if (previousBit != -1 && previousBit != currentBit)
                {
                    transitionCount++; // Transition from 0→1 or 1→0
                }

                previousBit = currentBit;
                remainingBinaryValue /= 10; // Remove last digit
                currentPower++; // Move to next power of two
            }

            io_Number.m_decimalFormat = decimalValue;
            io_Number.m_numOfOnes = totalOnes;
            io_Number.m_numOfOnesSeries = maxConsecutiveOnes;
            io_Number.m_numOfTransitions = transitionCount;

            s_TotalOnesAcrossAllNumbers += totalOnes;

            // Update global statistics if this number has better metrics
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
            Number[] sortedNumbers = (Number[])s_InputNumbers.Clone(); // Clone so we don't modify the original array

            // Sort array by decimal value descending
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
