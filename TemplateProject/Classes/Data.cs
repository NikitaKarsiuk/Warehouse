namespace TemplateProject.Classes
{
    using System.Data.Entity;

    public partial class Data : DbContext
    {
        public Data()
            : base("name=SharagaContext")
        {
        }

        public virtual DbSet<Contractor> Contractor { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<RealizeOrder> RealizeOrder { get; set; }
        public virtual DbSet<RealizeOrderInfo> RealizeOrderInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.ContactInfo)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.BankDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.UTN)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.OKPO)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductOrder>()
                .HasMany(e => e.OrderInfo)
                .WithRequired(e => e.ProductOrder)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<ProductOrder>()
                .HasMany(e => e.RealizeOrder)
                .WithRequired(e => e.ProductOrder)
                .HasForeignKey(e => e.TtnID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.ProductType)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<RealizeOrder>()
                .HasMany(e => e.RealizeOrderInfo)
                .WithRequired(e => e.RealizeOrder)
                .HasForeignKey(e => e.OrderID);
        }
    }
}
