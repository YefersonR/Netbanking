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
        public string CardNumber { get; set; }
        public double Limit { get; set; }
        public double Debt { get; set;}
        public string UserID { get; set; }
        public List<Transations> Transations { get; set; } = new();

    }
}
