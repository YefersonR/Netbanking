using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class CreditCard : AuditableBase
    {
        public Guid CardNumber { get; set; }
        public float Limit { get; set; }
        public float Debt { get; set;}
        public Guid AccountUser { get; set; }
        public User User { get; set; }
    }
}
