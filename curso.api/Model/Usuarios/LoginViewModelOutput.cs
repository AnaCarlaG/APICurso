using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Model
{
    public class LoginViewModelOutput
    {
        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
