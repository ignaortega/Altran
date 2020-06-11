using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Entities;
using AltranExercise.Data.Repositories;
using AltranExercise.Service.DTOs;
using AltranExercise.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Runtime.InteropServices;

namespace AltranExercise.Test
{
    [TestClass]
    public class ServiceLayerTest
    {
        [TestMethod]
        public void ClientsService_GetClientByName_ExistingName_ReturnsClient()
        {
            //Arrange
            string name = ArrangeProvider._NAME_;
            Client client = ArrangeProvider.GetClient(email: name);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientByName(name)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.GetClientByName(name);

            //Assert
            Assert.AreEqual(mapper.Map<ClientDto>(client), result);
        }

        [TestMethod]
        public void ClientsService_GetClientById_ExistingId_ReturnsClient()
        {
            //Arrange
            string id = ArrangeProvider._ID1_;
            Client client = ArrangeProvider.GetClient(id: id);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientById(id)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.GetClientById(id);

            //Assert
            Assert.AreEqual(mapper.Map<ClientDto>(client), result);
        }

        [TestMethod]
        public void ClientsService_GetClientByName_NonExistingName_ReturnsNull()
        {
            //Arrange
            string name = ArrangeProvider._NAME_;
            Client client = ArrangeProvider.GetClient(email: name);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientByName(name)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.GetClientByName(ArrangeProvider._NAME2_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ClientsService_GetClientById_NonExistingId_ReturnsNull()
        {
            //Arrange
            string id = ArrangeProvider._ID1_;
            Client client = ArrangeProvider.GetClient(id: id);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientById(id)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.GetClientById(ArrangeProvider._ID0_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ClientsService_Authenticate_ExistingEmail_ReturnsValidJWDToken()
        {
            //Arrange
            string email = ArrangeProvider._EMAIL_;
            Client client = ArrangeProvider.GetClient(id: email);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientByEmail(email)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.Authenticate(ArrangeProvider._EMAIL_);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(result.Token));
        }

        [TestMethod]
        public void ClientsService_Authenticate_NonExistingEmail_ReturnsNull()
        {
            //Arrange
            string email = ArrangeProvider._EMAIL_;
            Client client = ArrangeProvider.GetClient(id: email);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientByEmail(email)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.Authenticate(ArrangeProvider._EMAIL2_);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ClientsService_Authenticate_ExistingEmailAndSpecificRole_ReturnsValidJWDTokenWithSameRole()
        {
            //Arrange
            string email = ArrangeProvider._EMAIL_;
            string role = Role.Admin;
            Client client = ArrangeProvider.GetClient(id: email, role: role);

            var mockRepo = new Mock<IClientsRepository>();
            mockRepo.Setup(x => x.GetClientByEmail(email)).Returns(client);

            var options = ArrangeProvider.GetAppSettingslOption();
            var mapper = ArrangeProvider.GetMapper();

            var service = new ClientsService(options, mapper, mockRepo.Object);

            //Act
            var result = service.Authenticate(ArrangeProvider._EMAIL_);

            //Assert
            Assert.AreEqual(role, result.Role);
        }

        [TestMethod]
        public void PoliciesService_GetPoliciesByClientId_ExistingClientIdWithPolicies_ReturnPolicies()
        {
            //Arrange
            var id = ArrangeProvider._ID0_;
            var email = ArrangeProvider._EMAIL_;

            var rootPolicy = ArrangeProvider.GetRootPolicy();
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("1", email, id, 1.1));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("2", email, id, 2.2));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("3", email, id, 3.3));

            var mockPoliciesRepository = new Mock<IPoliciesRepository>();
            mockPoliciesRepository.Setup(x => x.GetPoliciesByClientId(id)).Returns(rootPolicy.Policies);

            var mapper = ArrangeProvider.GetMapper();

            var service = new PoliciesService(mapper, mockPoliciesRepository.Object);

            var expected = rootPolicy.Policies.Select(p => mapper.Map<PolicyDto>(p)).ToList();

            //Act
            var result = service.GetPoliciesByClientId(id);
            
            //Assert
            CollectionAssert.AreEquivalent((System.Collections.ICollection)(expected), (System.Collections.ICollection)result);
        }

        [TestMethod]
        public void PoliciesService_GetPoliciesByClientId_NonExistingClientId_ReturnNull()
        {
            //Arrange
            var id = ArrangeProvider._ID0_;
            var email = ArrangeProvider._EMAIL_;

            var rootPolicy = ArrangeProvider.GetRootPolicy();
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("1", email, id, 1.1));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("2", email, id, 2.2));
            rootPolicy.Policies.Add(ArrangeProvider.GetPolicy("3", email, id, 3.3));

            var mockPoliciesRepository = new Mock<IPoliciesRepository>();
            mockPoliciesRepository.Setup(x => x.GetPoliciesByClientId(id)).Returns(rootPolicy.Policies);

            var mapper = ArrangeProvider.GetMapper();

            var service = new PoliciesService(mapper, mockPoliciesRepository.Object);

            //Act
            var result = service.GetPoliciesByClientId(ArrangeProvider._ID1_);

            //Assert
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void PoliciesService_GetPolicyById_ExistingPolicyId_ReturnPolicy()
        {
            //Arrange
            var id = ArrangeProvider._ID0_;

            var policy = ArrangeProvider.GetPolicy(id: id);

            var mockPoliciesRepository = new Mock<IPoliciesRepository>();
            mockPoliciesRepository.Setup(x => x.GetPolicyById(id)).Returns(policy);

            var mapper = ArrangeProvider.GetMapper();

            var service = new PoliciesService(mapper, mockPoliciesRepository.Object);

            //Act
            var result = service.GetPolicyById(id);

            //Assert
            Assert.AreEqual(mapper.Map<PolicyDto>(policy), result);
        }

        [TestMethod]
        public void PoliciesService_GetPolicyById_NonExistingExistingPolicyId_ReturnNull()
        {
            //Arrange
            var id = ArrangeProvider._ID0_;

            var policy = ArrangeProvider.GetPolicy(id: id);

            var mockPoliciesRepository = new Mock<IPoliciesRepository>();
            mockPoliciesRepository.Setup(x => x.GetPolicyById(id)).Returns(policy);

            var mapper = ArrangeProvider.GetMapper();

            var service = new PoliciesService(mapper, mockPoliciesRepository.Object);

            //Act
            var result = service.GetPolicyById(ArrangeProvider._ID1_);

            //Assert
            Assert.IsNull(result);
        }
    }
}

