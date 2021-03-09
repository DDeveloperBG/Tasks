using System;
using System.Linq;
using System.Collections.Generic;

namespace ConnectedAreasInMatrix
{
    class Program
    {
        public const char wall = '*';

        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            /*int columns = */
            int.Parse(Console.ReadLine()); // Not Needed Value

            char[][] matrix = new char[rows][];
            ReadInputAndSetMatrix(matrix, rows);

            Dictionary<AreaCordinates, int> areas = new Dictionary<AreaCordinates, int>();
            AreaCordinates cordinates = GetAreaCordinates(matrix);

            while (cordinates.AreValid())
            {
                int size = FindAreaReplaceAndReturnSize(matrix, cordinates.Row, cordinates.Column);
                areas.Add(cordinates, size);

                cordinates = GetAreaCordinates(matrix);
            }

            PrintResult(areas);
        }

        static void ReadInputAndSetMatrix(char[][] matrix, int rows)
        {
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }
        }

        static int FindAreaReplaceAndReturnSize(char[][] matrix, int areaRow, int areaColumn)
        {
            int size = 0;
            int lastColumnEndInd = 0;
            int lastColumnStartInd = areaColumn;

            // Go through all rows
            for (int row = areaRow; row < matrix.Length; row++)
            {
                int currentRowSize = 0;
                int currentColumnEndInd = -1;
                bool lastWasCell = true;

                // Find start of the current row
                if (matrix[row][areaColumn] != wall)
                {
                    while (areaColumn > 0)
                    {
                        if (matrix[row][areaColumn - 1] != wall)
                        {
                            areaColumn--;
                        }
                        else break;
                    }
                }

                // Go through all columns
                for (int column = areaColumn; column < matrix[0].Length; column++)
                {
                    // If this cell is part of the area
                    if (matrix[row][column] != wall)
                    {
                        if (column <= lastColumnEndInd || lastWasCell)
                        {
                            // Find if upper are more cells
                            if (column > lastColumnEndInd)
                            {
                                int wantedRow = row;

                                while (wantedRow > 0)
                                {
                                    if (matrix[wantedRow - 1][column] != wall)
                                    {
                                        wantedRow--;
                                    }
                                    else break;
                                }

                                if (wantedRow < row)
                                {
                                    size += FindAreaReplaceAndReturnSize(matrix, wantedRow, column);
                                    continue;
                                }
                            }

                            matrix[row][column] = wall;
                            currentColumnEndInd = column;
                            lastWasCell = true;
                            size++;
                            currentRowSize++;
                        }
                        else break;
                    }
                    else if (column < lastColumnEndInd)  // If this cell is wall but still inside the area
                    {
                        if (column < lastColumnStartInd)
                        {
                            size -= currentRowSize;
                            break;
                        }

                        lastWasCell = false;
                        continue;
                    }
                    else break;  // If area ends
                }

                // If row was empty end area
                if (currentRowSize == 0)
                {
                    break;
                }

                lastColumnStartInd = areaColumn;
                lastColumnEndInd = currentColumnEndInd;
            }

            return size;
        }

        static AreaCordinates GetAreaCordinates(char[][] matrix)
        {
            int areaRow = -1;
            int areaColumn = -1;

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int column = 0; column < matrix[0].Length; column++)
                {
                    if (matrix[row][column] != wall)
                    {
                        areaRow = row;
                        areaColumn = column;

                        return new AreaCordinates(areaRow, areaColumn);
                    }
                }
            }

            return new AreaCordinates(areaRow, areaColumn);
        }

        static void PrintResult(Dictionary<AreaCordinates, int> areas) 
        {
            Console.WriteLine($"Total areas found: {areas.Count}");

            areas = areas.OrderByDescending(kvp => kvp.Value).ThenBy(kvp => kvp.Key.Row).ThenBy(kvp => kvp.Key.Column).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            int order = 1;

            foreach (var kvp in areas)
            {
                Console.WriteLine($"Area #{order} at ({kvp.Key.Row}, {kvp.Key.Column}), size: {kvp.Value}");
                order++;
            }
        }

        class AreaCordinates
        {
            public AreaCordinates(int row, int column)
            {
                this.Row = row;
                this.Column = column;
            }

            public int Row { get; set; }
            public int Column { get; set; }

            public bool AreValid()
            {
                return this.Row > -1 && this.Column > -1;
            }
        }
    }
}