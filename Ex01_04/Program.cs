using System;

namespace Ex01_04
{
    internal class Program
    {
        public static void Main()
        {
            string input = StringAnalyzer.GetInput();
            StringAnalyzer.StartAnalyzeInput(input);
            StringAnalyzer.PrintAnalyzedResult();
        }
    }
}
