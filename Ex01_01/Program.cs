using System;

namespace Ex01_01
{
    internal class Program
    {
        public static void Main()
        {
            int[] numbers = new int[4];
            BinarySeries.GetInputFromUser(ref numbers);
            BinarySeries.ConvertBinaryToDecimal(ref numbers);
            Array.Sort(numbers);                                //TODO: check if ok
            Array.Reverse(numbers);                                //TODO: check if ok
            BinarySeries.PrintNumbers(ref numbers); 

        }
    }
}
