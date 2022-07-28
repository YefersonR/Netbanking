
using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAccountService _accountService;
        private readonly UserViewModel user;


        public SavingAccountService(IAccountService accountService, ISavingsAccountRepository savingsAccountRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(savingsAccountRepository, mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _accountService = accountService;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");

        }
        public override async Task<SavingsAccountSaveViewModel> Add(SavingsAccountSaveViewModel vm)
        {
            var Numberaccount = GenerateNumberAccount.GenerateAccount();
            var accountExist = await _savingsAccountRepository.GetById(Numberaccount);
            while(accountExist != null)
            {
                Numberaccount = GenerateNumberAccount.GenerateAccount();
                accountExist = await _savingsAccountRepository.GetById(Numberaccount);
            }
            vm.AccountNumber = Numberaccount;
            var useraccount =   await base.Add(vm);
            
            
            
            vm.UserID = user.Id;

            return null;
        }
        public override async Task<List<SavingsAccountViewModel>> GetAllAsync()
        {
            var userAccounts =  await base.GetAllAsync();
            return userAccounts.Where(account=>account.UserID == user.Id).ToList();
        }
        public async Task<SavingsAccountViewModel> GetById(string id)
        {
            var account = await _savingsAccountRepository.GetById(id);
            SavingsAccountViewModel accountVm = _mapper.Map<SavingsAccountViewModel>(account);
            return accountVm;
        }

    }
}
