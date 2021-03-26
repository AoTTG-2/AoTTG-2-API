using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Data.Repositories
{
    public class ReportRepository : BaseRepository<ReportDao>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
