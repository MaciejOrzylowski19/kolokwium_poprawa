using Kolos_poprawa.Models;

namespace Kolos_poprawa.Services;

public interface IRentalService
{
    Task<ClientDTO> GetClientRentals(int clientId);
    Task<int> AddClient(NewClientRentDTO rental);
}