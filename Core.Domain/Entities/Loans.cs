using Core.Domain.Commons;
using System.Collections.Generic;


namespace Core.Domain.Entities
{
    public class Loans : AuditableBase
    {
        public string Loan { get; set; }
        public double Debt { get; set; }
        public string UserID { get; set; }
        public double Limit { get; set; }
        public List<Transations> Transations { get; set; } = new();


    }
}
