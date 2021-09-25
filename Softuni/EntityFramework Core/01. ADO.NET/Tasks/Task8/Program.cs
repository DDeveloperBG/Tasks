using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Task8
{
    class Program
    {
        static void Main()
        {
            var vals = Console.ReadLine().Split().Select(int.Parse).ToDictionary(x => "@" + x, y => y);

            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                IncreaseAllMinionsAge(vals, connection);
                PrintAllMinions(connection);
            }
        }

        static void PrintAllMinions(SqlConnection connection)
        {
            var getAllMinions = new SqlCommand(
                @"SELECT [Name] As MinionName,
                        Age As MinionAge 
                    FROM Minions", connection);

            using (var reader = getAllMinions.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["MinionName"]} {reader["MinionAge"]}");
                }
            }
        }

        static void IncreaseAllMinionsAge(Dictionary<string, int> vals, SqlConnection connection)
        {
            var getAllMinions = new SqlCommand(
                $@"UPDATE Minions
                    SET Age = Age + 1
                    WHERE Id IN ({string.Join(", ", vals.Keys)})", connection);

            foreach (var kvp in vals)
            {
                getAllMinions.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }

            getAllMinions.ExecuteNonQuery();
        }
    }
}
