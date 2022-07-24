using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Loans;
using Core.Domain.Entities;
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
        
        public LoansService(ILoansRepository loansRepository, IMapper mapper) : base(loansRepository, mapper)
        {
            _loansRepository = loansRepository;
            _mapper = mapper;
        }

        public async Task<List<LoansViewModel>> GetAllByUserID(string ID)
        {
            var LoansList = await _loansRepository.GetAllAsync();
            return _mapper.Map<List<LoansViewModel>>(LoansList.Where(x => x.UserID == ID).ToList());
        }
        
    }
}
