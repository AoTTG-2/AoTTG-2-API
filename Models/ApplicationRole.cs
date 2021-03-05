using Microsoft.AspNetCore.Identity;
using System;

namespace AoTTG2.IDS.Models
{
    public sealed class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public ApplicationRole(string roleName)
        {
            Name = roleName;
        }
    }
}
