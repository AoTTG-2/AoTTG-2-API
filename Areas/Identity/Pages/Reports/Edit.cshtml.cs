using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AoTTG2.IDS.Data;
using AoTTG2.IDS.Data.Dao;

namespace AoTTG2.IDS.Pages.Reports
{
    public class EditModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public EditModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReportDao ReportDao { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportDao = await _context.Reports.FirstOrDefaultAsync(m => m.Id == id);

            if (ReportDao == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ReportDao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportDaoExists(ReportDao.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportDaoExists(long id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
