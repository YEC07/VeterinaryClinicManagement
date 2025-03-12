using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class InspectionsController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public InspectionsController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inspection>>> GetAll()
    {
        return await _context.Inspections
            .Include(i => i.Pet)
            .Include(i => i.Staff)
            .ToListAsync();
    }

    [HttpGet("inspections/{id}")]
    public async Task<ActionResult<Inspection>> GetById(int id)
    {
        var inspection = await _context.Inspections
            .Include(i => i.Pet)
            .Include(i => i.Staff)
            .FirstOrDefaultAsync(i => i.InspectionId == id);

        if (inspection == null) return NotFound();
        return inspection;
    }

    [HttpPost]
    public async Task<ActionResult<Inspection>> Create(Inspection inspection)
    {
        _context.Inspections.Add(inspection);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = inspection.InspectionId }, inspection);
    }

    [HttpPut("inspections/{id}")]
    public async Task<IActionResult> Update(int id, Inspection inspection)
    {
        if (id != inspection.InspectionId) return BadRequest();
        _context.Entry(inspection).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("inspections/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var inspection = await _context.Inspections.FindAsync(id);
        if (inspection == null) return NotFound();
        _context.Inspections.Remove(inspection);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}