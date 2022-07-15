using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class User : AuditableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string  Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public Guid SavingAccount { get; set; }
        public Guid CardCredit { get; set; }
        public SavingsAccount PrincipalSavingAccount { get; set;}
        public List<Beneficiary> Beneficiary { get; set; }
        public List<CreditCard> CreditCard{ get; set; }
        public List<Transations> Transations{ get; set; }
        public List<SavingsAccount> SavingsAccount { get; set; }
        public List<Loans> Loans { get; set; }

        

    }
}
