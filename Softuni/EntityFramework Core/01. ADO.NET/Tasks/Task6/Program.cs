using System;
using Microsoft.Data.SqlClient;

namespace Task6
{
    class Program
    {
        static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());

            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                string villainName = GetVillainName(connection, transaction, villainId);

                if (villainName is null)
                {
                    Console.WriteLine("No such villain was found.");
                    transaction.Commit();
                    return;
                }

                int releasedMinionsCount;

                try
                {
                    releasedMinionsCount = DeleteVillainToMinionsConnection(connection, transaction, villainId);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                try
                {
                    DeleteVillain(connection, transaction, villainId);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{releasedMinionsCount} minions were released.");
                transaction.Commit();
            }
        }

        static string GetVillainName(SqlConnection connection, SqlTransaction transaction, int villainId)
        {
            var command = new SqlCommand(
                @"SELECT [Name] AS VillainName
	                FROM Villains
	                WHERE Id = @VillainId", connection, transaction);
            command.Parameters.AddWithValue("@VillainId", villainId);

            object villainName = command.ExecuteScalar();
            return villainName is null ? null : (string)villainName;
        }

        static int DeleteVillainToMinionsConnection(SqlConnection connection, SqlTransaction transaction, int villainId)
        {
            var command = new SqlCommand(
              @"DELETE
	                FROM MinionsVillains
	                WHERE VillainId = @VillainId", connection, transaction);
            command.Parameters.AddWithValue("@VillainId", villainId);

            return command.ExecuteNonQuery();
        }

        static void DeleteVillain(SqlConnection connection, SqlTransaction transaction, int villainId)
        {
            var command = new SqlCommand(
               @"DELETE
	                FROM Villains
	                WHERE Id = @VillainId", connection, transaction);
            command.Parameters.AddWithValue("@VillainId", villainId);

            if (command.ExecuteNonQuery() != 1)
            {
                throw new Exception("Didn't deleted villain");
            }
        }
    }
}