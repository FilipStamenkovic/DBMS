namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductionOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionOrder()
        {
            ProductionOrderProperties = new HashSet<ProductionOrderProperty>();
            Children = new HashSet<ProductionOrder>();
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [StringLength(255)]
        public string ExternalId { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public int Priority { get; set; }

        public DateTime? StartProductionTime { get; set; }

        public DateTime? EndProductionTime { get; set; }

        public int? Status { get; set; }

        public DateTime DownloadDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? ScheduledStart { get; set; }

        public DateTime? ScheduledEnd { get; set; }

        [StringLength(255)]
        public string LineItem { get; set; }

        public long? MaterialItemId { get; set; }

        public int BatchSize { get; set; }

        public long? ParentId { get; set; }

        public long ProductTypeId { get; set; }

        public virtual MaterialItem MaterialItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionOrderProperty> ProductionOrderProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionOrder> Children { get; set; }

        public virtual ProductionOrder Parent { get; set; }

        public virtual ProductType ProductType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
