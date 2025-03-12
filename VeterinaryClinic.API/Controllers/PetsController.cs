using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class PetsController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public PetsController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
    {
        return await _context.Pets.Include(p => p.Owner).ToListAsync();
    }

    [HttpGet("pets/{id}")]
    public async Task<ActionResult<Pet>> GetById(int id)
    {
        var pet = await _context.Pets
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.PetId == id);
            
        if (pet == null) return NotFound();
        return pet;
    }

    [HttpPost]
    public async Task<ActionResult<Pet>> Create(Pet pet)
    {
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = pet.PetId }, pet);
    }

    [HttpPut("pets/{id}")]
    public async Task<IActionResult> Update(int id, Pet pet)
    {
        if (id != pet.PetId) return BadRequest();
        _context.Entry(pet).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("pets/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null) return NotFound();
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}