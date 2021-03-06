﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyLocation002.Data;
using MyLocation002.Data.Models;
using System.Threading.Tasks;

namespace MyLocation002.Pages.Locations
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Locations = await _dbContext.Locations.Include(w => w.Category).SingleOrDefaultAsync(w => w.Id == id).ConfigureAwait(false);

            if (Locations == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public Location Locations { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Locations = await _dbContext.Locations.FindAsync(id).ConfigureAwait(false);

            if (Locations != null)
            {
                _dbContext.Locations.Remove(Locations);

                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                Message = "Item Was Deleted !";
            }

            return RedirectToPage("./Index");
        }
    }
}