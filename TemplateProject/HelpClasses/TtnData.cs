using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.HelpClasses
{
    class TtnData
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public List<Contractor> Contractor { get; set; }

        public List<Employee> Employee { get; set; }
        
        public List<OrderInfo> OrderInfo { get; set; }
    }
}
