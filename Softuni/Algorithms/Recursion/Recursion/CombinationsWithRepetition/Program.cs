using System;

namespace CombinationsWithRepetition
{
    class Program
    {
        static void Main()
        {
            int maxNum = int.Parse(Console.ReadLine());
            int columns = int.Parse(Console.ReadLine());

            GenerateCombinationsAndPrint(maxNum, columns);

        }

        static void GenerateCombinationsAndPrint(int maxNum, int columns, int lastNum = 1, int column = 1, string result = "")
        {
            if (column > columns)
            {
                Console.WriteLine(result);
                return;
            }

            for (int number = lastNum; number <= maxNum; number++)
            {
                GenerateCombinationsAndPrint(maxNum, columns, number, column + 1, result + number + ' ');
            }
        }
    }
}
