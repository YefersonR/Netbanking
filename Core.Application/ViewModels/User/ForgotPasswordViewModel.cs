using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Debe ingresar el correo del usuario")]
        public string Email { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }

    }
}
