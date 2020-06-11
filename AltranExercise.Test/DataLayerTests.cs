using AltranExercise.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AltranExercise.Test
{
    [TestClass]
    public class DataLayerTests
    {
        [TestMethod]
        public void ClientsRepository_GetClientByEmail_ExistingEmail_ReturnsClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(email: ArrangeProvider._EMAIL_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientByEmail(ArrangeProvider._EMAIL_);

            //Assert
            Assert.AreEqual(client, result);
        }

        [TestMethod]
        public void ClientsRepository_GetClientByName_ExistingName_ReturnsClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(name: ArrangeProvider._NAME_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientByName(ArrangeProvider._NAME_);

            //Assert
            Assert.AreEqual(client, result);
        }

        [TestMethod]
        public void ClientsRepository_GetClientById_ExistingId_ReturnsClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(id: ArrangeProvider._ID1_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientById(ArrangeProvider._ID1_);

            //Assert
            Assert.AreEqual(client, result);
        }

        [TestMethod]
        public void ClientsRepository_GetClientByEmail_NonExistingEmail_ReturnsNullClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(email: ArrangeProvider._EMAIL_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientByEmail(ArrangeProvider._EMAIL2_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ClientsRepository_GetClientByName_NonExistingName_ReturnsNullClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(name: ArrangeProvider._NAME_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientByName(ArrangeProvider._NAME2_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ClientsRepository_GetClientById_NonExistingId_ReturnsNullClient()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var client = ArrangeProvider.GetClient(id: ArrangeProvider._ID1_);
            var json = JsonConvert.SerializeObject(ArrangeProvider.GetRootClient(client), Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new ClientsRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetClientById(ArrangeProvider._ID0_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PoliciesRepository_GetPoliciesByClientId_ExistingClientIdWithPolicies_ReturnsPolicies()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var rootPolicy = ArrangeProvider.GetRootPolicy();
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("1", ArrangeProvider._EMAIL_, ArrangeProvider._ID1_, 1.1));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("2", ArrangeProvider._EMAIL_, ArrangeProvider._ID1_, 2.2));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("3", ArrangeProvider._EMAIL_, ArrangeProvider._ID1_, 3.3));

            var json = JsonConvert.SerializeObject(rootPolicy, Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new PoliciesRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetPoliciesByClientId(ArrangeProvider._ID1_);

            //Assert
            CollectionAssert.AreEquivalent((System.Collections.ICollection)rootPolicy.Policies, (System.Collections.ICollection)result);
        }

        [TestMethod]
        public void PoliciesRepository_GetPoliciesByClientId_ExistingClientIdWithoutPolicies_ReturnsZeroPolicies()
        {
            //Arrange
            string url = ArrangeProvider._URL_;

            var rootPolicy = ArrangeProvider.GetRootPolicy();

            var json = JsonConvert.SerializeObject(rootPolicy, Formatting.Indented);

            var options = ArrangeProvider.GetResourcesUrlOption(url, url);
            var httpClient = ArrangeProvider.GetMockJsonHttpClient(url, json);

            var clientsRepository = new PoliciesRepository(options, httpClient);

            //Act
            var result = clientsRepository.GetPoliciesByClientId(ArrangeProvider._ID1_);

            //Assert
            Assert.IsTrue(result.Count == 0);
        }
    }
}
