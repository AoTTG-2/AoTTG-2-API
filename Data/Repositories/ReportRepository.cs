using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ReportDao report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }
    }
}
