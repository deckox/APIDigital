using NUnit.Framework;
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

        [Test]
        public void MoreThan_MaxLengthOf_CardId_Fail()
        {
            var clientController = new ClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 4,
                CardNumber = 132456798465123123,
                CVV = 3333
            };

            var apiSaveCustomerResponse = clientController.API_SaveCustomerDataOnDB(clientData);
            Assert.That(apiSaveCustomerResponse, Contains.Substring("Card Number has a max of 16 characters allowed"));

        }

        [Test]
        public void MoreThan_MaxLengthOf_CVV_Fail()
        {
            var clientController = new ClientsDataController();

            var clientData = new ClientData()
            {
                CustomerId = 4,
                CardNumber = 1324567984651231,
                CVV = 3333123
            };

            var apiSaveCustomerResponse = clientController.API_SaveCustomerDataOnDB(clientData);
            Assert.That(apiSaveCustomerResponse, Contains.Substring("CVV has a max of 5 characters allowed"));

        }

    }
}
