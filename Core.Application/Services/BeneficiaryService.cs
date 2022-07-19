using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Domain.Entities;
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
        private readonly IMapper _mapper;
        
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
        }
    }
}
