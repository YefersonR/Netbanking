using Core.Application.ViewModels.CreditCard;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<CreditCardSaveViewModel, CreditCardViewModel, CreditCard>
    {
        Task<CreditCardSaveViewModel> GetById(string id);
    }
}
