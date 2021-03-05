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
    public class IndexModel : PageModel
    {
        private readonly AoTTG2.IDS.Data.ApplicationDbContext _context;

        public IndexModel(AoTTG2.IDS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BadgeDao> BadgeDao { get;set; }

        public async Task OnGetAsync()
        {
            BadgeDao = await _context.Badges.ToListAsync();
        }
    }
}
