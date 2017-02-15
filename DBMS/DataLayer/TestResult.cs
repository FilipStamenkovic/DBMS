namespace DBMS.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TestResult
    {
        public long Id { get; set; }

        public DateTime LastUpdateTs { get; set; }

        public DateTime TestTs { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductSerial { get; set; }

        [Required]
        [StringLength(255)]
        public string TestStatus { get; set; }

        [Required]
        [StringLength(255)]
        public string Class1 { get; set; }

        [Required]
        [StringLength(255)]
        public string Class2 { get; set; }

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

        public bool Valid { get; set; }
    }
}
