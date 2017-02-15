namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductProperties = new HashSet<ProductProperty>();
            Children = new HashSet<Product>();
        }

        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string SerialNumber { get; set; }

        public DateTime? EndProductionTime { get; set; }

        public DateTime? ScheduledStart { get; set; }

        public DateTime? ScheduledEnd { get; set; }

        public DateTime? StartProductionTime { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? ScheduledDeliveryDate { get; set; }

        public DateTime DownloadDate { get; set; }

        public int InitialQuantity { get; set; }

        public int ProductionStatus { get; set; }

        public long? ProductionOrderId { get; set; }

        public long ProductTypeId { get; set; }

        public long? ParentId { get; set; }

        public virtual ProductionOrder ProductionOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductProperty> ProductProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Children { get; set; }

        public virtual Product Parent { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
