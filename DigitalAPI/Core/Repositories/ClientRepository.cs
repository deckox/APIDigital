using DigitalAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalAPI.Core.Repositories
{
    public interface IClientRepository
    {
        Task<bool> Save(ClientData clientData);
        List<ClientData> ListAllClientsData();

        Task<bool> IsClientDataInformationValidationOK(ClientData clientData);
    }

    public class ClientRepository : BaseRepository, IClientRepository
    {
        private const string Sql_Insert = "INSERT into ClientData (CustomerId,CardNumber,CVV,RegistrationDate) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE ClientData SET CustomerId='{1}',CardNumber='{2}',CVV='{3}',RegistrationDate='{4}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from ClientData WHERE CardId = {0}";
        private const string Sql_SelectById = "SELECT * from ClientData WHERE CardId = {0}";
        private const string Sql_Select = "SELECT * from ClientData";
      

        //public bool Excluir(int id)
        //{
        //    var sql = string.Format(Sql_Delete, id);
        //    return ExecuteCommand(sql);
        //}

        public async Task<bool> Save(ClientData clientData)
        {
            var date = clientData.RegistrationDate;
            var convertedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";

            if (clientData.CardId == 0) //if cardId 0 its a new user
                sql = string.Format(Sql_Insert, clientData.CustomerId, clientData.CardNumber, clientData.CVV, convertedDate);
            else //user with Id then user should be updated
                sql = string.Format(Sql_Update, clientData.CustomerId, clientData.CardNumber, clientData.CVV, convertedDate);

            var result = ExecuteCommand(sql);

            if (result == true)
            {
                var aux = new ClientData();
                aux = GetClientDataInformation();
                clientData.CardId = aux.CardId;
                clientData.RegistrationDate = aux.RegistrationDate;
            }

            return result;
        }


        public List<ClientData> ListAllClientsData()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = Sql_Select;

            var result = new List<ClientData>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cliente = Parse(reader);
                    result.Add(cliente);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        public async Task<bool> IsClientDataInformationValidationOK(ClientData clientData)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            var result = true;
            command.CommandText = string.Format(Sql_SelectById,clientData.CardId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows != true)
                {
                    result = false;
                }

                while (reader.Read())
                {
                    var clientOnDataBase = Parse(reader);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ########################### Card Number: " + clientOnDataBase.CardNumber.ToString() + " ###########################", Console.ForegroundColor);

                    var clientreturn = new ClientReturn(clientOnDataBase);
                    var generateNewToken = clientreturn.CircularArray(clientOnDataBase);

                    TimeSpan ts = clientData.RegistrationDate.Subtract(clientOnDataBase.RegistrationDate);
                    var time = ts.TotalMinutes;

                    if (time > 30)
                    {
                        result = false;
                    }
                    
                    else if (clientOnDataBase.CustomerId != clientData.CustomerId)
                    {
                        result = false;
                    }

                    else if (clientData.Token != generateNewToken)
                    {
                        result = false;
                    }

                    else if (clientData.CVV != clientOnDataBase.CVV)
                    {
                        result = false;
                    }
                }

            }

            return result;
        }

        private ClientData Parse(SQLiteDataReader reader)
        {

            var client = new ClientData()
            {
                CardId = int.Parse(reader["CardId"].ToString()),
                CustomerId = int.Parse(reader["CustomerId"].ToString()),
                CardNumber = long.Parse(reader["CardNumber"].ToString()),
                CVV = int.Parse(reader["CVV"].ToString()),
                RegistrationDate = DateTime.Parse(reader["RegistrationDate"].ToString()),
            };
            return client;
        }

    }
}
