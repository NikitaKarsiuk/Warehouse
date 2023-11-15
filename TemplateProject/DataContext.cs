namespace TemplateProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Contractor> Contractor { get; set; }
        public virtual DbSet<ContractorType> ContractorType { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Mismatch> Mismatch { get; set; }
        public virtual DbSet<MismatchInfo> MismatchInfo { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<PackedType> PackedType { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<RealizeOrder> RealizeOrder { get; set; }
        public virtual DbSet<RealizeOrderInfo> RealizeOrderInfo { get; set; }
        public virtual DbSet<Register> Register { get; set; }
        public virtual DbSet<ShopType> ShopType { get; set; }
        public virtual DbSet<Trailer> Trailer { get; set; }
        public virtual DbSet<TTN> TTN { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<Vat> Vat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.BankDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.UNP)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.OKPO)
                .IsUnicode(false);

            modelBuilder.Entity<Contractor>()
                .Property(e => e.ContactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ContractorType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ProductOrder)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ContractorEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ProductOrder1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PackedType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Structure)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductOrder>()
                .HasMany(e => e.OrderInfo)
                .WithRequired(e => e.ProductOrder)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<ProductOrder>()
                .HasMany(e => e.TTN)
                .WithRequired(e => e.ProductOrder)
                .HasForeignKey(e => e.OrderID);

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

            modelBuilder.Entity<ShopType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ShopType>()
                .HasMany(e => e.Contractor)
                .WithOptional(e => e.ShopType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Trailer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Trailer>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Address)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Car)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Contractor)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Mismatch)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.ProductOrder)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.RealizeOrder)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Register)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Trailer)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.TTN)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Vat)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
