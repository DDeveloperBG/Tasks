using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SoftJump
{
    class SoftJump
    {
        static void Main()
        {
            // Initialization of constants from the task
            const char PlayerSign = 'S';
            const char PlatformSign = '-';
            const char EmptySpaceSign = '0';

            // Reading first line of input
            int[] firstLineInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int fieldRows = firstLineInput[0], fieldColumns = firstLineInput[1];

            int playerRow = -1, playerColumn = -1;
            char[,] field = new char[fieldRows, fieldColumns];

            // Reading the field
            for (int row = 0; row < fieldRows; row++)
            {
                string inputRow = Console.ReadLine();

                for (int column = 0; column < fieldColumns; column++)
                {
                    field[row, column] = inputRow[column];

                    if (inputRow[column] == PlayerSign)
                    {
                        playerRow = row;
                        playerColumn = column;
                    }
                }
            }

            int commandsCount = int.Parse(Console.ReadLine());
            int[,] commands = new int[commandsCount, 2];

            // Reading the commands for platforms movement
            for (int i = 0; i < commandsCount; i++)
            {
                int[] command = Console.ReadLine().Split().Select(int.Parse).ToArray();

                commands[i, 0] = command[0];
                commands[i, 1] = command[1];
            }

            int jumpsCount = 0;

            // Moving platforms and if possible player
            for (int i = 0; i < commandsCount; i++)
            {
                Queue<int> indexes = new Queue<int>();
                int currentRow = commands[i, 0];

                // Saving indexes of all platforms at this row
                for (int column = 0; column < fieldColumns; column++)
                {
                    if (field[currentRow, column] == PlatformSign)
                    {
                        indexes.Enqueue(column);
                        field[currentRow, column] = EmptySpaceSign;
                    }
                }

                // Calculating needed moves of the platforms
                int moves = commands[i, 1] % fieldColumns;

                // Moving platforms
                while (indexes.Count > 0)
                {
                    int wantedIndex = indexes.Dequeue() + moves;

                    if (wantedIndex < fieldColumns)
                    {
                        field[currentRow, wantedIndex] = PlatformSign;
                    }
                    else
                    {
                        wantedIndex -= fieldColumns;
                        field[currentRow, wantedIndex] = PlatformSign;
                    }
                }

                // Checking if player can move
                if (field[playerRow - 1, playerColumn] == PlatformSign)
                {
                    jumpsCount++;

                    // Clearing old position
                    field[playerRow, playerColumn] = PlatformSign;

                    // Moving to new position
                    playerRow--;

                    field[playerRow, playerColumn] = PlayerSign;
                }
            }

            // Checking if player has moved
            if (playerRow < fieldRows - 1)
            {
                // Cleaning his old position
                field[fieldRows - 1, playerColumn] = EmptySpaceSign;
            }

            // Preparing result for printing
            StringBuilder result = new StringBuilder();

            if (playerRow == 0)
            {
                result.AppendLine("Win");
            }
            else
            {
                result.AppendLine("Lose");
            }

            result.AppendLine($"Total Jumps: {jumpsCount}");

            for (int row = 0; row < fieldRows; row++)
            {
                for (int column = 0; column < fieldColumns; column++)
                {
                    result.Append(field[row, column]);
                }

                result.AppendLine();
            }

            // Printing result
            Console.Write(result);
        }
    }
}