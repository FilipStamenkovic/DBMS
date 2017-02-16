namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductionOrderProperty
    {
        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(3000)]
        public string Value { get; set; }

        public long ProductionOrderId { get; set; }

        public virtual ProductionOrder ProductionOrder { get; set; }

        //public virtual ProductionOrderProperty ProductionOrderProperties1 { get; set; }

        //public virtual ProductionOrderProperty ProductionOrderProperty1 { get; set; }
    }
}
