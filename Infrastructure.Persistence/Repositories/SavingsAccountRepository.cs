using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SavingsAccountRepository : GenericRepository<SavingsAccount>, ISavingsAccountRepository
    {
        private readonly NetBankingContext _dbContext;

        public SavingsAccountRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SavingsAccount> GetById(string Id)
        {
            return await _dbContext.Set<SavingsAccount>().FindAsync(Id);

        }
        public async Task Pay(string Id)
        {
            SavingsAccount entry = await _dbContext.Set<SavingsAccount>().FindAsync(Id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entry);
            await _dbContext.SaveChangesAsync();
        }

    }
}
