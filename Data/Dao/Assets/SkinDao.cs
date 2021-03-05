using AoTTG2.IDS.Data.Dao.Enums;
using System.ComponentModel.DataAnnotations;

namespace AoTTG2.IDS.Data.Dao.Assets
{
    public class SkinDao : AssetDao
    {
        [Url]
        public string ImageUrl { get; set; }

        public SkinType Type { get; set; }

        /// <summary>
        /// An integer, which when converted to binary, can be used as a bitmask
        /// </summary>
        public uint CompatibleModels { get; set; }
    }
}
