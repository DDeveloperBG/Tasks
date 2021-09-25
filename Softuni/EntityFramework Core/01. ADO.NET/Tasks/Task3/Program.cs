using System;
using Microsoft.Data.SqlClient;

namespace Task3
{
    class Program
    {
        static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());
            GetVillainReport(villainId);
        }

        static void GetVillainReport(int villainId)
        {
            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = GetVillainDataCommand(connection, villainId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        return;
                    }

                    if (reader["MinionName"].GetType() == typeof(DBNull))
                    {
                        Console.WriteLine("(no minions)");
                        return;
                    }

                    Console.WriteLine($"Villain: {reader["VillainName"]}");
                    int counter = 1;

                    do
                    {
                        Console.WriteLine($"{counter}. {reader["MinionName"]} {reader["MinionAge"]}");
                        counter++;

                    } while (reader.Read());
                }
            }
        }

        static SqlCommand GetVillainDataCommand(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(
                   @"SELECT v.[Name] AS VillainName,
	                    	m.[Name] AS MinionName,
	                    	m.Age AS MinionAge
	                    FROM Villains v
	                    LEFT JOIN MinionsVillains mv ON mv.VillainId = v.Id
	                    LEFT JOIN Minions m ON m.Id = mv.MinionId
	                    WHERE mv.VillainId = @WantedVillainId
	                    ORDER BY MinionName ASC", connection);

            command.Parameters.AddWithValue("WantedVillainId", villainId);
            return command;
        }
    }
}
