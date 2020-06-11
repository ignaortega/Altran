using AltranExercise.Data.Entities;
using System.Collections.Generic;

namespace AltranExercise.Data.Repositories
{
    public interface IPoliciesRepository
    {
        IList<Policy> GetPoliciesByClientId(string clientId);
        Policy GetPolicyById(string policyId);
    }
}
