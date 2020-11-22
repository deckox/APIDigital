using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalAPI.Core.Repositories;
using DigitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DigitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsDataController : ControllerBase
    {
        [HttpGet]
        public string GetClientDatas()
        {
            try
            {
                var clientRepository = new ClientRepository();

                var listaDeAgendamentosDoBD = clientRepository.ListAllClientsData();

                var jsonResult = JsonConvert.SerializeObject(listaDeAgendamentosDoBD);

                return jsonResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public string APIOne(ClientData clientData)
        {
            try
            {
                var lengthCardNumber = clientData.CardNumber.ToString().Length;
                var lengthCVV = clientData.CVV.ToString().Length;

                var clientRepository = new ClientRepository();

                if (lengthCardNumber > 16)
                {
                    return "Card Number has a max of 16 characters allowed";
                }

                else if (lengthCVV > 5)
                {
                    return "CVV has a max of 5 characters allowed";
                }

                else if (clientRepository.Save(clientData) == true)
                {
                    var clientreturn = new ClientReturn(clientData);
                    var jsonResult = JsonConvert.SerializeObject(clientreturn);
                    return jsonResult;
                }

                else
                {
                    return "nao";
                }
            }
            catch (Exception msg)
            {

                throw;
            }
       
        }
    }
}