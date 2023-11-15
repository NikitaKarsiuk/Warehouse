namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RealizeOrder")]
    public partial class RealizeOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealizeOrder()
        {
            RealizeOrderInfo = new HashSet<RealizeOrderInfo>();
        }

        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserID { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealizeOrderInfo> RealizeOrderInfo { get; set; }
    }
}
