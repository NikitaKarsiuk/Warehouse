namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductType")]
    public partial class ProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int Square200 { get; set; }

        public int Square400 { get; set; }

        public int Square650 { get; set; }

        public int Square800 { get; set; }

        public int Square1000 { get; set; }

        public int Square2500 { get; set; }

        public int Square4000 { get; set; }

        public int Square6000 { get; set; }

        public int Square8000 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
