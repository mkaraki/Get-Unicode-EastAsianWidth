using System;
using Get_Unicode_EastAsianWidth;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EAWCheck.IsFullWidth('良'));
            Console.WriteLine(EAWCheck.IsFullWidth('g'));

            Console.ReadKey();
        }
    }
}
