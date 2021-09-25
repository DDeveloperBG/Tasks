using System;
using Microsoft.Data.SqlClient;

namespace Task2
{
    class Program
    {
        static void Main()
        {
            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    @"SELECT v.[Name] AS VillianName,
		                COUNT(*) AS MinionsCount
	                    FROM Villains v
	                    JOIN MinionsVillains mv ON mv.VillainId = v.Id
	                    JOIN Minions m ON m.Id = mv.MinionId
	                    GROUP BY v.[Name]
	                    HAVING COUNT(*) > 3
	                    ORDER BY MinionsCount DESC", connection);

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Console.WriteLine($"{result["VillianName"]} - {result["MinionsCount"]}");
                    }
                }
            }
        }
    }
}
