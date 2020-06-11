using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Entities;
using AltranExercise.Service.DTOs;
using AltranExercise.Service.Services;
using AltranExercise.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AltranExercise.Test
{
    [TestClass]
    public class WebApiLayerTest
    {
        [TestMethod]
        public void AltranController_GetClientById_NonExistingClientId_ReturnsClient()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(id: id));
            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientById(id)).Returns(clientDto);
            var mockPolicyService = new Mock<IPoliciesService>();


            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientById(id);

            //Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(clientDto, result.Value);
        }

        [TestMethod]
        public void AltranController_GetClientById_NonExistingClientId_ReturnsNoContentResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(id: id));
            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientById(id)).Returns(clientDto);
            var mockPolicyService = new Mock<IPoliciesService>();


            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientById(ArrangeProvider._ID1_);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void AltranController_GetClientByName_ExistingClientName_ReturnsClient()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name));
            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientByName(name)).Returns(clientDto);
            var mockPolicyService = new Mock<IPoliciesService>();


            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientByName(name);

            //Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(clientDto, result.Value);
        }

        [TestMethod]
        public void AltranController_GetClientByName_NonExistingClientName_ReturnsNoContentResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name));
            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientByName(name)).Returns(clientDto);
            var mockPolicyService = new Mock<IPoliciesService>();


            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientByName(ArrangeProvider._NAME2_);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void AltranController_GetPoliciesByName_ExistingClientNameWithPolicies_ReturnsPolicies()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var email = ArrangeProvider._EMAIL_;
            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name, email: email, id: id));

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientByName(name)).Returns(clientDto);

            List<Policy> policies = new List<Policy>();
            policies.Add(ArrangeProvider.GetPolicy("1", email, id, 1.1));
            policies.Add(ArrangeProvider.GetPolicy("2", email, id, 2.2));
            policies.Add(ArrangeProvider.GetPolicy("3", email, id, 3.3));

            var policyDtos = policies.Select(p => mapper.Map<PolicyDto>(p)).ToList();

            var mockPolicyService = new Mock<IPoliciesService>();
            mockPolicyService.Setup(x => x.GetPoliciesByClientId(id)).Returns(policyDtos);

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetPoliciesByName(name);

            //Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDtos, result.Value);
        }

        [TestMethod]
        public void AltranController_GetPoliciesByName_ExistingClientNameWithoutPolicies_ReturnsNoContentResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var email = ArrangeProvider._EMAIL_;
            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name, email: email, id: id));

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientByName(name)).Returns(clientDto);

            List<Policy> policies = new List<Policy>();

            var policyDtos = policies.Select(p => mapper.Map<PolicyDto>(p)).ToList();

            var mockPolicyService = new Mock<IPoliciesService>();
            mockPolicyService.Setup(x => x.GetPoliciesByClientId(id)).Returns(policyDtos);

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetPoliciesByName(name);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void AltranController_GetPoliciesByName_NonExistingClientName_ReturnsBadRequestObjectResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var email = ArrangeProvider._EMAIL_;
            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name, email: email, id: id));

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientByName(name)).Returns(clientDto);


            var mockPolicyService = new Mock<IPoliciesService>();


            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetPoliciesByName(ArrangeProvider._NAME2_);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void AltranController_GetClientPolicyById_ExistingPolicyId_ReturnsClient()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var email = ArrangeProvider._EMAIL_;
            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name, email: email, id: id));

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientById(id)).Returns(clientDto);

            Policy policy = ArrangeProvider.GetPolicy("1", email, id, 1.1);

            var policyDto = mapper.Map<PolicyDto>(policy);

            var mockPolicyService = new Mock<IPoliciesService>();
            mockPolicyService.Setup(x => x.GetPolicyById(id)).Returns(policyDto);

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientPolicyById(id);

            //Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(clientDto, result.Value);
        }

        [TestMethod]
        public void AltranController_GetClientPolicyById_NonExistingPolicyId_ReturnsBadRequestResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var name = ArrangeProvider._NAME_;
            var email = ArrangeProvider._EMAIL_;
            var id = ArrangeProvider._ID0_;
            var clientDto = mapper.Map<ClientDto>(ArrangeProvider.GetClient(name: name, email: email, id: id));

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.GetClientById(id)).Returns(clientDto);

            Policy policy = ArrangeProvider.GetPolicy("1", email, id, 1.1);

            var policyDto = mapper.Map<PolicyDto>(policy);

            var mockPolicyService = new Mock<IPoliciesService>();
            mockPolicyService.Setup(x => x.GetPolicyById(id)).Returns(policyDto);

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientPolicyById(ArrangeProvider._ID1_);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void AltranController_GetClientPolicyById_NonExistingClientForPolicyId_ReturnsNoContentResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var id = ArrangeProvider._ID0_;

            var mockClientService = new Mock<IClientsService>();

            Policy policy = ArrangeProvider.GetPolicy(id: id);

            var policyDto = mapper.Map<PolicyDto>(policy);

            var mockPolicyService = new Mock<IPoliciesService>();
            mockPolicyService.Setup(x => x.GetPolicyById(id)).Returns(policyDto);

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.GetClientPolicyById(id);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void AltranController_Authenticate_ExistingEmails_ReturnsAuthenticationToken()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var email = ArrangeProvider._EMAIL_;

            var token = new AuthenticationTokenDto
            {
                Email = ArrangeProvider._EMAIL_,
                Role = Role.Admin,
                Token = "Token"
            };

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.Authenticate(email)).Returns(token);

            var mockPolicyService = new Mock<IPoliciesService>();

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.Authenticate(email);

            //Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(token.Email, ((result.Value) as AuthenticationTokenDto).Email);
            Assert.AreEqual(token.Role, ((result.Value) as AuthenticationTokenDto).Role);
            Assert.AreEqual(token.Token, ((result.Value) as AuthenticationTokenDto).Token);
        }

        [TestMethod]
        public void AltranController_Authenticate_NonExistingEmails_ReturnsBadRequestObjectResult()
        {
            //Arrange
            var mapper = ArrangeProvider.GetMapper();

            var email = ArrangeProvider._EMAIL_;

            var token = new AuthenticationTokenDto
            {
                Email = ArrangeProvider._EMAIL_,
                Role = Role.Admin,
                Token = "Token"
            };

            var mockClientService = new Mock<IClientsService>();
            mockClientService.Setup(x => x.Authenticate(email)).Returns(token);

            var mockPolicyService = new Mock<IPoliciesService>();

            var controller = new AltranController(mockClientService.Object, mockPolicyService.Object);

            // Act
            var actionResult = controller.Authenticate(ArrangeProvider._EMAIL2_);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }
    }
}
