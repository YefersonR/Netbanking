using Core.Application.ViewModels.Transation;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Interfaces.Services
{
    public interface ITransationService : IGenericService<TransationsSaveViewModel, TransationsViewModel, Transaction>
    {
    }
}
