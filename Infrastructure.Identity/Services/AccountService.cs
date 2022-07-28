using Core.Application.DTOs.Account;
using Core.Application.DTOs.Email;
using Core.Application.Enums;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Core.Application.ViewModels.User;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using Core.Application.DTOs.Email;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Helpers;
using Infrastructure.Identity.Context;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISavingsAccountService _savingAccount;
        private readonly IdentityContext _identityContext;
        private readonly IEmailService _emailService;
        public AccountService(IEmailService emailService,IdentityContext identityContext, ISavingsAccountService savingAccount,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _savingAccount = savingAccount;
            _identityContext = identityContext;
            _emailService = emailService;
        }
        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
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
                response.Error = $"Email {request.Email} is already register";
                return response;
            }
            var cuenta = GenerateNumberAccount.GenerateAccount();
            var account = new SavingsAccountSaveViewModel()
            {
                AccountNumber = cuenta
            };
            account.Amount = float.Parse(request.SavingsAccount);
            await _savingAccount.Add(account);

            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                LastName =request.LastName,
                UserName = request.UserName,
                Identification = request.Identification,
                SavingAccount = account.AccountNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            var created= GetAllUser();
            var createdaccount = created.FirstOrDefault(user => user.SavingsAccount == account.AccountNumber);

            account.UserID = createdaccount.Id;
            await _savingAccount.UpdateC(account,account.AccountNumber);

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
                To=user.Email,
                Body =$"Please confirm your account vissitinf this Url {verificationUri}",
                Subject = "Confirm registration"
            });
            return response;
        }
        public async Task<RegisterResponse> UpdateClient(RegisterRequest request,string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userToUpdate = await _userManager.FindByNameAsync(request.UserName);
            var nuevoMonto = request.SavingsAccount;
            var user = userToUpdate;

            user.Email = request.Email;
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Identification = request.Identification;
            user.SavingAccount = userToUpdate.SavingAccount;
            //Agregar lo de editar monto
            SavingsAccountSaveViewModel savingVm = new();
            savingVm.UserID = user.Id;
            savingVm.AccountNumber = user.SavingAccount;
            savingVm.Amount = float.Parse(nuevoMonto);
            await _savingAccount.UpdateC(savingVm, user.SavingAccount);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to update the user";
                return response;
            }
            return response;
        }

        public async Task<RegisterResponse> RegisterAdmin(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();

            response.HasError = false;
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"userName {request.UserName} is already taken";
                return response;
            }
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already register";
                return response;
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                Identification = request.Identification
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to register the user";
                return response;
            }
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            var verificationUri = await SendVerificacionEmailUrl(user, origin);
            await _emailService.SendEmail(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please confirm your account vissitinf this Url {verificationUri}",
                Subject = "Confirm registration"
            });
            return response;
        }
        public async Task<RegisterResponse> UpdateAdmin(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userToUpdate = await _userManager.FindByNameAsync(request.UserName);

            var user = userToUpdate;

            user.Email = request.Email;
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Identification = request.Identification;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to update the user";
                return response;
            }
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
        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            return $"An error occurred while confirming {user.Email}.";
        }
        private async Task<string> SendVerificacionEmailUrl(ApplicationUser user, string origin)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/",route));
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(),"userId",user.Id);
            verificationUrl += QueryHelpers.AddQueryString(verificationUrl,"token",code);

            return verificationUrl;
        }
    private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/{route}"));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }


        public async Task SignOut()
       {
            await _signInManager.SignOutAsync();
       }
        public List<UserViewModel> GetAllUser()
        {
            var users = _userManager.Users.ToList();
            List<UserViewModel> usersList = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                Identification = user.Identification,
                SavingsAccount = user.SavingAccount
                
            }).ToList();

            return usersList;
        }

        public async Task<List<UserGetAllViewModel>> GetAllVMUser()
        {
            var users = _userManager.Users.ToList();
            var all = users.Select(x => new UserGetAllViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                IsAdmin = false
            }).ToList();

            var adminID = GetAdminUsers().Result;
            foreach (var item in all)
            {
                var user = await _userManager.FindByIdAsync(item.Id);
                item.IsActive = user.EmailConfirmed;
                if (adminID.Contains(item.Id))
                {
                    item.IsAdmin = true;
                }
                try
                {
                    var i = _savingAccount.GetAllByUserID(item.Id).Result.ToList();
                    foreach (var data in i)
                    {
                        if (data.AccountNumber == user.SavingAccount)
                        {
                            item.Monto = data.Amount;
                        }
                    }
                }
                catch (Exception e) { }
            }

            return all;
        }
        public async Task ChangeUserState(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.EmailConfirmed = user.EmailConfirmed == false ? true : false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<string>> GetAdminUsers()
        {
            var roleList = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            return roleList.Select(x => x.Id).ToList();
        }

        public async Task<string> GetSavingByID(string id)
        {
            var savigs = await _userManager.FindByIdAsync(id);
            return savigs.SavingAccount;
        }

        public async Task<UserSaveViewModel> GetAccountByid(string ID)
        {
            var data = await _userManager.FindByIdAsync(ID);
            return new UserSaveViewModel
            {
                Name = data.Name,
                LastName = data.LastName,
                UserName = data.UserName,
                Identification = data.Identification,
                Email = data.Email,
                Id = data.Id,
            };
        }

    }
}
