using Core.Domain.Commons;

namespace Core.Domain.Entities
{
    public class Beneficiary : AuditableBase
    {
        public int Id { get; set; }
        public string UserID{get;set;}
        public string BeneficiaryID { get; set; }
        public string AccountBeneficiary { get; set; }
    }
}
