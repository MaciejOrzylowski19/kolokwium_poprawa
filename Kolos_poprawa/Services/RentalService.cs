using Kolos_poprawa.Models;
using Kolos_poprawa.Repositories;

namespace Kolos_poprawa.Services;

public class RentalService : IRentalService
{

    IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository repository)
    {
        _rentalRepository = repository;
    }
    
    public async Task<ClientDTO> GetClientRentals(int clientId)
    {

        List<ClientsRentalDTO> clientsRentalDtos = await _rentalRepository.GetClientRental(clientId);

        if (clientsRentalDtos.Count == 0)
        {
            throw new Exception();
        }
        
        ClientDTO clientDto = new ClientDTO()
        {
            Id = clientsRentalDtos[0].Id,
            FirstName = clientsRentalDtos[0].FirstName,
            LastName = clientsRentalDtos[0].LastName,
            Adress = clientsRentalDtos[0].Adress,
            Rentals = new List<RentalDTO>()
        };
        foreach (var rental in clientsRentalDtos)
        {
            RentalDTO rentalDto = new RentalDTO()
            {
                Color = rental.Color,
                Model = rental.Model,
                Vin = rental.Vin,
                DateFrom = rental.DateFrom,
                DateTo = rental.DateTo,
                TotalPrice = rental.TotalPrice
            };
            clientDto.Rentals.Add(rentalDto);
        }

        return clientDto;

    }

    public async Task<int> AddClient(NewClientRentDTO rental)
    {

        if (! await _rentalRepository.CarExists(rental.CarId))
        {
            return -1;
        }

        if (rental.DateFrom > rental.DateTo)
        {
            return -2;
        }

        int days = (rental.DateTo - rental.DateFrom).Days;
        
        Console.WriteLine(days);
        return 1;
    }
    
}