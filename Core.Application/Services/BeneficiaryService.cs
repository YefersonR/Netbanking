using AutoMapper;
using Core.Application.DTOs.Account;
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
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse user;

        public BeneficiaryService(IAccountService accountService, ISavingsAccountRepository savingsAccountRepository,IBeneficiaryRepository beneficiaryRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _savingsAccountRepository = savingsAccountRepository;
            _httpContext = httpContext;
            _accountService = accountService;
            user = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");

        }
        public async Task<BeneficiarySaveViewModel> AddBeneficiary(BeneficiarySaveViewModel vm)
        {
            var accountExist = await _savingsAccountRepository.GetById(vm.AccountBeneficiary);            
            var users = _accountService.GetAllUser();
            if (accountExist == null)
            {
                vm.Error = "No existe esta cuenta";
                vm.HasError = true;
                return vm;

            }
            var useer = users.FirstOrDefault(us => us.Id == accountExist.UserID);
            if (useer == null)
            {
                vm.Error = "No existe un usuario con esta cuenta";
                vm.HasError = true;
                return vm;
            }
            vm.BeneficiaryID = useer.Id;
            vm.UserID = user.Id;
            Beneficiary beneficiary = _mapper.Map<Beneficiary>(vm);
            await _beneficiaryRepository.AddAsync(beneficiary);

            return vm;
        }
        public async Task DeleteBeneficiary(int beneficiaryID)
        {
            var beneficiaryVm = await _beneficiaryRepository.GetByIdAsync(beneficiaryID);
            if(beneficiaryVm is not null)
            {
                var beneficiary = _mapper.Map<Beneficiary>(beneficiaryVm);
                await _beneficiaryRepository.DeleteAsync(beneficiary);
            }

        }
        public async Task<List<UserBeneficiaryViewModel>> GetUserBeneficiary()
        {
            var beneficiaries = await _beneficiaryRepository.GetAllAsync();
            var users = _accountService.GetAllUser();
            var accounts = await _savingsAccountRepository.GetAllAsync();
            var myBenficiaries = beneficiaries.Where(beneficiary => beneficiary.UserID == user.Id).ToList();
            var userBeneficiearies = (from u in users
                                         join mb in myBenficiaries on
                                         u.Id equals mb.BeneficiaryID
                                         select u);
            var beneficiariesAccounts = (from a in accounts
                                         join mb in myBenficiaries on
                                         a.UserID equals mb.BeneficiaryID

                                         select a);
            var BeneficiariesWithAccount = (from ub in userBeneficiearies
                                            join ba in beneficiariesAccounts 
                                            on ub.Id equals ba.UserID
                                            join mb in myBenficiaries 
                                            on ub.Id equals mb.BeneficiaryID
                                            where mb.AccountBeneficiary == ba.AccountNumber
                                            select new UserBeneficiaryViewModel
                                            {
                                                ID = mb.Id,
                                                Name = ub.Name,
                                                LastName = ub.LastName,
                                                Email = ub.Email,
                                                SavingsAccount = ba.AccountNumber,
                                                Created = mb.Created
                                            }).ToList();

            return BeneficiariesWithAccount;
        }
        public async Task<UserConfirmationViewModel> GetUserByAccount(string confirmccount)
        {
            UserConfirmationViewModel userInfo = new();

            List<UserViewModel> users = _accountService.GetAllUser();
            List<SavingsAccount> accounts = await _savingsAccountRepository.GetAllAsync();
            var UserID = accounts.FirstOrDefault(account => account.AccountNumber == confirmccount).UserID ;
            var User = users.FirstOrDefault(user=>user.Id == UserID);
            userInfo.Name = User.Name;
            userInfo.LastName = User.LastName;
            userInfo.SavingsAccount = User.SavingsAccount;

            return userInfo;
        }
    }
}
