namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaterialItemProperty
    {
        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        public long MaterialClassId { get; set; }

        [StringLength(255)]
        public string Value { get; set; }

        public bool? IsValid { get; set; }

        public long MaterialItemId { get; set; }

        public long? InheritedFrom { get; set; }

        public virtual MaterialClass MaterialClass { get; set; }

        public virtual MaterialItem MaterialItem { get; set; }
    }
}
