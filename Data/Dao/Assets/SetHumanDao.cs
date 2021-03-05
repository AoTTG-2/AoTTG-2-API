namespace AoTTG2.IDS.Data.Dao.Assets
{
    public class SetHumanDao : SetDao
    {
        public SkinDao Hair { get; set; }
        public SkinDao Skin { get; set; }
        public SkinDao Eyes { get; set; }
        public SkinDao Glasses { get; set; }
        public SkinDao Facial { get; set; }
        public SkinDao Outfit { get; set; }
        public SkinDao Cape { get; set; }
        public SkinDao Emblem { get; set; }
    }
}
