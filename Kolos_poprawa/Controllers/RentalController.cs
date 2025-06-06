using Kolos_poprawa.Models;
using Kolos_poprawa.Repositories;
using Kolos_poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolos_poprawa.Controllers;

[ApiController]
[Route("api")]
public class RentalController : ControllerBase
{
    private IRentalService _rentalService;

    public RentalController(IRentalService service)
    {
        _rentalService = service;
    }
    
    [HttpGet]
    [Route("clients/{clientId}")]
    public async Task<IActionResult> GetClients([FromRoute] int clientId)
    {
        try
        {
            return Ok(await _rentalService.GetClientRentals(clientId));
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Route("clients")]
    public async Task<IActionResult> AddRental([FromBody] NewClientRentDTO clientRent)
    {

        int r = await _rentalService.AddClient(clientRent);
        
        return Ok(r);
    }
    
    
}