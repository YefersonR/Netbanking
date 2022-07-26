using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Beneficiary : AuditableBase
    {
        public int Id { get; set; }
        public string UserID{get;set;}
        public string AccountBeneficiary { get; set; }
        public string BeneficiaryID { get; set; }
        public SavingsAccount BeneficiaryUser { get; set; }

    }
}
