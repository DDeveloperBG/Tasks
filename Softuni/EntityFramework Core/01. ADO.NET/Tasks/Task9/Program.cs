using System;
using Microsoft.Data.SqlClient;

namespace Task9
{
    class Program
    {
        /*
         CREATE PROCEDURE usp_GetOlder(
	        @MinionId INT
         )
         AS
         	UPDATE Minions
         		SET Age = Age + 1
         		WHERE Id = @MinionId
         */

        static void Main()
        {
            int minionId = int.Parse(Console.ReadLine());

            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                IncreaseMinionAgeWithOne(connection, minionId);
                PrintMinionNameAndAge(connection, minionId);
            }
        }

        static void IncreaseMinionAgeWithOne(SqlConnection connection, int minionId)
        {
            var increaseAgeCommand = new SqlCommand(@"EXEC dbo.usp_GetOlder @MinionId", connection);
            increaseAgeCommand.Parameters.AddWithValue("@MinionId", minionId);

            increaseAgeCommand.ExecuteNonQuery();
        }

        static void PrintMinionNameAndAge(SqlConnection connection, int minionId)
        {
            var getNameAndAgeCommand = new SqlCommand(
                @"SELECT [Name] AS MinionName,
	                    	Age AS MinionAge
	                    FROM Minions
	                    WHERE Id = @MinionId", connection);
            getNameAndAgeCommand.Parameters.AddWithValue("@MinionId", minionId);

            using (var reader = getNameAndAgeCommand.ExecuteReader())
            {
                reader.Read();
                Console.WriteLine($"{reader["MinionName"]} - {reader["MinionAge"]} years old");
            }
        }
    }
}
