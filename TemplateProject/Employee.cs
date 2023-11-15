namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            ProductOrder = new HashSet<ProductOrder>();
            ProductOrder1 = new HashSet<ProductOrder>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FIO { get; set; }

        public int PositionID { get; set; }

        public int ContractorID { get; set; }

        public int UserID { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual Position Position { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrder> ProductOrder1 { get; set; }
    }
}
