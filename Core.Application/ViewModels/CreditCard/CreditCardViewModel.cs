using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public string CardNumber { get; set; }
        public double Limit { get; set; }
        public double Debt { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }
        public List<TransationsViewModel> Transations { get; set; }
    }
}
