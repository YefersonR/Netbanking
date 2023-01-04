using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar el apellido del usuario")]
        [DataType(DataType.Text)] 
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar la cedula del usuario")]
        [DataType(DataType.Text)] 
        public string Identification { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar el correo del usuario")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar la contraseñas del usuario")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben ser iguales")]
        [Required(ErrorMessage = "Debe ingresar una contraseña para el usuario")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;}
        public string SavingsAccount { get; set; }
        public bool isActive { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }

    }
}
