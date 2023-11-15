namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Register")]
    public partial class Register
    {
        public int ID { get; set; }

        public int TtnID { get; set; }

        public int UserID { get; set; }

        public virtual TTN TTN { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
