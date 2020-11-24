using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalAPI.Controllers;
using DigitalAPI.Core.Repositories;
using DigitalAPI.Models;
using Moq;
using DigitalAPI.ControllersValidation;

namespace DigitalAPI.Tests
{
    [TestFixture]
    public class ValidationOfClientsDataTests
    {
        [Test]
        public void ValidationData_OnDatabase_Success()
        {
            var clientController = new ValidationOfClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 11,
                CardId = 23,
                Token = 2222,
                CVV = 2222
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.IsClientDataInformationValidationOK(It.IsAny<ClientData>())).ReturnsAsync(true);
            clientController.ClientRepository = Service.Object;

            var validateTokenResponse = clientController.API_ValidateToken(clientData);
            Assert.That(validateTokenResponse, Is.EqualTo(true));

        }

        public void ValidationData_OnDatabase_TokenExpired()
        {
            var clientController = new ValidationOfClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 11,
                CardId = 23,
                Token = 2222,
                CVV = 2222
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.IsClientDataInformationValidationOK(clientData)).ReturnsAsync(true);
            clientController.ClientRepository = Service.Object;

            var validateTokenResponse = clientController.API_ValidateToken(clientData);
            Assert.That(validateTokenResponse, Is.EqualTo(true));

        }

        public void ValidationData_OnDatabase_CustomerIsNotOwnerOfTheCard()
        {
            var clientController = new ValidationOfClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 11,
                CardId = 23,
                Token = 2222,
                CVV = 2222
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.IsClientDataInformationValidationOK(clientData)).ReturnsAsync(true);
            clientController.ClientRepository = Service.Object;

            var validateTokenResponse = clientController.API_ValidateToken(clientData);
            Assert.That(validateTokenResponse, Is.EqualTo(true));

        }

        public void ValidationData_OnDatabase_TokenNotMatch()
        {
            var clientController = new ValidationOfClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 11,
                CardId = 23,
                Token = 2222,
                CVV = 2222
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.IsClientDataInformationValidationOK(It.IsAny<ClientData>())).ReturnsAsync(true);
            clientController.ClientRepository = Service.Object;

            var validateTokenResponse = clientController.API_ValidateToken(clientData);
            Assert.That(validateTokenResponse, Is.EqualTo(true));

        }

        [Test]
        public void MoreThan_MaxLengthOf_CVV_Fail()
        {
            var clientController = new ValidationOfClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 11,
                CardId = 23,
                Token = 2222,
                CVV = 222244
            };

            var validateTokenResponse = clientController.API_ValidateToken(clientData);
            Assert.That(validateTokenResponse, Is.EqualTo(false));

        }
    }
}
