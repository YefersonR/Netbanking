using Core.Application.ViewModels.Transation;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Interfaces.Services
{
    public interface ITransationService : IGenericService<TransationsSaveViewModel, TransationsViewModel, Transaction>
    {
        Task<TransationsSaveViewModel> PayToAccount(TransationsSaveViewModel vm);
        Task<TransationsSaveViewModel> PayToCard(TransationsSaveViewModel vm);
        Task<TransationsSaveViewModel> RetireToCard(TransationsSaveViewModel vm);
        Task<TransationsSaveViewModel> PayLoans(TransationsSaveViewModel vm);
        Task<TransationsSaveViewModel> RetireLoans(TransationsSaveViewModel vm);
        Task<List<TransationsViewModel>> GetAll();
        Task<TransationsSaveViewModel> GetById(string id);

    }
}
