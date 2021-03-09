using System;
using System.Linq;

namespace ReverseArray
{
    class Program
    {
        static void Main()
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            ReverseArrayAndPrint(array);
        }

        static void ReverseArrayAndPrint(int[] array, int index = 0)
        {
            // Stop
            if (index == array.Length / 2)
            {
                Console.WriteLine(string.Join(" ", array));
                return;
            }

            // Change places
            int backUp = array[index];
            array[index] = array[array.Length - index - 1];
            array[array.Length - index - 1] = backUp;

            // Call for next
            ReverseArrayAndPrint(array, index + 1);
        }
    }
}