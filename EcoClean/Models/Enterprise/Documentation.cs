using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Enterprise
{
    public class Documentation
    {
        public int DocumentationId { get; set; }
        public int EnterpriseId { get; set; }
        public Report[] Reports { get; set; }
    }
}
