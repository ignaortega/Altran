using AltranExercise.Common.Infraestructure;
using AltranExercise.Service.DTOs;

namespace AltranExercise.Service.Services
{
    public interface IClientsService
    {
        ClientDto GetClientById(string clientId);
        ClientDto GetClientByName(string clientName);
        AuthenticationTokenDto Authenticate(string clientEmail);
    }
}
