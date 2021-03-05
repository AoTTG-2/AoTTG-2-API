using System;
using System.Collections.Generic;
using AoTTG2.IDS.Data.Dao;
using Microsoft.AspNetCore.Identity;

namespace AoTTG2.IDS.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid DisplayBadgeId { get; set; }
        public UserBadgeDao DisplayBadge { get; set; }
        public ICollection<UserBadgeDao> Badges { get; set; }
    }
}
