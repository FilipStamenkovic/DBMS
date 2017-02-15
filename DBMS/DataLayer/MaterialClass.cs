namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaterialClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaterialClass()
        {
            MaterialItemProperties = new HashSet<MaterialItemProperty>();
        }

        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string DataType { get; set; }

        [StringLength(255)]
        public string DefaultValue { get; set; }

        public int? PropagationType { get; set; }

        public long? PrototypedFrom { get; set; }

        public long? InheritedFrom { get; set; }

        public bool? IsValid { get; set; }

        public bool? IsRequired { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialItemProperty> MaterialItemProperties { get; set; }
    }
}
