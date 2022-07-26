using Core.Application.DTOs.Account;
using Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Netbanking.Middleware
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor _httpContext;
        public ValidateSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool HasUser()
        {
            AuthenticationResponse user = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public bool IsAdmin()
        {
            AuthenticationResponse user = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            return user != null ? user.Roles.Any(rol => rol == "Admin") : false;
        }
    }
}
