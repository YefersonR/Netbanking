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
        Task<List<CreditCardViewModel>> GetAllByUserID(string id);
        Task DeleteByStringID(string id);
        Task<string> getByIdString(string id);
        Task<double> TotalTarjeta();
        Task<double> DisponibleTarjeta();
        Task<double> TarjetaAlCorte();
        Task<List<CreditCardViewModel>> GetAllCreditCards();
    }
}
