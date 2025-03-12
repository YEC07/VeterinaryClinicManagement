using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.Data;

namespace VeterinaryClinic.API.Controllers;

public class VaccinationsController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public VaccinationsController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vaccination>>> GetAll()
    {
        return await _context.Vaccinations
            .Include(v => v.Pet)
            .Include(v => v.Staff)
            .ToListAsync();
    }

    [HttpGet("vaccinations/{id}")]
    public async Task<ActionResult<Vaccination>> GetById(int id)
    {
        var vaccination = await _context.Vaccinations
            .Include(v => v.Pet)
            .Include(v => v.Staff)
            .FirstOrDefaultAsync(v => v.VaccinationId == id);

        if (vaccination == null) return NotFound();
        return vaccination;
    }

    [HttpPost]
    public async Task<ActionResult<Vaccination>> Create(Vaccination vaccination)
    {
        _context.Vaccinations.Add(vaccination);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = vaccination.VaccinationId }, vaccination);
    }

    [HttpPut("vaccinations/{id}")]
    public async Task<IActionResult> Update(int id, Vaccination vaccination)
    {
        if (id != vaccination.VaccinationId) return BadRequest();
        _context.Entry(vaccination).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("vaccinations/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vaccination = await _context.Vaccinations.FindAsync(id);
        if (vaccination == null) return NotFound();
        _context.Vaccinations.Remove(vaccination);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}