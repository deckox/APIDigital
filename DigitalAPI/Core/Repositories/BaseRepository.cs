using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using DigitalAPI.Models;

namespace DigitalAPI.Core.Repositories
{
    public abstract class BaseRepository
    {
        private const string DataBaseFile = "database.db";
        private string ConnectionString;
        private string Database;

        private const string Sql_CreateClientData = "CREATE TABLE ClientData (CardId INTEGER PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER, CardNumber INTEGER, CVV INTEGER, RegistrationDate DATETIME)";
        private const string Sql_SelectLastCardId = "SELECT* from Clientdata order by cardid desc limit 1";
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

        public ClientData GetClientDataInformation()
        {
            try
            {
                var connection = GetConnection();
                connection.Open();
                var command = new SQLiteCommand(connection);


             
                command.CommandText = Sql_SelectLastCardId;
                var client = new ClientData();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        client = Parse(reader);
                    }
                }

                command.Dispose();
                connection.Close();
                connection.Dispose();

                return client;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private ClientData Parse(SQLiteDataReader reader)
        {
            var client = new ClientData()
            {
                CardId = int.Parse(reader["CardId"].ToString()),
                CardNumber = long.Parse(reader["CardNumber"].ToString()),
                CVV = int.Parse(reader["CVV"].ToString()),
                RegistrationDate = DateTime.Parse(reader["RegistrationDate"].ToString()),
            };
            return client;
        }
    }
}

   
