using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.CreditCard;
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
    public class CreditCardService : GenericService<CreditCardSaveViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper, IHttpContextAccessor httpContext) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override async Task<CreditCardSaveViewModel> Add(CreditCardSaveViewModel vm)
        {
            var Numberaccount = GenerateNumberAccount.GenerateAccount();
            var accountExist = await _creditCardRepository.GetById(Numberaccount);
            while (accountExist != null)
            {
                accountExist = await _creditCardRepository.GetById(Numberaccount);
                Numberaccount = GenerateNumberAccount.GenerateAccount();
            }
            vm.CardNumber = Numberaccount;
            return await base.Add(vm);
        }
        public override async Task<List<CreditCardViewModel>> GetAllAsync()
        {
            var userAccounts = await base.GetAllAsync();
            return userAccounts.Where(account => account.UserID == user.Id).ToList();
        }
        public async Task<CreditCardSaveViewModel> GetById(string id)
        {
            var Card = await _creditCardRepository.GetById(id);
            CreditCardSaveViewModel CardVm = _mapper.Map<CreditCardSaveViewModel>(Card);
            return CardVm;
        }
        public async Task<List<CreditCardViewModel>> GetAllByUserID(string ID)
        {
            var CreditCardsList = await _creditCardRepository.GetAllAsync();
            return _mapper.Map<List<CreditCardViewModel>>(CreditCardsList.Where(x => x.UserID == ID).ToList());
        }

        public async Task DeleteByStringID(string id)
        {
            var data = await _creditCardRepository.GetById(id);
            await _creditCardRepository.DeleteAsync(data);
        }
        public async Task<string> getByIdString(string id)
        {
            var data = await _creditCardRepository.GetById(id);
            return data.CardNumber;
        }

    }
}
