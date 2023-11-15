namespace TemplateProject.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RealizeOrder")]
    public partial class RealizeOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealizeOrder()
        {
            RealizeOrderInfo = new HashSet<RealizeOrderInfo>();
        }

        public int ID { get; set; }

        public int ContractorID { get; set; }

        public int EmployeeID { get; set; }

        public int TtnID { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ProductOrder ProductOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealizeOrderInfo> RealizeOrderInfo { get; set; }
    }
}
