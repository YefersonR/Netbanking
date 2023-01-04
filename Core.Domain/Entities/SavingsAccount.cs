using Core.Domain.Commons;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class SavingsAccount:AuditableBase
    {
        public string AccountNumber{ get; set; }
        public double Amount { get; set; }
        public string UserID { get; set; }
    }
}
