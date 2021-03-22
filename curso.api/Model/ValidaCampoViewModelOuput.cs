using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Model
{
    public class ValidaCampoViewModelOuput
    {
        public IEnumerable<string> Erros { get; private set; }
        public ValidaCampoViewModelOuput(IEnumerable<string> erros)
        {
            this.Erros = erros;
        }
    }
}
