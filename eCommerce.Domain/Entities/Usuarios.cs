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
using eCommerce.Domain.Exceptions;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um usuário no sistema
    /// </summary>
    public class Usuarios : BaseEntity
    {

        #region Propriedades
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
            protected set => Email = ValueObjects.Email.Create(value);
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
        public string Usuarios_Sen
        {
            get => Senha?.Value;
            protected set => Senha = Senha.Create(value);
        }

        /// <summary>
        /// Permissão de visualização automática comercial
        /// </summary>
        public bool Usuarios_VisAutcom { get; protected set; }

        /// <summary>
        /// Permissão de visualização automática empresarial
        /// </summary>
        public bool Usuarios_VisAutemp { get; protected set; }

        // Value Objects internos
        private ValueObjects.Email Email { get; set; }
        private Cpf Cpf { get; set; }
        private Senha Senha { get; set; }
        #endregion

        #region Construtores
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
            Usuarios_Nom = nome;
            Usuarios_Ema = email; // Isso vai usar o setter que cria o Email Value Object
            Usuarios_Cpf = cpf;  // Isso vai usar o setter que cria o Cpf Value Object
            Usuarios_Sen = senha; // Isso vai usar o setter que cria o Senha Value Object
            IdBroker = idBroker;
            IdLoja = idLoja;
            IdVarejista = idVarejista;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

            Usuarios_Ati = true;
            Usuarios_DatCad = DateTime.UtcNow;
            Usuarios_EmpPad = 1;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        public ValidationResult Validar()
        {
            var validation = new Validations.Usuario(this);
            validation.Validate();
            return validation.GetValidationResult();
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
            Usuarios_Nom = nome;
            Usuarios_Ema = email; // Usa o setter que valida através do Value Object
            Usuarios_Cpf = cpf;  // Usa o setter que valida através do Value Object

            if (!string.IsNullOrEmpty(senha))
            {
                SenhaAnterior = Usuarios_Sen;
                Usuarios_Sen = senha;
            }

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

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
            usuario.AddDomainEvent(new Events.Usuario(
                usuario.Usuarios_Cod,
                usuario.Usuarios_Nom,
                usuario.Usuarios_Ema,
                createdBy));

            return usuario;
        }
        #endregion

        #region Navegação
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

        #endregion
    }
}
