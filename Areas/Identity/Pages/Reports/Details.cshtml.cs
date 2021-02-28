using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AoTTG2.IDS.Data;
using AoTTG2.IDS.Data.Dao;

namespace AoTTG2.IDS.Pages.Reports
{
    public class DetailsModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public DetailsModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ReportDao ReportDao { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
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
    }
}
