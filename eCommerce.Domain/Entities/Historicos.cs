using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class Historicos : BaseEntity
    {
        public int Historicos_Cod { get; protected set; }
        public string Historicos_Aca { get; protected set; }
        public DateTime Historicos_Dat { get; protected set; }
        public string Historicos_Det { get; protected set; }
        public string Historicos_Tab { get; protected set; }
        public int IdEmpresa { get; protected set; }
        public int Usuarios_Cod { get; protected set; }

        protected Historicos() { }

        public Historicos(
            string aca,
            string det,
            string tab,
            int idEmpresa,
            int usuariosCod)
        {
            ValidarDados(aca, det, tab, idEmpresa, usuariosCod);

            Historicos_Aca = aca;
            Historicos_Det = det;
            Historicos_Tab = tab;
            IdEmpresa = idEmpresa;
            Usuarios_Cod = usuariosCod;
            Historicos_Dat = DateTime.UtcNow;
        }

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoDados = ValidarDados(
                Historicos_Aca,
                Historicos_Det,
                Historicos_Tab,
                IdEmpresa,
                Usuarios_Cod);

            if (!resultadoDados.EhValido)
                erros.AddRange(resultadoDados.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string aca,
            string det,
            string tab,
            int idEmpresa,
            int usuariosCod)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(aca))
                erros.Add("Ação é obrigatória");

            if (!string.IsNullOrEmpty(aca) && aca.Length > 1)
                erros.Add("Ação deve ter exatamente 1 caractere");

            if (string.IsNullOrWhiteSpace(det))
                erros.Add("Descrição é obrigatória");

            if (!string.IsNullOrEmpty(tab) && tab.Length > 150)
                erros.Add("Tabela deve ter no máximo 150 caracteres");

            if (idEmpresa < 0)
                erros.Add("IdEmpresa deve ser maior ou igual a zero");

            if (usuariosCod < 0)
                erros.Add("Usuarios_Cod deve ser maior ou igual a zero");

            return new ValidationResult(erros.Count == 0, erros);
        }

        public void AtualizarDados(
            string aca,
            string det,
            string tab,
            int idEmpresa,
            int usuariosCod)
        {
            ValidarDados(aca, det, tab, idEmpresa, usuariosCod);

            Historicos_Aca = aca;
            Historicos_Det = det;
            Historicos_Tab = tab;
            IdEmpresa = idEmpresa;
            Usuarios_Cod = usuariosCod;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
