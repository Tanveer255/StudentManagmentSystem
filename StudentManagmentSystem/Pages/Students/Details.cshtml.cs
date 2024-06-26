﻿using StudentManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace StudentManagmentSystem.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly StudentManagmentSystem.Data.SchoolContext _context;

    public DetailsModel(StudentManagmentSystem.Data.SchoolContext context)
    {
        _context = context;
    }

    public Student Student { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Student = await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Student == null)
        {
            return NotFound();
        }
        return Page();
    }
}
