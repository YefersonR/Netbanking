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
    public class LoansRepository : GenericRepository<Loans>, ILoansRepository
    {
        private readonly NetBankingContext _dbContext;

        public LoansRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Loans> GetById(string Id)
        {
            return await _dbContext.Set<Loans>().FindAsync(Id);
        }
        public async Task Pay(string Id,Loans loan)
        {
            if (loan.Debt == 0)
            {
                _dbContext.Set<Loans>().Remove(loan);
            }
            else
            {
                Loans entry = await _dbContext.Set<Loans>().FindAsync(Id);
                _dbContext.Entry(entry).CurrentValues.SetValues(loan);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
