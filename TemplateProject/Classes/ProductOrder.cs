namespace TemplateProject.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductOrder")]
    public partial class ProductOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOrder()
        {
            OrderInfo = new HashSet<OrderInfo>();
            RealizeOrder = new HashSet<RealizeOrder>();
        }

        public int ID { get; set; }

        public int ContractorID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealizeOrder> RealizeOrder { get; set; }
    }
}
