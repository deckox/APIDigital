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

        [HttpPost]
        public bool API_ValidateToken(ClientData clientData)
        {
            try
            {
                var lengthCVV = clientData.CVV.ToString().Length;
                var clientRepository = new ClientRepository();

                if (lengthCVV > 5)
                {
                    return false;
                }

                else if (clientRepository.IsClientDataInformationValidationOK(clientData).Result == true)
                {
                    return true;
                }

                return false;
           
            }
            catch (Exception msg)
            {

                return false; 
            }

        }
    }
}
