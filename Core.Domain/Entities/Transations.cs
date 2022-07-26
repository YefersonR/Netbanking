using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Transations : AuditableBase
    {
        public int  Id { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public string UserToPayAccount { get; set; }
        public CreditCard CreditCard { get; set; }
        public SavingsAccount SavingsAccount { get; set; }
        public Loans Loans { get; set; }

    }
}
