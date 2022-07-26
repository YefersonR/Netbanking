using AutoMapper;
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
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        
        public LoansService(ILoansRepository loansRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(loansRepository, mapper)
        {
            _loansRepository = loansRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");

        }
        public override async Task<LoansSaveViewModel> Add(LoansSaveViewModel vm)
        {
            var Numberaccount = GenerateNumberAccount.GenerateAccount();
            var accountExist = await _loansRepository.GetById(Numberaccount);
            while (accountExist == null)
            {
                accountExist = await _loansRepository.GetById(Numberaccount);
                Numberaccount = GenerateNumberAccount.GenerateAccount();
            }
            vm.Loan = Numberaccount;
            vm.UserID = user.Id;
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

        public async Task<List<LoansViewModel>> GetAllByUserID(string ID)
        {
            var LoansList = await _loansRepository.GetAllAsync();
            return _mapper.Map<List<LoansViewModel>>(LoansList.Where(x => x.UserID == ID).ToList());
        }
        
    }
}
