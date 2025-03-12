using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class OwnersController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public OwnersController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Owner>>> GetAll()
    {
        return await _context.Owner.ToListAsync();
    }

    [HttpGet("owners/{id}")]
    public async Task<ActionResult<Owner>> GetById(int id)
    {
        var owner = await _context.Owner.FindAsync(id);
        if (owner == null) return NotFound();
        return owner;
    }

    [HttpPost]
    public async Task<ActionResult<Owner>> Create(Owner owner)
    {
        _context.Owner.Add(owner);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = owner.OwnerId }, owner);
    }

    [HttpPut("owners/{id}")]
    public async Task<IActionResult> Update(int id, Owner owner)
    {
        if (id != owner.OwnerId) return BadRequest();
        _context.Entry(owner).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("owners/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var owner = await _context.Owner.FindAsync(id);
        if (owner == null) return NotFound();
        _context.Owner.Remove(owner);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}