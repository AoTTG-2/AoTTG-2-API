using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AoTTG2.IDS.Data;
using AoTTG2.IDS.Data.Dao;

namespace AoTTG2.IDS.Pages.Reports
{
    public class CreateModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public CreateModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReportDao ReportDao { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reports.Add(ReportDao);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
