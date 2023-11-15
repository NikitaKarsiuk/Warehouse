namespace TemplateProject.Classes
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderInfo")]
    public partial class OrderInfo
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int OrderCount { get; set; }

        public virtual ProductOrder ProductOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
