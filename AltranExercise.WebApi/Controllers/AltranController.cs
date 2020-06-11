using AltranExercise.Common.Infraestructure;
using AltranExercise.Service.DTOs;
using AltranExercise.Service.Services;
using AltranExercise.WebApi.Infraestructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AltranExercise.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class AltranController : Controller
    {
        private readonly IClientsService _clientService;
        private readonly IPoliciesService _policiesService;

        public AltranController(IClientsService clientsService, IPoliciesService policiesService)
        {
            _clientService = clientsService;
            _policiesService = policiesService;
        }

        [AuthorizeRoles(Role.Admin, Role.User)]
        [HttpGet("client/id/{clientId}")]
        public ActionResult<ClientDto> GetClientById(string clientId)
        {
            var client = this._clientService.GetClientById(clientId);

            if (client != null)
            {
                return Ok(client);
            }

            return NoContent();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("client/name/{clientName}")]
        public ActionResult<ClientDto> GetClientByName(string clientName)
        {
            var client = this._clientService.GetClientByName(clientName);

            if (client != null)
            {
                return Ok(client);
            }

            return NoContent();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("client/policy/{policyId}")]
        public ActionResult<ClientDto> GetClientPolicyById(string policyId)
        {
            var policy = this._policiesService.GetPolicyById(policyId);

            if (policy != null)
            {
                var client = this._clientService.GetClientById(policy.ClientId);

                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return BadRequest(new { message = "Policy id is incorrect" });
            }
        }

        [AuthorizeRoles(Role.Admin, Role.User)]
        [HttpGet("policy/name/{clientName}")]
        public ActionResult<ClientDto> GetPoliciesByName(string clientName)
        {
            var client = this._clientService.GetClientByName(clientName);

            if (client != null)
            {
                var policies = this._policiesService.GetPoliciesByClientId(client.Id);

                if (policies != null && policies.Count > 0)
                {
                    return Ok(policies);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return BadRequest(new { message = "Client name is incorrect" });
            }
        }

        [AllowAnonymous]
        [HttpPost("client/authenticate/")]
        public ActionResult<AuthenticationTokenDto> Authenticate([FromBody] string clientEmail)
        {
            var authenticationTokenDto = this._clientService.Authenticate(clientEmail);

            if (authenticationTokenDto != null)
            {
                return Ok(authenticationTokenDto);
            }

            return BadRequest(new { message = "Email is incorrect" });
        }
    }
}
