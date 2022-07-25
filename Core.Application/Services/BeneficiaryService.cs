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
    public class BeneficiaryService : GenericService<BeneficiarySaveViewModel, BeneficiaryViewModel, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;

        public BeneficiaryService(ISavingsAccountRepository savingsAccountRepository,IBeneficiaryRepository beneficiaryRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _savingsAccountRepository = savingsAccountRepository;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override async Task<BeneficiarySaveViewModel> Add(BeneficiarySaveViewModel vm)
        {
            var accountExist = await _savingsAccountRepository.GetById(vm.AccountBeneficiary);
            if (accountExist != null)
            {
                vm.BeneficiaryUser = _mapper.Map<SavingsAccountViewModel>(accountExist); 
                vm.BeneficiaryID = accountExist.UserID;
                vm.UserID = user.Id; 
                return await base.Add(vm);
            }
            return null;
        }
        public override async Task<List<BeneficiaryViewModel>> GetAllAsync()
        {
            var userAccounts = await base.GetAllAsync();
            var userBeneficiary = await _savingsAccountRepository.GetAllAsync();
            return userAccounts.Where(account => account.UserID == user.Id).ToList();
        }
    }
}
