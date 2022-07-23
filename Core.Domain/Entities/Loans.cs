using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Loans : AuditableBase
    {
        public int Id { get; set; }
        public Guid AccountUser { get; set; }
        public float Amount { get; set; }
        public string UserID { get; set; }
    }
}
