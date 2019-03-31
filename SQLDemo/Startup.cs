using System;
using System.Data.SqlClient;

namespace SQLDemo
{
    class Startup
    {
        static void Main()
        {
            // SQL Connection string example
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-BTSMGLL\\SQLEXPRESS; Database=SoftUni; Integrated Security=true");

            using (connection)
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Employees"; // Query string
                SqlCommand command = new SqlCommand(query, connection); // Command are created, but not executed yet
                var result = command.ExecuteScalar(); // Command are executed
                Console.WriteLine($"Employees count: {result}"); // Print the result from command

                query = "SELECT * FROM Employees";
                command = new SqlCommand(query, connection);
                    
                var reader = command.ExecuteReader();
                using (reader)
                {
                    reader.Read(); // Read first row from database
                    Console.WriteLine(reader[0]); // Return first row, first column -> Id = 1
                    Console.WriteLine(reader[1]); // Return first row, second column -> FirstName = Guy

                    reader.Read(); // Read second row from database
                    Console.WriteLine(reader[0]); // Return second row, first column -> Id = 2
                    Console.WriteLine(reader[1]); // Return second row, second column -> FirstName = Kevin
                }
                
                string townName = "New Town";
                query = $"INSERT INTO Towns (Name) VALUES ('{townName}')"; // SQL Injection friendly
                command = new SqlCommand(query, connection);
                int affectedRows = command.ExecuteNonQuery();
                Console.WriteLine($"Affected {affectedRows} rows.");

                // User parameters to avoid SQL injection
                query = $"INSERT INTO Towns (Name) VALUES (@TownName)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TownName", "New New Town");
                affectedRows = command.ExecuteNonQuery();
                Console.WriteLine($"Affected {affectedRows} rows.");
            }
        }
    }
}
