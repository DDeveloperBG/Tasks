using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Task5
{
    class Program
    {
        static void Main()
        {
            string countryName = Console.ReadLine();
            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int changedNames = ChangeCountryNames(countryName, connection);
                
                if(changedNames == 0)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                Console.WriteLine($"{changedNames} town names were affected.");
               
                var towns = GetCountryTownNames(countryName, connection);
                Console.WriteLine($"[{string.Join(", ", towns)}]");
            }
        }

        static int ChangeCountryNames(string countryName, SqlConnection connection)
        {
            var updateToUppercase = new SqlCommand(
                    @"DECLARE @CountryCode CHAR(3) = (SELECT TOP(1) Id 
                                                        FROM Countries 
                                                        WHERE [Name] = @CountryName)

                    UPDATE Towns
                    	SET [Name] = UPPER([Name])
                    	WHERE CountryCode = @CountryCode", connection);
            updateToUppercase.Parameters.AddWithValue("@CountryName", countryName);

            return updateToUppercase.ExecuteNonQuery();
        }

        static List<string> GetCountryTownNames(string countryName, SqlConnection connection)
        {
            var getTownsCommand = new SqlCommand(
                @"SELECT t.[Name] AS TownName
	                FROM Towns t
	                JOIN Countries c ON c.Id = t.CountryCode
	                WHERE c.[Name] = @CountryName", connection);
            getTownsCommand.Parameters.AddWithValue("@CountryName", countryName);

            List<string> names = new List<string>();
            using (var reader = getTownsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    names.Add((string)reader["TownName"]);
                }
            }

            return names;
        }
    }
}
