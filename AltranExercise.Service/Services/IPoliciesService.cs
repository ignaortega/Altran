using AltranExercise.Service.DTOs;
using System.Collections.Generic;

namespace AltranExercise.Service.Services
{
    public interface IPoliciesService
    {
        IList<PolicyDto> GetPoliciesByClientId(string clientId);
        PolicyDto GetPolicyById(string policyId);
    }
}
