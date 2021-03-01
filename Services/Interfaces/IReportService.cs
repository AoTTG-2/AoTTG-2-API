using AoTTG2.IDS.Controllers.NSwag;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Services.Interfaces
{
    public interface IReportService
    {
        Task<Report> AddAsync(Report report);
    }
}
