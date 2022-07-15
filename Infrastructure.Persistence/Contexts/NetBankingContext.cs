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
        public DbSet<User> Users{ get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transations> Transations{ get; set; }
        public DbSet<CreditCard> CreditCards{ get; set; }
        public DbSet<Loans> Loans{ get; set; }
        public DbSet<SavingsAccount> SavingsAccounts{ get; set; }

        public NetBankingContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<User>()
                .HasKey(user=>user.CardCredit);
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

            modelBuilder.Entity<User>()
                .HasMany<CreditCard>(user => user.CreditCard)
                .WithOne(card => card.User)
                .HasForeignKey(card=>card.AccountUser)
                .OnDelete(DeleteBehavior.Cascade);
                
            //modelBuilder.Entity<User>()
            //    .HasMany<Beneficiary>(user => user.Beneficiary)
            //    .WithOne(benediciary => benediciary.User)
            //    .HasForeignKey(benediciary => benediciary.AccountUser)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Beneficiary>()
                .HasOne<SavingsAccount>(transations => transations.BeneficiaryUser)
                .WithMany(user => user.Beneficiaries)
                .HasForeignKey(transations => transations.AccountBeneficiary);

            modelBuilder.Entity<User>()
                .HasMany<Loans>(user => user.Loans)
                .WithOne(loans => loans.User)
                .HasForeignKey(loans => loans.AccountUser)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany<SavingsAccount>(user => user.SavingsAccount)
                .WithOne(savings => savings.User)
                .HasForeignKey(savings => savings.AccountNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SavingsAccount>()
                .HasMany<Transations>(user => user.Transations)
                .WithOne(transations => transations.User)
                .HasForeignKey(benediciary => benediciary.AccountNumber);

            
            #endregion

        }


    }
}
