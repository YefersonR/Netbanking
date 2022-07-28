using Core.Application.ViewModels.SavingsAccount;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface ISavingsAccountService : IGenericService <SavingsAccountSaveViewModel, SavingsAccountViewModel, SavingsAccount>
    {
        Task<List<SavingsAccountViewModel>> GetAllByUserID(string id);
        Task UpdateC(SavingsAccountSaveViewModel model, string id);

    }
}
