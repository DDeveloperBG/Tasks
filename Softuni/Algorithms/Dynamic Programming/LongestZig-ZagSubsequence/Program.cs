using System;
using System.Linq;
using System.Collections.Generic;

namespace LongestZig_ZagSubsequence
{
    class Program
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[,] lengths = new int[2, numbers.Length];
            // Row 0 is for zig-zag which goes from hight to low (>)
            // Row 1 is for zig-zag which goes from low to hight (<)

            int[,] prevIndexes = new int[2, numbers.Length];
            int biggestLength = 0;
            int biggestLengthIndex = 0;
            int biggestLengthRow = 0;

            lengths[1, 0] = lengths[0, 0] = 1;
            prevIndexes[1, 0] = prevIndexes[0, 0] = -1;

            // Find paths and biggestLength
            for (int currentInd = 1; currentInd < numbers.Length; currentInd++)
            {
                int currentNum = numbers[currentInd];

                for (int prevInd = 0; prevInd < currentInd; prevInd++)
                {
                    int prevNum = numbers[prevInd];

                    if (prevNum < currentNum && lengths[1, prevInd] > lengths[0, currentInd])
                    {
                        lengths[0, currentInd] = lengths[1, prevInd];
                        prevIndexes[0, currentInd] = prevInd;
                    }
                    else if (prevNum > currentNum && lengths[0, prevInd] > lengths[1, currentInd])
                    {
                        lengths[1, currentInd] = lengths[0, prevInd];
                        prevIndexes[1, currentInd] = prevInd;
                    }
                }

                lengths[0, currentInd]++;
                lengths[1, currentInd]++;

                if (lengths[0, currentInd] > biggestLength)
                {
                    biggestLength = lengths[0, currentInd];
                    biggestLengthIndex = currentInd;
                    biggestLengthRow = 0;
                }
                else if (lengths[1, currentInd] > biggestLength)
                {
                    biggestLength = lengths[1, currentInd];
                    biggestLengthIndex = currentInd;
                    biggestLengthRow = 1;
                }
            }

            int[] rows = new int[biggestLength - 1];
            bool lastIsDown = biggestLengthRow == 1;

            // Set up rows order
            for (int i = biggestLength - 2; i >= 0; i--)
            {
                if (lastIsDown)
                {
                    lastIsDown = false;
                }
                else
                {
                    rows[i] = 1;
                    lastIsDown = true;
                }
            }

            Stack<int> zigZagElements = new Stack<int>();
            int currentRow = biggestLengthRow;
            int currentIndex = biggestLengthIndex;
            int count = biggestLength - 2;

            // Find longest path
            while (true)
            {
                zigZagElements.Push(numbers[currentIndex]);

                currentIndex = prevIndexes[currentRow, currentIndex];

                if (currentIndex == -1) break;

                currentRow = rows[count];
                count--;
            }

            Console.WriteLine(string.Join(' ', zigZagElements));
        }
    }
}