using Core.Application.DTOs.Account;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IUserService 
    {
        Task<AuthenticationResponse> Login(LoginViewModel loginViewModel);
        Task<RegisterResponse> Register(UserSaveViewModel userSaveViewModel, string origin);
        Task<string> ConfirmEmail(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel, string origin);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        Task SingOut();

    }
}
