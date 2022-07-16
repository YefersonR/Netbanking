using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public Guid CardNumber { get; set; }
        public float Limit { get; set; }
        public float Debt { get; set; }
        public Guid AccountUser { get; set; }
        public UserViewModel User { get; set; }
    }
}
