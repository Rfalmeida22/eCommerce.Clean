using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using eCommerce.Domain.Common;

namespace eCommerce.Domain.Entities.Usuarios
{
    public class Usuarios : BaseEntity
    {
        public int Usuarios_Cod { get; protected set; }
        public int IdBroker { get; protected set; }
        public int IdLoja { get; protected set; }
        public int IdVarejista { get; protected set; }
        public string SenhaAnterior { get; protected set; }
        public bool Usuarios_Ati { get; protected set; }
        public DateTime Usuarios_DatCad { get; protected set; }
        public string Usuarios_Ema { get; protected set; }
        public int Usuarios_EmpPad { get; protected set; }
        public string Usuarios_Nom { get; protected set; }
        public string Usuarios_Sen { get; protected set; }
        public bool Usuarios_VisAutcom { get; protected set; }
        public bool Usuarios_VisAutemp { get; protected set; }

        protected Usuarios() { }

        public Usuarios(
            string nome,
            string email,
            string senha,
            int idBroker = 0,
            int idLoja = 0,
            int idVarejista = 0)
        {
            ValidarDados(nome, email, senha, idBroker, idLoja, idVarejista);

            Usuarios_Nom = nome;
            Usuarios_Ema = email;
            Usuarios_Sen = senha;
            IdBroker = idBroker;
            IdLoja = idLoja;
            IdVarejista = idVarejista;
            Usuarios_Ati = true;
            Usuarios_DatCad = DateTime.UtcNow;
            Usuarios_EmpPad = 1;
        }

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoDados = ValidarDados(
                Usuarios_Nom,
                Usuarios_Ema,
                Usuarios_Sen,
                IdBroker,
                IdLoja,
                IdVarejista);

            if (!resultadoDados.EhValido)
                erros.AddRange(resultadoDados.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string nome,
            string email,
            string senha,
            int idBroker,
            int idLoja,
            int idVarejista)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("Nome é obrigatório");

            if (nome?.Length > 100)
                erros.Add("Nome deve ter no máximo 100 caracteres");

            if (string.IsNullOrWhiteSpace(email))
                erros.Add("Email é obrigatório");

            if (!string.IsNullOrEmpty(email) && email.Length > 200)
                erros.Add("Email deve ter no máximo 200 caracteres");

            if (string.IsNullOrWhiteSpace(senha))
                erros.Add("Senha é obrigatória");

            if (!string.IsNullOrEmpty(senha) && senha.Length > 200)
                erros.Add("Senha deve ter no máximo 200 caracteres");

            if (idBroker < 0)
                erros.Add("IdBroker deve ser maior ou igual a zero");

            if (idLoja < 0)
                erros.Add("IdLoja deve ser maior ou igual a zero");

            if (idVarejista < 0)
                erros.Add("IdVarejista deve ser maior ou igual a zero");

            return new ValidationResult(erros.Count == 0, erros);
        }

        public void AtualizarDados(
            string nome,
            string email,
            string senha)
        {
            ValidarDados(nome, email, senha, IdBroker, IdLoja, IdVarejista);

            Usuarios_Nom = nome;
            Usuarios_Ema = email;
            Usuarios_Sen = senha;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
