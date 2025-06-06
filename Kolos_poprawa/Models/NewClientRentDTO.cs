namespace Kolos_poprawa.Models;

public class NewClientRentDTO
{
    
    public NewClientDTO Client { get; set; }
    public int CarId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
}