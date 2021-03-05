using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AoTTG2.IDS.Data.Dao
{
    public class BadgeDao : Entity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<UserBadgeDao> UserBadges { get; set; }
    }
}
