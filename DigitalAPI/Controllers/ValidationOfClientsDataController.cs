using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalAPI.Core.Repositories;
using DigitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DigitalAPI.ControllersValidation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationOfClientsDataController : ControllerBase
    {
        [HttpGet]
        public string GetClientDatas1()
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
        public bool APITwo(ClientData clientData)
        {
            try
            {
                var clientRepository = new ClientRepository();
                
                if (clientRepository.ClientDataInformationValidation(clientData) == true)
                {
                    return true;
                }

                return false;
           
            }
            catch (Exception msg)
            {

                throw;
            }

        }
    }
}
