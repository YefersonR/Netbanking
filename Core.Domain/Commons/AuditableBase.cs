using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Commons
{
    public class AuditableBase
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
    }
}
