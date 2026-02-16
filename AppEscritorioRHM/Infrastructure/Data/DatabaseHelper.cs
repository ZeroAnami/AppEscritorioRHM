using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace AppEscritorioRHM.Infrastructure.Data
{
    public static class DatabaseHelper
    {
        private const string DbName = "AppRHM.db";
        public static string ConnectionString = $"Data Source={DbName}";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DbName))
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserID TEXT PRIMARY KEY,
                        UserName TEXT NOT NULL UNIQUE,
                        PasswordHashed TEXT NOT NULL,
                        ProjectSelected TEXT, 
                        ProjectsConfiguredJson TEXT
                    );";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
