namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MismatchInfo")]
    public partial class MismatchInfo
    {
        public int ID { get; set; }

        public int MismatchID { get; set; }

        public int ProductID { get; set; }

        public double OrderCount { get; set; }

        public virtual Mismatch Mismatch { get; set; }

        public virtual Product Product { get; set; }
    }
}
