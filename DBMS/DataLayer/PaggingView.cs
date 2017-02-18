namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PaggingView
    {
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

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime LastUpdateTs { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime TestTs { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string ProductSerial { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string TestStatus { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string Class1 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(255)]
        public string Class2 { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal TestTemperature { get; set; }

        public DateTime? DCTs { get; set; }

        public decimal? DCParam1 { get; set; }

        public decimal? DCParam2 { get; set; }

        public decimal? DCParam3 { get; set; }

        public decimal? DCParam4 { get; set; }

        public decimal? DCParam5 { get; set; }

        public decimal? DCParam6 { get; set; }

        [StringLength(255)]
        public string DCParam7 { get; set; }

        [StringLength(255)]
        public string DCParam8 { get; set; }

        public float? DCAlpha { get; set; }

        [StringLength(255)]
        public string DCStatus { get; set; }

        public DateTime? ACTs { get; set; }

        public decimal? ACParam1 { get; set; }

        public decimal? ACParam2 { get; set; }

        public decimal? ACParam3 { get; set; }

        [StringLength(255)]
        public string ACParam4 { get; set; }

        [StringLength(255)]
        public string ACParam5 { get; set; }

        [StringLength(255)]
        public string ACStatus { get; set; }

        public DateTime? RestTs { get; set; }

        public decimal? RestParam1 { get; set; }

        public decimal? RestParam2 { get; set; }

        public decimal? RestParam3 { get; set; }

        public decimal? RestParam4 { get; set; }

        public decimal? RestParam5 { get; set; }

        [StringLength(255)]
        public string RestParam6 { get; set; }

        [StringLength(255)]
        public string RestStatus { get; set; }

        public DateTime? ChargeTs { get; set; }

        [StringLength(255)]
        public string ChargeParam1 { get; set; }

        public decimal? ChargeParam2 { get; set; }

        [StringLength(255)]
        public string ChargeParam3 { get; set; }

        [StringLength(255)]
        public string ChargeStatus { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool Valid { get; set; }
    }
}
