using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class NetBankingContext : DbContext
    {
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transations> Transations{ get; set; }
        public DbSet<CreditCard> CreditCards{ get; set; }
        public DbSet<Loans> Loans{ get; set; }
        public DbSet<SavingsAccount> SavingsAccounts{ get; set; }

        public NetBankingContext(DbContextOptions<NetBankingContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<CreditCard>()
                .HasKey(card=>card.CardNumber);
            modelBuilder.Entity<Beneficiary>()
                .HasKey(beneficiary=> beneficiary.Id);
            modelBuilder.Entity<Transations>()
                .HasKey(transations => transations.Id);
            modelBuilder.Entity<Loans>()
                .HasKey(loans => loans.Id);
            modelBuilder.Entity<SavingsAccount>()
                .HasKey(savings => savings.AccountNumber);
            #endregion
            #region Relationship

            modelBuilder.Entity<Beneficiary>()
                .HasOne<SavingsAccount>(transations => transations.BeneficiaryUser)
                .WithMany(user => user.Beneficiaries)
                .HasForeignKey(transations => transations.AccountBeneficiary);
            
            modelBuilder.Entity<SavingsAccount>()
                .HasMany<Transations>(user => user.Transations)
                .WithOne(transations => transations.User)
                .HasForeignKey(benediciary => benediciary.AccountNumber);

            
            #endregion

        }


    }
}
