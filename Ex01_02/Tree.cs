using System;

namespace Ex01_02
{
    public class Tree
    {
        public static void PrintTree(int i_Height)
        {
            int numberOfCharsInLine = 1;
            int numberOfSpaces = i_Height * 2 - 4;
            int numberToStart = 1;
            char rowLetter = 'A';

            printTreeHelper(i_Height, numberOfCharsInLine, numberToStart, rowLetter, numberOfSpaces, numberOfSpaces);

        }

        private static void printTreeHelper(int i_Height, int i_NumberOfCharsInLine, int i_NumberToStart,
            char i_RowLetter, int i_NumberOfSpaces, int i_NumberOfSpacesOriginal)
        {
            if (i_Height == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Write(i_RowLetter++);
                    Console.Write(new string(' ', i_NumberOfSpacesOriginal));
                    Console.WriteLine("|{0}|", i_NumberToStart);
                }

                return;
            }

            Console.Write(i_RowLetter);
            Console.Write(new string(' ', i_NumberOfSpaces));       // Printing spaces

            for (int i = 0; i < i_NumberOfCharsInLine; i++)
            {
                if (i_NumberToStart == 10)
                {
                    i_NumberToStart = 1;
                }

                Console.Write(" {0}", i_NumberToStart++);
            }

            Console.WriteLine();

            printTreeHelper(i_Height - 1, i_NumberOfCharsInLine + 2,
                i_NumberToStart, (char)(i_RowLetter + 1), i_NumberOfSpaces - 2, i_NumberOfSpacesOriginal);
        }

        public static int GetInputFromUser()
        {
            int input;
            bool isValid = false;

            do
            {
                Console.Write("Please enter the tree height: ");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out input) == true && CheckInput(input) == true)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            while (!isValid);

            return input;
        }

        private static bool CheckInput(int i_input)
        {
            return i_input >= 4 && i_input <= 15;
        }
    }
}


