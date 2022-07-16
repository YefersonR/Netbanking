using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Transation
{
    public class TransationsSaveViewModel
    {
        public int Id { get; set; }
        public Guid AccountNumber { get; set; }
        public float Amount { get; set; }
        public Guid NumberAccountToPay { get; set; }

    }
}
