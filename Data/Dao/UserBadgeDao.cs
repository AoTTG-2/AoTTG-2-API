using AoTTG2.IDS.Models;
using System;

namespace AoTTG2.IDS.Data.Dao
{
    public class UserBadgeDao
    {
        public Guid UserId { get; set; }
        public Guid BadgeId { get; set; }

        public ApplicationUser User { get; set; }
        public BadgeDao Badge { get; set; }

        public DateTime ObtainedDate { get; set; }
    }
}
