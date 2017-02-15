namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaterialItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaterialItem()
        {
            ConfigurationVariants = new HashSet<ConfigurationVariant>();
            MaterialItemProperties = new HashSet<MaterialItemProperty>();
            ProductionOrders = new HashSet<ProductionOrder>();
        }

        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(255)]
        public string ItemDescription { get; set; }

        public long? ParentId { get; set; }

        public long? ProductTypeId { get; set; }

        public int Valid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfigurationVariant> ConfigurationVariants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialItemProperty> MaterialItemProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionOrder> ProductionOrders { get; set; }
    }
}
