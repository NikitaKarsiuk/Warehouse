using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.HelpClasses
{
    class OrderInfoData
    {
        public int ID { get; set; }

        public Product Product { get; set; }

        public string UnitName { get; set; }
        public string PackedName { get; set; }
        public string TypeName { get; set; }

        public double OrderCount { get; set; }

        public double Sum { get; set; }
    }
}
