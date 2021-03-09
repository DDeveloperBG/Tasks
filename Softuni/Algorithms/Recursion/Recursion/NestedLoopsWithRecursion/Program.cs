using System;

namespace NestedLoopsWithRecursion
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            Repeat(n);
        }

        static void Repeat(int count, int index = 1, string output = "")
        {
            if (index > count)
            {
                Console.WriteLine(output);
                return;
            }

            for (int i = 1; i <= count; i++)
            {
                Repeat(count, index + 1, output + i + ' ');
            }
        }
    }
}