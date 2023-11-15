namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOrder")]
    public partial class ProductOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOrder()
        {
            OrderInfo = new HashSet<OrderInfo>();
            TTN = new HashSet<TTN>();
        }

        public int ID { get; set; }

        public int ContractorID { get; set; }

        public int EmployeeID { get; set; }

        public int ContractorEmployeeID { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserID { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TTN> TTN { get; set; }
    }
}
