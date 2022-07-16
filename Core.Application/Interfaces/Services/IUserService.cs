using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserSaveViewModel, UserViewModel, User>
    {
    }
}
