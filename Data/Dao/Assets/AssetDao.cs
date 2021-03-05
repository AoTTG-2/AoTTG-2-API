using AoTTG2.IDS.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AoTTG2.IDS.Data.Dao.Assets
{
    public abstract class AssetDao : Entity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public uint Endorsement { get; set; }

        /// <summary>
        /// OwnerId is optional, as due to data migration, the owner of the asset may not have an account yet
        /// </summary>
        public Guid? OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        /// <summary>
        /// The name of the owner who does not have an AoTTG2 account (Assets before 2021). Should not be used when there's an OwnerId.
        /// </summary>
        [StringLength(100)]
        public string LegacyOwner { get; set; }
    }
}
