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
        public string Loan { get; set; }
        public double Debt { get; set; }
        public string UserID { get; set; }
        public double Limit { get; set; }
        public List<Transations> Transations { get; set; }


    }
}
