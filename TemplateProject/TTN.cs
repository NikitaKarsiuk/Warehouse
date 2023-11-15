namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TTN")]
    public partial class TTN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTN()
        {
            Mismatch = new HashSet<Mismatch>();
            Register = new HashSet<Register>();
        }

        public int ID { get; set; }

        public int OrderID { get; set; }

        public int CarID { get; set; }

        public int? TrailerID { get; set; }

        public int EmployeeID { get; set; }

        public int UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mismatch> Mismatch { get; set; }

        public virtual ProductOrder ProductOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Register> Register { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
