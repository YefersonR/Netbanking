﻿using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TransationsRepository : GenericRepository<Transations>, ITransationsRepository
    {
        private readonly NetBankingContext _dbContext;

        public TransationsRepository(NetBankingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<List<Transations>> GetAllAsync()
        {
            return await _dbContext.Set<Transations>()
                                    .OrderByDescending(transation => transation.Created)
                                    .ToListAsync();
        }
    }
}
