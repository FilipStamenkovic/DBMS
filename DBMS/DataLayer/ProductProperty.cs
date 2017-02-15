namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductProperty
    {
        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Value { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
