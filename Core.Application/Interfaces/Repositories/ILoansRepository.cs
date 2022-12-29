using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface ILoansRepository : IGenericRepository<Loans>
    {
        Task<Loans> GetById(string Id);
        Task Pay(string Id,Loans loan);
    }
}
