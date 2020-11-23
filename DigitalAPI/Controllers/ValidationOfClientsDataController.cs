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
        [HttpPost]
        public bool API_ValidateToken(ClientData clientData)
        {
            try
            {
                var lengthCVV = clientData.CVV.ToString().Length;

                if (lengthCVV > 5)
                {
                    return false;
                }

                var clientRepository = new ClientRepository();

                if (clientRepository.IsClientDataInformationValidationOK(clientData) == true)
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
