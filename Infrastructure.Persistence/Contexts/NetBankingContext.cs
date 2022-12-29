using Core.Application.DTOs.Account;
using Core.Application.Helpers;
using Core.Domain.Commons;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class NetBankingContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse authentication;
        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<Transations> Transations{ get; set; }
        public DbSet<CreditCard> CreditCards{ get; set; }
        public DbSet<Loans> Loans{ get; set; }
        public DbSet<SavingsAccount> SavingsAccounts{ get; set; }

        public NetBankingContext(DbContextOptions<NetBankingContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            authentication = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach(var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = authentication.UserName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Updated = DateTime.Now;
                        entry.Entity.UpdatedBy = authentication.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

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
                .HasKey(loans => loans.Loan);
            modelBuilder.Entity<SavingsAccount>()
                .HasKey(savings => savings.AccountNumber);
            #endregion
            #region Relationship

            //modelBuilder.Entity<Beneficiary>()
            //    .HasOne<SavingsAccount>(transations => transations.BeneficiaryUser)
            //    .WithOne(beneficiary => beneficiary.Beneficiary)
            //    .HasForeignKey<SavingsAccount>(transations => transations.BeneficiaryID);

            //modelBuilder.Entity<Transations>()
            //    .HasOne<SavingsAccount>(account => account.SavingsAccount)
            //    .WithMany(transations => transations.Transations)
            //    .HasForeignKey(transations => transations.UserToPayAccount);

            //modelBuilder.Entity<Transations>()
            //    .HasOne<Loans>(account => account.Loans)
            //    .WithMany(transations => transations.Transations)
            //    .HasForeignKey(transations => transations.UserToPayAccount);

            //modelBuilder.Entity<Transations>()
            //    .HasOne<CreditCard>(account => account.CreditCard)
            //    .WithMany(transations => transations.Transations)
            //    .HasForeignKey(transations => transations.UserToPayAccount);
            #endregion

        }


    }
}
