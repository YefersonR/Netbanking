using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class SavingsAccount:AuditableBase
    {
        public string AccountNumber{ get; set; }
        public double Amount { get; set; }
        public string UserID { get; set; }
        public List<Transations> Transations { get; set; }
    }
}
