using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace terminarz_projekt.Models
{
    class TableSearch
    { 
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=terminarz_projekt.Data;Trusted_Connection=True;"; 
            string tableName = "dbo.CalendarModel"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM {tableName}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Wyświetlanie nagłówków kolumn
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i) + "\t");
                        }
                        Console.WriteLine();

                        /// Wyświetlanie danych wiersz po wierszu
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[i] + "\t");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}


