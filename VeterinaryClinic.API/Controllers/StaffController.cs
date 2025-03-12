using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.API.Models;
//using VeterinaryClinic.API.Data;

namespace VeterinaryClinic.API.Controllers;

public class StaffController : ControllerBase
{
    private readonly VetClinicDbContext _context;

    public StaffController(VetClinicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> GetAll()
    {
        return await _context.Staff.ToListAsync();
    }

    [HttpGet("staff/{id}")]
    public async Task<ActionResult<Staff>> GetById(int id)
    {
        var staff = await _context.Staff.FindAsync(id);
        if (staff == null) return NotFound();
        return staff;
    }

    [HttpPost]
    public async Task<ActionResult<Staff>> Create(Staff staff)
    {
        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = staff.StaffId }, staff);
    }

    [HttpPut("staff/{id}")]
    public async Task<IActionResult> Update(int id, Staff staff)
    {
        if (id != staff.StaffId) return BadRequest();
        _context.Entry(staff).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("staff/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var staff = await _context.Staff.FindAsync(id);
        if (staff == null) return NotFound();
        _context.Staff.Remove(staff);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}