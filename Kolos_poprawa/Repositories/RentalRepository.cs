using Kolos_poprawa.Models;
using Microsoft.Data.SqlClient;

namespace Kolos_poprawa.Repositories;

public class RentalRepository : IRentalRepository
{
    private IConfiguration _configuration;

    private string GetClientsCommand = @"Select clients.Id, clients.FirstName, LastName, clients.""Address"", vin, colors.""Name"", ""models"".""Name"", DateFrom, DateTo, TotalPrice, car_rentals.ID
From clients inner join car_rentals on clients.ID = car_rentals.ClientID
inner join cars on car_rentals.CarID = cars.ID
inner join colors on cars.ColorID = colors.ID
inner join models on cars.ModelID = models.ID
Where clients.Id = @id";

    private string DoCarExists = "Select 1 From cars Where cars.ID = @id";

    private string CarPriceRate = "Select cars.PricePerDay From cars where cars.Id = @id";

    private string AddClientCommand = "Insert into clients (ID, FirstName, LastName, \"Address\") values (@id, @f, @l, @a);";
    
    public RentalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<ClientsRentalDTO>> GetClientRental(int clientId)
    {
        List<ClientsRentalDTO> result = new List<ClientsRentalDTO>();
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(GetClientsCommand, connection))
            {
                command.Parameters.AddWithValue("@id", clientId);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        ClientsRentalDTO rentalDto = new ClientsRentalDTO()
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Adress = reader.GetString(3),
                            Vin = reader.GetString(4),
                            Color = reader.GetString(5),
                            Model = reader.GetString(6),
                            DateFrom = reader.GetDateTime(7),
                            DateTo = reader.GetDateTime(8),
                            TotalPrice = reader.GetInt32(9),
                            RentalId = reader.GetInt32(10)
                        };
                        result.Add(rentalDto);
                    }
                }
            }
        }
        return result;
    }

    public async Task<bool> AddClient(ClientDTO clientDto)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
          
            using (SqlCommand command = new SqlCommand(AddClientCommand, connection))
            {
                command.Parameters.AddWithValue("@id",await GetMaxClientId() + 1);
                command.Parameters.AddWithValue("@n", clientDto.FirstName);
                command.Parameters.AddWithValue("@l", clientDto.LastName);
                command.Parameters.AddWithValue("a", clientDto.Adress);
                
                var result = command.ExecuteNonQuery();
                return true;
            }
            
        }
    }
    
    

    public async Task<bool> CarExists(int carId)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(DoCarExists, connection))
            {
                command.Parameters.AddWithValue("@Id", carId);
                
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    

    public async Task<int> GetCarPriceRate(int carId)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(CarPriceRate, connection))
            {
                var result = command.ExecuteScalar();
                
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    public async Task<int> GetMaxClientId()
    {
        string commnadReadMaxInt = "Select Max(id) from clients;";
        
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(commnadReadMaxInt, connection))
            {
                var result = command.ExecuteScalar();
                
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // lub inna wartość domyślna
                }
            }
        }
    }
    
}