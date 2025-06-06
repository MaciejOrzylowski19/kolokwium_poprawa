namespace Kolos_poprawa.Models;

public class ClientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Adress { get; set; }
    public List<RentalDTO> Rentals { get; set; }
}