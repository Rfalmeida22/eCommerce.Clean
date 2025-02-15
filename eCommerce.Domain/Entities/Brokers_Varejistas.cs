using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class Brokers_Varejistas : BaseEntity
    {
        public int IdSequencial { get; protected set; }
        public int IdBroker { get; protected set; }
        public int IdVarejista { get; protected set; }

        protected Brokers_Varejistas() { }

        public Brokers_Varejistas(int idBroker, int idVarejista)
        {
            ValidarRelacionamentos(idBroker, idVarejista);
            IdBroker = idBroker;
            IdVarejista = idVarejista;
        }

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoRelacionamentos = ValidarRelacionamentos(IdBroker, IdVarejista);
            if (!resultadoRelacionamentos.EhValido)
                erros.AddRange(resultadoRelacionamentos.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarRelacionamentos(int idBroker, int idVarejista)
        {
            var erros = new List<string>();

            if (idBroker <= 0)
                erros.Add("IdBroker deve ser maior que zero");

            if (idVarejista <= 0)
                erros.Add("IdVarejista deve ser maior que zero");

            return new ValidationResult(erros.Count == 0, erros);
        }
    }
}
