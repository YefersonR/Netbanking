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
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
    {
        private readonly NetBankingContext _dbContext;

        public CreditCardRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreditCard> GetById(string Id)
        {
            return await _dbContext.Set<CreditCard>().FindAsync(Id);
        }
        public async Task Pay(string Id)
        {
            CreditCard entry = await _dbContext.Set<CreditCard>().FindAsync(Id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
