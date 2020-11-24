using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalAPI.Core.Repositories;
using DigitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace DigitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsDataController : ControllerBase
    {
        private IClientRepository _ClientRepository { get; set; }
        public IClientRepository ClientRepository
        {
            get
            {
                if (_ClientRepository == null)
                {
                    return new ClientRepository();
                }
                return _ClientRepository;
            }

            set { _ClientRepository = value; }
        }

        [HttpGet]
        public string GetClientDatas()
        {
            try
            {
                var listofClientDatas = ClientRepository.ListAllClientsData();

                var jsonResult = JsonConvert.SerializeObject(listofClientDatas);

                return jsonResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public string API_SaveCustomerDataOnDB(ClientData clientData)
        {
            try
            {
                var lengthCardNumber = clientData.CardNumber.ToString().Length;
                var lengthCVV = clientData.CVV.ToString().Length;

                if (lengthCardNumber > 16)
                {
                    return "Card Number has a max of 16 characters allowed";
                }

                else if (lengthCVV > 5)
                {
                    return "CVV has a max of 5 characters allowed";
                }

                else if (ClientRepository.Save(clientData).Result == true)
                {
                    var clientreturn = new ClientReturn(clientData);
                    var jsonResult = JsonConvert.SerializeObject(clientreturn);
                    return jsonResult;
                }

                else
                {
                    return "It was not possible to save client information, contact the support team";
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return "It was not possible to save client information, contact the support team"; 
                
            }
       
        }
    }
}