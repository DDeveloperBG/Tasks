using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Task7
{
    class Program
    {
        static void Main()
        {
            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var minionsNames = GetAllMinionsNames(connection);
                int middle = minionsNames.Count / 2;

                for (int i = 0; i < middle; i++)
                {
                    Console.WriteLine(minionsNames[i]);
                    Console.WriteLine(minionsNames[minionsNames.Count - i - 1]);
                }

                Console.WriteLine(minionsNames[middle]);

                if (minionsNames.Count % 2 == 0)
                {
                    Console.WriteLine(minionsNames[middle + 1]);
                }
            }
        }

        static List<string> GetAllMinionsNames(SqlConnection connection)
        {
            var getAllNamesCommand = new SqlCommand(
                @"SELECT [Name] AS MinionName
                    FROM Minions", connection);

            List<string> names = new List<string>();
            using (var reader = getAllNamesCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    names.Add((string)reader["MinionName"]);
                }
            }

            return names;
        }
    }
}
