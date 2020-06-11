using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Entities;
using AltranExercise.Data.Infraestructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http;

namespace AltranExercise.Data.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly string _resourceUrl;
        private readonly HttpClient _httpClient;

        public ClientsRepository(IOptions<ResourcesUrlsOption> options, HttpClient httpClient)
        {
            this._resourceUrl = options.Value.ClientsURL;
            this._httpClient = httpClient;
        }

        public Client GetClientByEmail(string clientEmail)
        {
            var result = JSonResourceAsyncReader
                .GetResourceAsync<RootClient>(this._resourceUrl, this._httpClient).Result.Clients
                .Where(c => c.Email == clientEmail)
                .FirstOrDefault();

            return result;
        }

        public Client GetClientById(string clientId)
        {
            var result = JSonResourceAsyncReader
                .GetResourceAsync<RootClient>(this._resourceUrl, this._httpClient).Result.Clients
                .Where(c => c.Id == clientId)
                .FirstOrDefault();

            return result;
        }

        public Client GetClientByName(string clientName)
        {
            var result = JSonResourceAsyncReader
                .GetResourceAsync<RootClient>(this._resourceUrl, this._httpClient).Result.Clients
                .Where(c => c.Name == clientName)
                .FirstOrDefault();

            return result;
        }
    }
}
