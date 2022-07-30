using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Services
{
    public class TransationsService : GenericService<TransationsSaveViewModel, TransationsViewModel, Transations>, ITransationService
    {
        private readonly ITransationsRepository _transationsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ILoansRepository _loansRepository;


        public TransationsService(ILoansRepository loansRepository, ICreditCardRepository creditCardRepository, ISavingsAccountRepository savingsAccountRepository, ITransationsRepository transationsRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(transationsRepository, mapper)
        {
            _transationsRepository = transationsRepository;
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _creditCardRepository = creditCardRepository;
            _loansRepository = loansRepository;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<TransationsSaveViewModel> PayToAccount(TransationsSaveViewModel vm)
        {
            var account = await _savingsAccountRepository.GetById(vm.AccountNumber);
            var accountToPay = await _savingsAccountRepository.GetById(vm.UserToPayAccount);
            if (account.Amount >= vm.Amount)
            {
                    account.Amount -= vm.Amount;
                await _savingsAccountRepository.Pay(account.AccountNumber);

                accountToPay.Amount += vm.Amount;
                await _savingsAccountRepository.Pay(accountToPay.AccountNumber);
            }
            return await base.Add(vm);
        }
        public async Task<TransationsSaveViewModel> PayToCard(TransationsSaveViewModel vm)
        {
            var account = await _savingsAccountRepository.GetById(vm.AccountNumber);
            var card = await _creditCardRepository.GetById(vm.CardNumber);
            if(account.Amount >= vm.Amount )
            {
                account.Amount -= vm.Amount;
                await _savingsAccountRepository.Pay(account.AccountNumber);

                if(vm.Amount - card.Debt <= 0)
                {
                    card.Debt -= vm.Amount;
                }
                else
                {
                    account.Amount += vm.Amount - card.Debt;
                    card.Debt -=  card.Debt;

                }
                Transations aTransation = _mapper.Map<Transations>(vm);
                account.Transations.Add(aTransation);
                card.Transations.Add(aTransation);
                await _creditCardRepository.Pay(card.CardNumber);
            }
            
            return await base.Add(vm);
        }
        public async Task<TransationsSaveViewModel> RetireToCard(TransationsSaveViewModel vm)
        {

            var card = await _creditCardRepository.GetById(vm.CardNumber);
            var account = await _savingsAccountRepository.GetById(vm.AccountNumber);
            if (card.Limit >= card.Debt)
            {
                card.Debt += vm.Amount  + (100 * 0.0625);
                await _creditCardRepository.Pay(card.CardNumber);

                account.Amount += vm.Amount;
                await _savingsAccountRepository.Pay(account.AccountNumber);
              
            }

            return await base.Add(vm);
        }
        public async Task<TransationsSaveViewModel> PayLoans(TransationsSaveViewModel vm)
        {
            var account = await _savingsAccountRepository.GetById(vm.AccountNumber);
            var loans = await _loansRepository.GetById(vm.UserToPayAccount);
            if (account.Amount >= vm.Amount)
            {
                account.Amount -= vm.Amount;
                await _savingsAccountRepository.Pay(account.AccountNumber);

                if (loans.Debt >= 0)
                {
                    loans.Debt -= vm.Amount;
                }
                else
                {
                    account.Amount += vm.Amount - loans.Debt;
                    loans.Debt -= loans.Debt;

                }
                await _loansRepository.Pay(loans.Loan);
                Transations aTransation = _mapper.Map<Transations>(vm);
                account.Transations.Add(aTransation);
                loans.Transations.Add(aTransation);
            }

            return await base.Add(vm);
        }
        public async Task<TransationsSaveViewModel> RetireLoans(TransationsSaveViewModel vm)
        {

            var loans = await _loansRepository.GetById(vm.UserToPayAccount);
            var account = await _savingsAccountRepository.GetById(vm.AccountNumber);
            if (loans.Limit >= loans.Debt)
            {
                loans.Debt += vm.Amount;
                await _loansRepository.Pay(loans.Loan);

                account.Amount += vm.Amount;
                await _savingsAccountRepository.Pay(account.AccountNumber);
            }

            return await base.Add(vm);
        }
        public async Task<List<TransationsViewModel>> GetAll()
        {
            var userTransations = await _transationsRepository.GetAllAsync();
            var userAccount = await _savingsAccountRepository.GetAllAsync();
         
            var result = (from t in userTransations
                          join a in userAccount
                          on t.AccountNumber equals a.AccountNumber
                          where (a.UserID == user.Id)
                          select t).ToList();
            
            List<TransationsViewModel> viewModels = _mapper.Map<List<TransationsViewModel>>(result);
            return viewModels;
        }
        public async Task<TransationsSaveViewModel> GetById(string id)
        {
            var transation = await _savingsAccountRepository.GetById(id);
            TransationsSaveViewModel transationVm = _mapper.Map<TransationsSaveViewModel>(transation);
            return transationVm;
        }

    }
}
