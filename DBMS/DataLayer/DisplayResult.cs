namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DisplayResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(255)]
        public string Operation { get; set; }

        [StringLength(3000)]
        public string Batch { get; set; }

        [StringLength(3000)]
        public string BatchType { get; set; }

        public string BatchSegment { get; set; }

        public string BatchLot { get; set; }

        [StringLength(3000)]
        public string PowderCharge { get; set; }

        [StringLength(255)]
        public string TestPlan { get; set; }

        public int? TestPlanRevision { get; set; }

        [StringLength(255)]
        public string Material { get; set; }

        [StringLength(255)]
        public string MaterialDescription { get; set; }

        [StringLength(255)]
        public string VaristorType { get; set; }

        [StringLength(255)]
        public string VarDiameter { get; set; }

        [StringLength(255)]
        public string VarHeight { get; set; }

        public long TestResultId { get; set; }

        public virtual TestResult TestResult { get; set; }
    }
}
