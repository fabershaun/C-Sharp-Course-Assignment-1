using System;

namespace Ex01_05
{
    internal class Program
    {
        public static void Main()
        {
            string input = NumberStatistics.GetInput();
            NumberStatistics.CalculateStatistics(input);
            NumberStatistics.PrintStatisticsResult();
        }
    }
}
