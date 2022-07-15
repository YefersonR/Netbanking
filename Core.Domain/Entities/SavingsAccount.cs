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
        public Guid AccountNumber{ get; set; }
        public float Amount { get; set; }
        public User User { get; set; }
        public List<Transations> Transations{ get; set; }
        public List<Beneficiary> Beneficiaries{ get; set; }


    }
}
