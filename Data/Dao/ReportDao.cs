using AoTTG2.IDS.Controllers.NSwag;
using System;
using System.ComponentModel.DataAnnotations;

namespace AoTTG2.IDS.Data.Dao
{
    public class ReportDao : Entity
    {
        public Guid EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public Guid SenderId { get; set; }
        public ReportType Type { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }

        public ReportStatus Status { get; set; }
        [StringLength(1500)]
        public string Note { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
