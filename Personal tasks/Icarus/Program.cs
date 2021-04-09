using System;
using System.Linq;
using System.Collections.Generic;

namespace Icarus
{
    class Program
    {
        static int planeSize;
        static int damage = 1;

        static readonly Func<int, int> right = (index) =>
        {
            index++;

            if (index == planeSize)
            {
                index = 0;
                damage++;
            }

            return index;
        };
        static readonly Func<int, int> left = (index) =>
        {
            index--;

            if (index == -1)
            {
                index = planeSize - 1;
                damage++;
            }

            return index;
        };

        static readonly Dictionary<string, Func<int, int>> directions = new Dictionary<string, Func<int, int>>()
        {
            { nameof(right), right },
            { nameof(left), left },
        };

        static void Main()
        {
            int[] plane = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int index = int.Parse(Console.ReadLine());
            string[] commandData = Console.ReadLine().Split();
            planeSize = plane.Length;

            while (commandData[0] != "Supernova")
            {
                Func<int, int> direction = directions[commandData[0]];
                int times = int.Parse(commandData[1]);

                for (int i = 0; i < times; i++)
                {
                    index = direction(index);
                    plane[index] -= damage;
                }

                commandData = Console.ReadLine().Split();
            }

            Console.WriteLine(string.Join(' ', plane));
        }
    }
}