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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly NetBankingContext _dbContext;

        public UserRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
