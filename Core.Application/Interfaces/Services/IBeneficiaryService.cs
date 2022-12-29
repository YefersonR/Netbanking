using Core.Application.ViewModels.Beneficiary;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService : IGenericService<BeneficiarySaveViewModel, BeneficiaryViewModel, Beneficiary>
    {
        Task<List<UserBeneficiaryViewModel>> GetUserBeneficiary();
        Task<BeneficiarySaveViewModel> AddBeneficiary(BeneficiarySaveViewModel vm);
        Task DeleteBeneficiary(int beneficiaryID);
    }
}
