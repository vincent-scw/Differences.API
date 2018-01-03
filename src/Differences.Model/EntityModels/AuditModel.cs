using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Differences.Interaction.EntityModels
{
    public abstract class AuditModel
    {
        public AuditModel()
        {
            CreateTime = DateTime.Now;
        }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime? LastUpdateTime { get; set; }
        public Guid? LastUpdatedBy { get; set; }
    }
}
