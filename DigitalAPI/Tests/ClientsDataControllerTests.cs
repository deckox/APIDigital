using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalAPI.Controllers;
using DigitalAPI.Core.Repositories;
using DigitalAPI.Models;
using Moq;

namespace DigitalAPI.Tests
{
    [TestFixture]
    public class ClientsDataControllerTests
    {
        [Test]
        public void SaveClientData_OnDatabase_Success()
        {
            var clientController = new ClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 4,
                CardNumber = 132456798465123,
                CVV = 3333
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.Save(clientData)).ReturnsAsync(true);
            clientController.ClientRepository = Service.Object;

            var apiSaveCustomerResponse = clientController.API_SaveCustomerDataOnDB(clientData);
            Assert.That(apiSaveCustomerResponse, Contains.Substring("3512"));

        }

        [Test]
        public void SaveClientData_OnDatabase_Fail()
        {
            var clientController = new ClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 4,
                CardNumber = 132456798465123,
                CVV = 3333
            };


            var Service = new Mock<IClientRepository>();
            Service.Setup(_ => _.Save(clientData)).ReturnsAsync(false);
            clientController.ClientRepository = Service.Object;

            var apiSaveCustomerResponse = clientController.API_SaveCustomerDataOnDB(clientData);
            Assert.That(apiSaveCustomerResponse, Contains.Substring("It was not possible to save client information, contact the support team"));

        }

    }
}
