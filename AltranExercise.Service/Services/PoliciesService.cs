using AltranExercise.Data.Repositories;
using AltranExercise.Service.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AltranExercise.Service.Services
{
    public class PoliciesService : IPoliciesService
    {
        private readonly IPoliciesRepository _repository;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        public PoliciesService(IMapper mapper, IPoliciesRepository repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IList<PolicyDto> GetPoliciesByClientId(string clientId)
        {
            var result = this._repository.GetPoliciesByClientId(clientId);

            if(result == null)
            {
                return new List<PolicyDto>();
            }
            
            return result.Select(p => this._mapper.Map<PolicyDto>(p)).ToList();
        }

        public PolicyDto GetPolicyById(string policyId)
        {
            var result = this._repository.GetPolicyById(policyId);

            return this._mapper.Map<PolicyDto>(result);
        }
    }
}
