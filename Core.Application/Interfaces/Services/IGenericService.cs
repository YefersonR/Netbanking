﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<List<ViewModel>> GetAllAsync();
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm, int ID);
        Task<SaveViewModel> GetById(int Id);
        Task Delete(int id);
    }
}
