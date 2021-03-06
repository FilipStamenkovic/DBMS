namespace DBMS.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel(string connectionName)
            : base(connectionName)
        {
        }

        public virtual DbSet<ConfigurationVariant> ConfigurationVariants { get; set; }
        public virtual DbSet<DisplayResult> DisplayResults { get; set; }
        public virtual DbSet<MaterialClass> MaterialClasses { get; set; }
        public virtual DbSet<MaterialItemProperty> MaterialItemProperties { get; set; }
        public virtual DbSet<MaterialItem> MaterialItems { get; set; }
        public virtual DbSet<ProductionOrderProperty> ProductionOrderProperties { get; set; }
        public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<PaggingView> PaggingView { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialClass>()
                .HasMany(e => e.MaterialItemProperties)
                .WithRequired(e => e.MaterialClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialItem>()
                .HasMany(e => e.MaterialItemProperties)
                .WithRequired(e => e.MaterialItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestResult>()
                .HasOptional(e => e.DisplayResult)
                .WithRequired(e => e.TestResult);

            modelBuilder.Entity<ProductionOrder>()
                .Property(e => e.Quantity)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ProductionOrder>()
                .HasMany(e => e.ProductionOrderProperties)
                .WithRequired(e => e.ProductionOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductionOrder>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductProperties)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ProductionOrders)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.TestTemperature)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam4)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam5)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.DCParam6)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.ACParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.ACParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.ACParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.RestParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.RestParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.RestParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.RestParam4)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.RestParam5)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TestResult>()
                .Property(e => e.ChargeParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.TestTemperature)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam4)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam5)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.DCParam6)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.ACParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.ACParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.ACParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.RestParam1)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.RestParam2)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.RestParam3)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.RestParam4)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.RestParam5)
                .HasPrecision(19, 5);

            modelBuilder.Entity<PaggingView>()
                .Property(e => e.ChargeParam2)
                .HasPrecision(19, 5);
        }
    }
}
