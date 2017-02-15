namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfigurationVariant
    {
        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool DefaultVariant { get; set; }

        public long? MaterialItemId { get; set; }

        public int Valid { get; set; }

        public int? Revision { get; set; }

        public long PersonId { get; set; }

        public bool IsHidden { get; set; }

        public virtual MaterialItem MaterialItem { get; set; }
    }
}
