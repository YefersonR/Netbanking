using Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService
    {
        private readonly IAccountService _accountService;
        
        public UserService(IAccountService accountService)
        {
            _accountService = accountService;
        }


    }
}
