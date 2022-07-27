using Core.Application.ViewModels.CreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Transation
{
    public class TransationsCardSaveViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; }
        public string NumberAccountToPay { get; set; }
        public List<CreditCardViewModel> CreditCards{get;set;}

    }
}
