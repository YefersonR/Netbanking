﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public string SavingsAccount { get; set; }

    }
}
