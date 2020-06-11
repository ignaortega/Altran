using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Entities;
using AltranExercise.Service.Mapping;
using AutoMapper;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AltranExercise.Test
{
    internal static class ArrangeProvider
    {
        public const string _URL_ = "http://someserivce.com/json";
        public const string _EMAIL_ = "client@company.com";
        public const string _EMAIL2_ = "provider@company.com";
        public const string _NAME_ = "Client";
        public const string _NAME2_ = "Unknown";
        public const string _ID0_ = "00000000-0000-0000-0000-000000000000";
        public const string _ID1_ = "11111111-1111-1111-1111-111111111111";
        public const double _AMOUNT_INSURED_ = 11.11;
        public const string _INCEPTION_DATE_ = "01/01/2020";
        public const bool _INSTALLMENT_PAYMENT_ = true;
        public const string _SECRET_ = "50m357.-1N970|_|5345|<3`/|=0.-4|_7.-4NB|_|717|)035N07.-34|_|_`/M4773.-";

        public static IOptions<ResourcesUrlsOption> GetResourcesUrlOption(string clientsURL, string policiesURL)
        {
            var resourcesUrls = new ResourcesUrlsOption
            {
                ClientsURL = clientsURL,
                PoliciesURL = policiesURL
            };

            return Options.Create(resourcesUrls);
        }

        public static IOptions<AppSettingsOption> GetAppSettingslOption(string secret = null)
        {
            var appSettingsOption = new AppSettingsOption
            {
                Secret = string.IsNullOrEmpty(secret) ? _SECRET_ : secret
            };

            return Options.Create(appSettingsOption);
        }

        public static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            return mapper;
        }

        public static HttpClient GetMockJsonHttpClient(string url, string jsonString)
        {
            var mockHttpMessageHandler = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttpMessageHandler.When(url).Respond("application/json", jsonString); // Respond with JSON

            // Inject the handler or client into your application code
            var mockHttpClient = mockHttpMessageHandler.ToHttpClient();

            return mockHttpClient;
        }

        public static RootClient GetRootClient(Client client)
        {
            var rootClient = new RootClient();
            rootClient.Clients = new List<Client>();
            rootClient.Clients.Add(client);

            return rootClient;
        }

        public static Client GetClient(string id = "", string name = "", string email = "", string role = "")
        {
            return new Client
            {
                Id = string.IsNullOrEmpty(id) ? _ID1_ : id,
                Name = string.IsNullOrEmpty(name) ? _NAME_ : name,
                Email = string.IsNullOrEmpty(email) ? _EMAIL_ : email,
                Role = string.IsNullOrEmpty(role) ? Role.User : role
            };
        }

        public static RootPolicy GetRootPolicy()
        {
            var rootPolicy = new RootPolicy();
            rootPolicy.Policies = new List<Policy>();

            return rootPolicy;
        }

        public static Policy GetPolicy(string id = "",
            string email = "",
            string clientId = "",
            double? amountInsured = null,
            DateTime? inceptionDate = null,
            bool? installmentPayment = null)
        {
            return new Policy
            {
                Id = string.IsNullOrEmpty(id) ? _ID1_ : id,
                AmountInsured = amountInsured.HasValue ? amountInsured.Value : _AMOUNT_INSURED_,
                Email = string.IsNullOrEmpty(email) ? _EMAIL_ : email,
                InceptionDate = inceptionDate.HasValue ? inceptionDate.Value : DateTime.Parse(_INCEPTION_DATE_),
                InstallmentPayment = installmentPayment.HasValue ? installmentPayment.Value : true,
                ClientId = string.IsNullOrEmpty(clientId) ? _ID1_ : clientId
            };
        }
    }
}
