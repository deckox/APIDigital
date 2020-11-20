using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DigitalAPI.Core.Repositories
{
    public abstract class BaseRepository
    {
        private const string DataBaseFile = "database.db";
        private string ConnectionString;
        private string Database;

        private const string Sql_CreateClientData = "CREATE TABLE ClientData (CardId INTEGER PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER, CardNumber INTEGER, CVV INTEGER, RegistrationDate DATETIME)";

        public BaseRepository()
        {
            Database = Path.Combine(System.AppContext.BaseDirectory, DataBaseFile);
            ConnectionString = string.Format("Data Source={0};Version=3;", Database);
            CreateDatabaseIfDoNotExist();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public void CreateDatabaseIfDoNotExist()
        {
            var exist = File.Exists(Database);

            if (!exist)
            {
                SQLiteConnection.CreateFile(Database);
                ExecuteCommand(Sql_CreateClientData);
                 
            }

        }

        public bool ExecuteCommand(string sqlcommand)
        {
            try
            {
                var connection = GetConnection();
                connection.Open();
                var command = new SQLiteCommand(connection);


                command.CommandText = sqlcommand;
                var result = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();
                connection.Dispose();

                return result > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

   
