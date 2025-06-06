using Kolos_poprawa.Models;

namespace Kolos_poprawa.Repositories;

public interface IRentalRepository
{
    Task<List<ClientsRentalDTO>> GetClientRental(int clientId);
    Task<bool> AddClient(ClientDTO clientDto);

    Task<bool> AddRental(int clientId, int carId, DateTime from, DateTime to, int price);
    Task<bool> CarExists(int carId);
    Task<int> GetMaxFrom(string table, string column);
    Task<int> GetCarPriceRate(int carId);
    
}