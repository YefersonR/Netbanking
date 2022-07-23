
using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.SavingsAccount;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class SavingAccountService : GenericService<SavingsAccountSaveViewModel, SavingsAccountViewModel, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;
        
        public SavingAccountService(ISavingsAccountRepository savingsAccountRepository, IMapper mapper) : base(savingsAccountRepository, mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
        }

        public async Task<List<SavingsAccountViewModel>> GetAllByUserID(string ID)
        {
            var SavingList = await _savingsAccountRepository.GetAllAsync();
            return _mapper.Map<List<SavingsAccountViewModel>>(SavingList.Where(x => x.UserID == ID).ToList());
        }

    }
}
