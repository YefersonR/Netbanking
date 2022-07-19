using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Transation;
using Core.Domain.Entities;
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
        
        public TransationsService(ITransationsRepository transationsRepository, IMapper mapper) : base(transationsRepository, mapper)
        {
            _transationsRepository = transationsRepository;
            _mapper = mapper;
        }
    }
}
