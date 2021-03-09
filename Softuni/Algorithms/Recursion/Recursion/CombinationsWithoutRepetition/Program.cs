using System;

namespace CombinationsWithoutRepetition
{
    class Program
    {
        static void Main()
        {
            int numsRange = int.Parse(Console.ReadLine());
            int columns = int.Parse(Console.ReadLine());

            GenerateCombinations(numsRange, columns);
        }

        static void GenerateCombinations(int numsRange, int columns, int currentNum = 1, int currentColumn = 1, string output = "")
        {
            if (currentColumn > columns)
            {
                Console.WriteLine(output);
                return;
            }

            for (int i = currentNum; i <= numsRange; i++)
            {
                GenerateCombinations(numsRange, columns, i + 1, currentColumn + 1, $"{output}{i} ");
            }
        }
    }
}
