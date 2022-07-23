using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Netbanking.Controllers;

namespace WebApp.Netbanking.Middleware
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateSession _validateSession;

        public LoginAuthorize(ValidateSession validateSession)
        {
            _validateSession = validateSession;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_validateSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                context.Result = controller.RedirectToRoute("Index", "Home");
            }
            else
            {
                await next();
            }
        }
    }
}
