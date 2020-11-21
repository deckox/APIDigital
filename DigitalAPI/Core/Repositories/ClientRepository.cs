﻿using DigitalAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalAPI.Core.Repositories
{
    public class ClientRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into ClientData (CustomerId,CardNumber,CVV,RegistrationDate) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE ClientData SET CustomerId='{1}',CardNumber='{2}',CVV='{3}',RegistrationDate='{4}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from ClientData WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from ClientData";
      

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Save(ClientData clientData)
        {
            var data = clientData.RegistrationDate;
            var dataConvertida = data.ToString("yyyy-MM-dd");
            var sql = "";
           // var aux = new ClientData();
            if (clientData.CardId == 0) //if cardId 0 its a new user
                sql = string.Format(Sql_Insert, clientData.CustomerId, clientData.CardNumber, clientData.CVV, dataConvertida);
            else //user with Id then user should be updated
                sql = string.Format(Sql_Update, clientData.CustomerId, clientData.CardNumber, clientData.CVV, dataConvertida);

            var result = ExecuteCommand(sql);

            //if (result == true)
            //{
            //   aux = SearchForCardId();
            //}

          //  clientData.CardId = aux.CardId;

            return result;
        }


        public List<ClientData> Listar()
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

        private ClientData Parse(SQLiteDataReader reader)
        {

            var client = new ClientData()
            {
                CardId = int.Parse(reader["CardId"].ToString()),
                CustomerId = int.Parse(reader["CustomerId"].ToString()),
                CVV = int.Parse(reader["CVV"].ToString()),
                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
               
            };
            return client;
        }

    }
}
