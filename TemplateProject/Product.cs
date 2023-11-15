namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            MismatchInfo = new HashSet<MismatchInfo>();
            OrderInfo = new HashSet<OrderInfo>();
            RealizeOrderInfo = new HashSet<RealizeOrderInfo>();
        }

        public int ID { get; set; }

        public int TypeID { get; set; }

        public int UnitID { get; set; }

        public int VatID { get; set; }

        public double Wholesale { get; set; }

        public double Trading { get; set; }

        public int PackedTypeID { get; set; }

        [Required]
        public string Structure { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public double Cost { get; set; }

        public int UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MismatchInfo> MismatchInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

        public virtual PackedType PackedType { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        public virtual Vat Vat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealizeOrderInfo> RealizeOrderInfo { get; set; }
    }
}
