using AoTTG2.IDS.Controllers.NSwag;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Controllers
{
    public class BadgeController : BadgeControllerBase
    {
        public override Task<ActionResult<UserBadgePaged>> GetUserBadges(Guid? userId = null, int? page = 0, IEnumerable<SearchQuery> searchQuery = null, string sortColumn = null,
            SortDirection? sortDirection = null)
        {
            throw new NotImplementedException();
        }
    }
}
