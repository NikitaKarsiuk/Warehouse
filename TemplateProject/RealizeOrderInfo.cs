namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RealizeOrderInfo")]
    public partial class RealizeOrderInfo
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public double OrderCount { get; set; }

        public virtual Product Product { get; set; }

        public virtual RealizeOrder RealizeOrder { get; set; }
    }
}
