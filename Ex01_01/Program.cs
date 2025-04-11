using System;
using System.Collections.Generic;

namespace Ex01_01
{
    internal class Program
    {
        //protected const int k_NumOfInputs = 4;
        struct NumberDetails
        {
            public int decimalFormat;
            public int numOfOnesSeries;
            public int numOfTransitions;
            public int numOfOnes;
        }
        Dictionary<string, NumberDetails> NumbersDictionary = new Dictionary<string, NumberDetails>();

        public static void Main()
        {

            string[] numbers = new string[k_NumOfInputs];
            BinarySeries.GetInputFromUser(ref numbers);

            //BinarySeries.AnalyzeNumbers(ref numbers);

            //BinarySeries.ConvertBinaryToDecimal(ref numbers);
            //Array.Sort(numbers);                                
            //Array.Reverse(numbers);                                
            //BinarySeries.PrintNumbersArray(ref numbers);
            //BinarySeries.PrintAverageValueDecimal(ref numbers);

        }
    }
}
