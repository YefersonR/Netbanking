using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.CreditCard;
using Core.Domain.Entities;
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
        
        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }

        public async Task<List<CreditCardViewModel>> GetAllByUserID(string ID)
        {
            var CreditCardsList = await _creditCardRepository.GetAllAsync();
            return _mapper.Map<List<CreditCardViewModel>>(CreditCardsList.Where(x => x.UserID == ID).ToList());
        }
    }
}
