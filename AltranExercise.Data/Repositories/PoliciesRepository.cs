using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Entities;
using AltranExercise.Data.Infraestructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AltranExercise.Data.Repositories
{
    public class PoliciesRepository : IPoliciesRepository
    {
        private readonly string _resourceUrl;
        private readonly HttpClient _httpClient;

        public PoliciesRepository(IOptions<ResourcesUrlsOption> options, HttpClient httpClient)
        {
            this._resourceUrl = options.Value.PoliciesURL;
            this._httpClient = httpClient;
        }

        public IList<Policy> GetPoliciesByClientId(string clientId)
        {
            var result = JSonResourceAsyncReader
                .GetResourceAsync<RootPolicy>(this._resourceUrl, this._httpClient).Result.Policies
                .Where(p => p.ClientId == clientId)
                .ToList();

            return result;
        }

        public Policy GetPolicyById(string policyId)
        {
            var result = JSonResourceAsyncReader
                .GetResourceAsync<RootPolicy>(this._resourceUrl, this._httpClient).Result.Policies
                .Where(p => p.Id == policyId)
                .FirstOrDefault();

            return result;
        }
    }
}
