using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Debe ingresar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar la clave")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool HasError { get; set; } = false;
        public string Error { get; set; }
    }
}
