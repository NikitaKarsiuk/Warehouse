namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trailer")]
    public partial class Trailer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        public int ContractorID { get; set; }

        public int UserID { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
