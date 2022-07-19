using Core.Application.DTOs.Account;
using Core.Application.DTOs.Email;
using Core.Application.Enums;
using Infrastructure.Identity.Models;
using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailService _emailService;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.UserName}";
                
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,false,lockoutOnFailure:false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";

                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.UserName}";

                return response;
            }
            response.Id = user.Id;
            response.Email= user.Email;
            response.UserName= user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false); 
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }
        public async Task<string> ConfirmAccount(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            return $"An error occurred while confirming {user.Email}.";
        }
        public async Task<RegisterResponse> RegisterClients(RegisterRequest request,string origin)
        {
            RegisterResponse response = new();

            response.HasError = false;
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"userName {request.UserName} is already taken";
                return response;
            }
            var userWithSameEmail= await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.UserName} is already register";
                return response;
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName =request.LastName,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to register the user";
                return response;
            }
            await _userManager.AddToRoleAsync(user,Roles.Client.ToString());
            var verificationUri = await SendVerificacionEmailUrl(user,origin);
            await _emailService.SendEmail(new EmailRequest()
            {
                To = user.Email,
                Body = $"Confirm your account this url {verificationUri}",
                Subject="Confirm Registration"
            }); 

            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new();

            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}"; 

                return response;
            }
            var verificationUri = await SendForgotPasswordUrl(user, origin);
            await _emailService.SendEmail(new EmailRequest()
            {
                To = user.Email,
                Body = $"Reset your account visiting this url {verificationUri}",
                Subject = "Reset Password"
            });

            return response;
        }
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token,request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resetting password ";
                return response;
            }

            return response;
        }


        public async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/{route}"));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }

        public async Task<string> SendVerificacionEmailUrl(ApplicationUser user,string origin)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/{route}"));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(),"userId",user.Id);
            verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }

    }
}
