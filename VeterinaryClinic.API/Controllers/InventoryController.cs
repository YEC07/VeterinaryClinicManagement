using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class InventoryController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public InventoryController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetAll()
    {
        return await _context.Inventory.ToListAsync();
    }

    [HttpGet("inventory/{id}")]
    public async Task<ActionResult<Inventory>> GetById(int id)
    {
        var inventory = await _context.Inventory.FindAsync(id);
        if (inventory == null) return NotFound();
        return inventory;
    }

    [HttpPost]
    public async Task<ActionResult<Inventory>> Create(Inventory inventory)
    {
        _context.Inventory.Add(inventory);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = inventory.ItemId }, inventory);
    }

    [HttpPut("inventory/{id}")]
    public async Task<IActionResult> Update(int id, Inventory inventory)
    {
        if (id != inventory.ItemId) return BadRequest();
        _context.Entry(inventory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("inventory/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var inventory = await _context.Inventory.FindAsync(id);
        if (inventory == null) return NotFound();
        _context.Inventory.Remove(inventory);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}