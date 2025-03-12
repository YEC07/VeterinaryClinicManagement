using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class AnimalGroupsController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public AnimalGroupsController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnimalGroup>>> GetAll()
    {
        return await _context.AnimalGroups.Include(ag => ag.Pets).ToListAsync();
    }

    [HttpGet("animal-groups/{id}")]
    public async Task<ActionResult<AnimalGroup>> GetById(int id)
    {
        var animalGroup = await _context.AnimalGroups
            .Include(ag => ag.Pets)
            .FirstOrDefaultAsync(ag => ag.GroupId == id);

        if (animalGroup == null) return NotFound();
        return animalGroup;
    }

    [HttpPost]
    public async Task<ActionResult<AnimalGroup>> Create(AnimalGroup animalGroup)
    {
        _context.AnimalGroups.Add(animalGroup);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = animalGroup.GroupId }, animalGroup);
    }

    [HttpPut("animal-groups/{id}")]
    public async Task<IActionResult> Update(int id, AnimalGroup animalGroup)
    {
        if (id != animalGroup.GroupId) return BadRequest();
        _context.Entry(animalGroup).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("animal-groups/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var animalGroup = await _context.AnimalGroups.FindAsync(id);
        if (animalGroup == null) return NotFound();
        _context.AnimalGroups.Remove(animalGroup);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}