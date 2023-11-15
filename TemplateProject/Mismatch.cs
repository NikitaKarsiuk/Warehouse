namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mismatch")]
    public partial class Mismatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mismatch()
        {
            MismatchInfo = new HashSet<MismatchInfo>();
        }

        public int ID { get; set; }

        public int TtnID { get; set; }

        public DateTime Date { get; set; }

        public int UserID { get; set; }

        public virtual TTN TTN { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MismatchInfo> MismatchInfo { get; set; }
    }
}
