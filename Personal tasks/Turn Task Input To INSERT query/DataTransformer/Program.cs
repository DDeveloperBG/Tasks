using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DataTransformer
{
    class Program
    {
        public class Input
        {
            static string inputFilePath = @"C:\Users\User\Desktop\inputForTask2.txt";
            private Queue<string> fullInput = null;

            private void PrepareInputeData()
            {
                using (StreamReader inputReader = new StreamReader(inputFilePath))
                {
                    fullInput = new Queue<string>(inputReader.ReadToEnd().Split(Environment.NewLine));
                }
            }

            public Input()
            {
                PrepareInputeData();
            }

            public string ReadLine()
            {
                return fullInput.Dequeue();
            }
        }

        /// <summary>
        /// Data format:
        ///     [Table Name]
        ///     [Sequence of Column Names]
        ///     [Sequence rows of values]
        ///
        /// Wanted Result:
        ///    INSERT INTO ...(..., ...)
	    ///        VALUES (...), (...), ...
        /// </summary>
        static void Main()
        {
            Input input = new Input();

            List<string> result = new List<string>();

            while (true)
            {
                string tableName = input.ReadLine();
                string columns = string.Join(", ", input.ReadLine().Split('	'));
                string inputRow = input.ReadLine();
                List<string> rowsOfValues = new List<string>();

                while (inputRow != "End" && inputRow != "")
                {
                    var values = inputRow.Split('	').Select(value =>
                    {
                        if(value.Any(character => char.IsLetter(character)) && value != "NULL")
                        {
                            return $"'{value}'";
                        }

                        return value;
                    });

                    rowsOfValues.Add('(' + string.Join(", ", values) + ')');

                    inputRow = input.ReadLine();
                }

                PrintResult(tableName, columns, rowsOfValues);

                if (inputRow == "End")
                {
                    Console.ReadLine();
                    return;
                }
            }
        }

        static void PrintResult(string tableName, string columns, List<string> values)
        {
            Console.WriteLine($"INSERT INTO {tableName}({columns})");
            Console.WriteLine("\tVALUES " + string.Join($",{Environment.NewLine}\t\t", values));
            Console.WriteLine();
        }
    }
}