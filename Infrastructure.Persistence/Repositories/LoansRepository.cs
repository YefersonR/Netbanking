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
    }
}
