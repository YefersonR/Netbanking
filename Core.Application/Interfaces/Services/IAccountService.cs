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
    public interface IAccountService 
    {
        Task<AuthenticationResponse> Authentication(AuthenticationRequest request);
        Task<string> ConfirmAccount(string userId, string token);
        Task<RegisterResponse> RegisterClients(RegisterRequest request, string origin);
        Task<RegisterResponse> UpdateClient(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterAdmin(RegisterRequest request, string origin);
        Task<RegisterResponse> UpdateAdmin(RegisterRequest request, string origin);
        List<UserViewModel> GetAllUser();
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        Task SignOut();
        Task<List<UserGetAllViewModel>> GetAllVMUser();
    }
}
