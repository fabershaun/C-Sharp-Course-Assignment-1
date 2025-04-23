using System;

namespace Ex01_02
{
    public class Program
    {
        private const int k_TreeHeight = 7;

        public static void Main()
        {
            PrintTree(k_TreeHeight);
        }

        public static void PrintTree(int i_Height)
        {
            int numberOfCharsInLine = 1; // Number of numbers printed in the current line (starts at 1)
            int numberOfSpaces = i_Height * 2 - 4; // Initial number of spaces for the first row
            int numberToStart = 1; // Starting number to print
            char rowLetter = 'A'; // Starting letter for the first row

            printTreeHelper(i_Height, numberOfCharsInLine, numberToStart, rowLetter, numberOfSpaces, numberOfSpaces);

        }

        private static void printTreeHelper(int i_Height, int i_NumberOfCharsInLine, int i_NumberToStart,
            char i_RowLetter, int i_NumberOfSpaces, int i_NumberOfSpacesOriginal)
        {
            if (i_Height == 2) // Base case: last two rows (tree "trunk")
            {
                if (i_NumberToStart == 10) // Reset number back to 1 after 9
                {
                    i_NumberToStart = 1;
                }

                for (int i = 0; i < 2; i++)
                {
                    Console.Write(i_RowLetter++); // Print letter and increment to next letter
                    Console.Write(new string(' ', i_NumberOfSpacesOriginal)); // Print fixed spaces
                    Console.WriteLine("|{0}|", i_NumberToStart); // Print tree trunk with number
                }

                return; // Stop recursion
            }

            Console.Write(i_RowLetter); // Print current row letter
            Console.Write(new string(' ', i_NumberOfSpaces)); // Print spaces between letter and numbers

            for (int i = 0; i < i_NumberOfCharsInLine; i++)
            {
                if (i_NumberToStart == 10) // Reset number back to 1 after 9
                {
                    i_NumberToStart = 1;
                }

                Console.Write(" {0}", i_NumberToStart++); // Print current number and increment
            }

            Console.WriteLine();

            // Recursive call to print the next row with updated parameters
            printTreeHelper(
                i_Height - 1,
                i_NumberOfCharsInLine + 2, // Increase numbers in next line by 2
                i_NumberToStart,
                (char)(i_RowLetter + 1), // Move to next letter
                i_NumberOfSpaces - 2, // Decrease spaces as the tree gets wider
                i_NumberOfSpacesOriginal); // Pass original number of spaces unchanged
        }
    }
}
