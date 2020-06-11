using AltranExercise.Data.Entities;

namespace AltranExercise.Data.Repositories
{
    public interface IClientsRepository
    {
        Client GetClientById(string clientId);
        Client GetClientByEmail(string clientEmail);
        Client GetClientByName(string clientName);
    }
}
