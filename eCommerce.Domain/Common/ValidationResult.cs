using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Common
{
    public class ValidationResult
    {
        public bool EhValido { get; }
        public List<string> Erros { get; }

        public ValidationResult(bool ehValido, List<string> erros)
        {
            EhValido = ehValido;
            Erros = erros ?? new List<string>();
        }
    }
}
