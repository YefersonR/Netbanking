using Core.Domain.Commons;
using System.Collections.Generic;

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
