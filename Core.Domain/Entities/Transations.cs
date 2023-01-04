using Core.Domain.Commons;


namespace Core.Domain.Entities
{
    public class Transations : AuditableBase
    {
        public int  Id { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public string UserToPayAccount { get; set; }
        public string CreditCard { get; set; }
        public string SavingsAccount { get; set; }
        public string Loans { get; set; }

    }
}
