using Core.Application.DTOs.Account;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using WebApp.Netbanking.Middleware;

namespace WebApp.Netbanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            AuthenticationResponse user = await _userService.Login(loginViewModel);
            if(user != null && user.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user",user);
                if (user.Roles.Any(rol => rol == "Admin"))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                    return RedirectToRoute(new { controller = "Client", action = "Index" });


            }
            else
            {
                loginViewModel.HasError = user.HasError;
                loginViewModel.Error = user.Error;
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
            }
            return View(loginViewModel);
        }
        public async Task<IActionResult> LogOut()
        {
            await _userService.SingOut();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller="User",action="Index"});
        }
        
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmail(userId,token);
            return View("ConfirmEmail", response);
        }
        
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }
        
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse forgotResponse = await _userService.ForgotPassword(forgotPassword,origin);
            if (forgotResponse.HasError)
            {
                forgotPassword.HasError = forgotResponse.HasError;
                forgotPassword.Error = forgotResponse.Error;

                return View(forgotPassword);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token});
        }
        
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }
            ResetPasswordResponse resetResponse = await _userService.ResetPassword(resetPassword);
            if (resetResponse.HasError)
            {
                resetPassword.HasError = resetResponse.HasError;
                resetPassword.Error = resetResponse.Error;

                return View(resetPassword);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

    }

}