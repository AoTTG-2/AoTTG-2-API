using AoTTG2.IDS.Controllers.NSwag;
using AoTTG2.IDS.Services.Interfaces;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Controllers
{
    [Authorize]
    public class ReportController : ReportControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public override async Task<IActionResult> AddReport(Report body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            body.SenderId = Guid.Parse(User.GetSubjectId());
            var result = await _reportService.AddAsync(body);
            return CreatedAtAction(nameof(AddReport), result);
        }
    }
}
