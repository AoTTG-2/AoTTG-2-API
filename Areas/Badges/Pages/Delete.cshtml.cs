using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AoTTG2.IDS.Data;
using AoTTG2.IDS.Data.Dao;

namespace AoTTG2.IDS.Areas.Badges
{
    public class DeleteModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public DeleteModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BadgeDao BadgeDao { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BadgeDao = await _context.Badges.FirstOrDefaultAsync(m => m.Id == id);

            if (BadgeDao == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BadgeDao = await _context.Badges.FindAsync(id);

            if (BadgeDao != null)
            {
                _context.Badges.Remove(BadgeDao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
