using System.Collections.Generic;

namespace AoTTG2.IDS.Security
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string Moderator = "Moderator";

        /// <summary>
        /// A convenient const
        /// </summary>
        public const string AdminOrModerator = Administrator + "," + Moderator;

        public static IEnumerable<string> GetConfigRoles =>
            new[]
            {
                Administrator,
                Moderator
            };
    }
}
