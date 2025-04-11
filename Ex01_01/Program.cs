using System;

namespace Ex01_01
{
    internal class Program
    {
        protected const int k_NumOfInputs = 4;
        public static void Main()
        {

            string[] numbers = new string[k_NumOfInputs];
            BinarySeries.GetInputFromUser(ref numbers);

            BinarySeries.AnalyzeNumbers(ref numbers);

            BinarySeries.ConvertBinaryToDecimal(ref numbers);
            Array.Sort(numbers);                                
            Array.Reverse(numbers);                                
            BinarySeries.PrintNumbersArray(ref numbers);
            BinarySeries.PrintAverageValueDecimal(ref numbers);

        }
    }
}
