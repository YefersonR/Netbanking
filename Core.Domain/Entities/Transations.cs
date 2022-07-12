using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Transations:AuditableBase
    {
        public int  Id { get; set; }
        public Guid AccountNumber { get; set; }
        public float Amount { get; set; }
        public Guid NumberAccountToPay{ get; set; }
        public SavingsAccount User { get; set; }
        public SavingsAccount AccountToPay{ get; set; }
    }
}
