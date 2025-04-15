using System;
using Ex01_02;

namespace ex3
{
    internal class Program
    {
        public static void Main()
        {
            int height = Tree.GetInputFromUser();
            Tree.PrintTree(height);
        }
    }
}