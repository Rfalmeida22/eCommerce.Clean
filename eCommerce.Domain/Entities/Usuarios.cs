using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using eCommerce.Domain.Common;
using eCommerce.Domain.ValueObjects;
using eCommerce.Domain.Events;
using eCommerce.Domain.Validations;

namespace eCommerce.Domain.Entities.Usuarios
{
    /// <summary>
    /// Entidade que representa um usuário no sistema
    /// </summary>
    public class Usuarios : BaseEntity
    {
        private const int NOME_MAX_LENGTH = 100;
        private const int EMAIL_MAX_LENGTH = 200;
        private const int SENHA_MAX_LENGTH = 200;
        private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        /// <summary>
        /// Código identificador do usuário
        /// </summary>
        public int Usuarios_Cod { get; protected set; }

        /// <summary>
        /// Identificador do broker associado
        /// </summary>
        public int IdBroker { get; protected set; }

        /// <summary>
        /// Identificador da loja associada
        /// </summary>
        public int IdLoja { get; protected set; }

        /// <summary>
        /// Identificador do varejista associado
        /// </summary>
        public int IdVarejista { get; protected set; }

        /// <summary>
        /// Senha anterior do usuário
        /// </summary>
        public string SenhaAnterior { get; protected set; }

        /// <summary>
        /// Indica se o usuário está ativo
        /// </summary>
        public bool Usuarios_Ati { get; protected set; }

        /// <summary>
        /// Data de cadastro do usuário
        /// </summary>
        public DateTime Usuarios_DatCad { get; protected set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Usuarios_Ema 
        { 
            get => Email?.Address; 
            protected set => Email = Email.Create(value); 
        }

        /// <summary>
        /// CPF do usuário
        /// </summary>
        public string Usuarios_Cpf 
        { 
            get => Cpf?.Value; 
            protected set => Cpf = Cpf.Create(value); 
        }

        /// <summary>
        /// Empresa padrão do usuário
        /// </summary>
        public int Usuarios_EmpPad { get; protected set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Usuarios_Nom { get; protected set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Usuarios_Sen { get; protected set; }

        /// <summary>
        /// Permissão de visualização automática comercial
        /// </summary>
        public bool Usuarios_VisAutcom { get; protected set; }

        /// <summary>
        /// Permissão de visualização automática empresarial
        /// </summary>
        public bool Usuarios_VisAutemp { get; protected set; }

        // Value Objects internos
        private Email Email { get; set; }
        private Cpf Cpf { get; set; }

        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Usuarios() { }

        /// <summary>
        /// Cria uma nova instância de usuário
        /// </summary>
        public Usuarios(
            string nome,
            string email,
            string cpf,
            string senha,
            int idBroker = 0,
            int idLoja = 0,
            int idVarejista = 0)
        {
            ValidateNome(nome);
            ValidateSenha(senha);
            ValidateIds(idBroker, idLoja, idVarejista);

            // Usando os Value Objects internamente
            Email = Email.Create(email);
            Cpf = Cpf.Create(cpf);

            Usuarios_Nom = nome;
            Usuarios_Ema = email; // Isso vai usar o setter que cria o Email Value Object
            Usuarios_Sen = HashSenha(senha);
            IdBroker = idBroker;
            IdLoja = idLoja;
            IdVarejista = idVarejista;
            Usuarios_Ati = true;
            Usuarios_DatCad = DateTime.UtcNow;
            Usuarios_EmpPad = 1;
        }

        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        public ValidationResult Validar()
        {
            var validation = new UsuarioValidation(this);
            return validation.GetValidationResult();
        }

        private class UsuarioValidation : Validation
        {
            private readonly Usuarios _usuario;

            public UsuarioValidation(Usuarios usuario)
            {
                _usuario = usuario;

                ValidateRequired(_usuario.Usuarios_Nom, "Nome");
                ValidateMaxLength(_usuario.Usuarios_Nom, 100, "Nome");
                ValidateEmail(_usuario.Usuarios_Ema, "Email");
                ValidateRequired(_usuario.Usuarios_Sen, "Senha");
                ValidateMaxLength(_usuario.Usuarios_Sen, 200, "Senha");
                ValidateCPF(_usuario.Usuarios_Cpf, "CPF");
            }
        }

        private void ValidateNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome é obrigatório");

            if (nome.Length > NOME_MAX_LENGTH)
                throw new DomainException($"Nome deve ter no máximo {NOME_MAX_LENGTH} caracteres");
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email é obrigatório");

            if (email.Length > EMAIL_MAX_LENGTH)
                throw new DomainException($"Email deve ter no máximo {EMAIL_MAX_LENGTH} caracteres");

            if (!Regex.IsMatch(email, EMAIL_REGEX))
                throw new DomainException("Email inválido");
        }

        private void ValidateSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("Senha é obrigatória");

            if (senha.Length > SENHA_MAX_LENGTH)
                throw new DomainException($"Senha deve ter no máximo {SENHA_MAX_LENGTH} caracteres");
        }

        private void ValidateIds(int idBroker, int idLoja, int idVarejista)
        {
            if (idBroker < 0)
                throw new DomainException("IdBroker deve ser maior ou igual a zero");

            if (idLoja < 0)
                throw new DomainException("IdLoja deve ser maior ou igual a zero");

            if (idVarejista < 0)
                throw new DomainException("IdVarejista deve ser maior ou igual a zero");
        }

        /// <summary>
        /// Atualiza os dados do usuário
        /// </summary>
        public void AtualizarDados(
            string nome,
            string email,
            string cpf,
            string senha,
            string updatedBy)
        {
            ValidateNome(nome);
            
            if (!string.IsNullOrEmpty(senha))
            {
                ValidateSenha(senha);
                SenhaAnterior = Usuarios_Sen;
                Usuarios_Sen = HashSenha(senha);
            }

            Usuarios_Nom = nome;
            Usuarios_Ema = email; // Usa o setter que valida através do Value Object
            Usuarios_Cpf = cpf;   // Usa o setter que valida através do Value Object
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Ativa o usuário
        /// </summary>
        public void Ativar(string updatedBy)
        {
            Usuarios_Ati = true;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Desativa o usuário
        /// </summary>
        public void Desativar(string updatedBy)
        {
            Usuarios_Ati = false;
            SetUpdatedBy(updatedBy);

            // Adicionar evento
            AddDomainEvent(new UsuarioDesativado(
                Usuarios_Cod,
                Usuarios_Nom,
                updatedBy));
        }

        private string HashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Cria uma nova instância de usuário
        /// </summary>
        public static Usuarios Create(
            string nome,
            string email,
            string cpf,
            string senha,
            int idBroker,
            int idLoja,
            int idVarejista,
            string createdBy)
        {
            var usuario = new Usuarios(nome, email, cpf, senha, idBroker, idLoja, idVarejista);
            usuario.SetCreatedBy(createdBy);
            
            // Adicionar evento
            usuario.AddDomainEvent(new Usuario(
                usuario.Usuarios_Cod,
                usuario.Usuarios_Nom,
                usuario.Usuarios_Ema,
                createdBy));

            return usuario;
        }

        /// <summary>
        /// Navegação virtual para o Broker (Entity Framework)
        /// </summary>
        public virtual Broker Broker { get; protected set; }

        /// <summary>
        /// Navegação virtual para a Loja (Entity Framework)
        /// </summary>
        public virtual Lojas Loja { get; protected set; }

        /// <summary>
        /// Navegação virtual para o Varejista (Entity Framework)
        /// </summary>
        public virtual Varejista Varejista { get; protected set; }
    }
}
