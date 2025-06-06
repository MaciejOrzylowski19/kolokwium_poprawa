using Kolos_poprawa.Models;

namespace Kolos_poprawa.Repositories;

public interface IRentalRepository
{
    Task<List<ClientsRentalDTO>> GetClientRental(int clientId);
    Task<bool> AddClient(ClientDTO clientDto);
    Task<bool> CarExists(int carId);
    Task<int> GetCarPriceRate(int carId);
}