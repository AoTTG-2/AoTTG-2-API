using AoTTG2.IDS.Data.Dao;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Data.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task AddAsync(ReportDao report);
    }
}
