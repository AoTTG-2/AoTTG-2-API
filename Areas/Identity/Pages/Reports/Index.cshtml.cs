using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Pages.Reports
{
    [Authorize(Roles = Roles.AdminOrModerator)]
    public class IndexModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public IndexModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ReportDao> ReportDao { get;set; }

        public async Task OnGetAsync()
        {
            ReportDao = await _context.Reports.ToListAsync();
        }
    }
}
