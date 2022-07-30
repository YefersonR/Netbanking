using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Loans;
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
    public class LoansService : GenericService<LoansSaveViewModel, LoansViewModel, Loans>, ILoansService
    {
        private readonly ILoansRepository _loansRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly UserViewModel user;

        public LoansService(IAccountService accountService,ILoansRepository loansRepository, ISavingsAccountRepository savingsAccountRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(loansRepository, mapper)
        {
            _accountService = accountService;
            _loansRepository = loansRepository;
            _mapper = mapper;
            _savingsAccountRepository = savingsAccountRepository;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");


        }
        public override async Task<LoansSaveViewModel> Add(LoansSaveViewModel vm)
        {
            var Numberaccount = GenerateNumberAccount.GenerateAccount();
            var accountExist = await _loansRepository.GetById(Numberaccount);
            while (accountExist != null)
            {
                accountExist = await _loansRepository.GetById(Numberaccount);
                Numberaccount = GenerateNumberAccount.GenerateAccount();
            }
            var account = await _savingsAccountRepository.GetAllAsync();
            var useer = await _accountService.GetAccountByid(vm.UserID);
            var principalAccount =  account.FirstOrDefault(accountU=>accountU.AccountNumber == useer.SavingsAccount);
            principalAccount.Amount += vm.Debt;
            await _savingsAccountRepository.Update(principalAccount,useer.SavingsAccount);
            vm.Loan = Numberaccount;
            return await base.Add(vm);
        }
        public override async Task<List<LoansViewModel>> GetAllAsync()
        {
            var userAccounts = await base.GetAllAsync();
            return userAccounts.Where(account => account.UserID == user.Id).ToList();
        }
        public async Task<LoansSaveViewModel> GetById(string id)
        {
            var loan = await _loansRepository.GetById(id);
            LoansSaveViewModel loanVm = _mapper.Map<LoansSaveViewModel>(loan);
            return loanVm;
        }

        public async Task<List<LoansViewModel>> GetAllByUserID(string id)
        {
            var LoansList = await _loansRepository.GetAllAsync();
            return _mapper.Map<List<LoansViewModel>>(LoansList.Where(x => x.UserID == id).ToList());
        }

        public async Task DeleteByStringID(string id)
        {
            var data = await _loansRepository.GetById(id);
            await _loansRepository.DeleteAsync(data);
        }

    }
}
