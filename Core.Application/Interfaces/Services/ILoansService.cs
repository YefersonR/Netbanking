using Core.Application.ViewModels.Loans;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface ILoansService : IGenericService<LoansSaveViewModel, LoansViewModel, Loans>
    {
        Task<List<LoansViewModel>> GetAllByUserID(string id);
        Task DeleteByStringID(string id);
        Task<double> TotalDeudas();

    }
}
