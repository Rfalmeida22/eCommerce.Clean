using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma loja no sistema
    /// </summary>
    public class Lojas : BaseEntity
    {
        private const int CNPJ_MAX_LENGTH = 50;
        private const int CODIGO_MAX_LENGTH = 50;
        private const int NOME_MAX_LENGTH = 100;
        private const int ENDERECO_MAX_LENGTH = 255;
        private const string NOME_REQUIRED_MESSAGE = "Nome da loja é obrigatório";

        /// <summary>
        /// Identificador único da loja
        /// </summary>
        public int IdLoja { get; protected set; }

        /// <summary>
        /// CNPJ da loja
        /// </summary>
        public string CdCnpj 
        { 
            get => Cnpj?.Value; 
            protected set => Cnpj = Cnpj.Create(value); 
        }

        // Value Object interno
        private Cnpj Cnpj { get; set; }

        /// <summary>
        /// Código da loja
        /// </summary>
        public string CdLoja { get; protected set; }

        /// <summary>
        /// Identificador do lojista
        /// </summary>
        public int IdLojista { get; protected set; }

        /// <summary>
        /// Identificador do varejista
        /// </summary>
        public int IdVarejista { get; protected set; }

        /// <summary>
        /// Nome da loja
        /// </summary>
        public string NmLoja { get; protected set; }

        /// <summary>
        /// Endereço da loja
        /// </summary>
        public string TxEndereco { get; protected set; }

        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Lojas() { }

        /// <summary>
        /// Cria uma nova instância de Loja
        /// </summary>
        public Lojas(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            ValidarDados(cdLoja, idLojista, idVarejista, nmLoja, txEndereco);
            
            // Usando Value Object
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object

            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;
        }

        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var erros = new List<string>();

            try
            {
                // Revalidar usando Value Object
                if (!string.IsNullOrEmpty(CdCnpj))
                    Cnpj = Cnpj.Create(CdCnpj);

                ValidarDados(CdLoja, IdLojista, IdVarejista, NmLoja, TxEndereco);
            }
            catch (DomainException ex)
            {
                erros.Add(ex.Message);
            }

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            var erros = new List<string>();

            ValidateCodigoLoja(cdLoja, erros);
            ValidateIds(idLojista, idVarejista, erros);
            ValidateNome(nmLoja, erros);
            ValidateEndereco(txEndereco, erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private void ValidateCodigoLoja(string codigo, List<string> errors)
        {
            if (!string.IsNullOrEmpty(codigo) && codigo.Length > CODIGO_MAX_LENGTH)
                errors.Add($"Código da loja deve ter no máximo {CODIGO_MAX_LENGTH} caracteres");
        }

        private void ValidateIds(int idLojista, int idVarejista, List<string> errors)
        {
            if (idLojista < 0)
                errors.Add("IdLojista deve ser maior ou igual a zero");

            if (idVarejista < 0)
                errors.Add("IdVarejista deve ser maior ou igual a zero");
        }

        private void ValidateNome(string nome, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(nome))
                errors.Add(NOME_REQUIRED_MESSAGE);

            if (!string.IsNullOrEmpty(nome) && nome.Length > NOME_MAX_LENGTH)
                errors.Add($"Nome da loja deve ter no máximo {NOME_MAX_LENGTH} caracteres");
        }

        private void ValidateEndereco(string endereco, List<string> errors)
        {
            if (!string.IsNullOrEmpty(endereco) && endereco.Length > ENDERECO_MAX_LENGTH)
                errors.Add($"Endereço deve ter no máximo {ENDERECO_MAX_LENGTH} caracteres");
        }

        /// <summary>
        /// Atualiza os dados da loja
        /// </summary>
        public void AtualizarDados(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco,
            string updatedBy)
        {
            ValidarDados(cdLoja, idLojista, idVarejista, nmLoja, txEndereco);

            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object
            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância de Loja
        /// </summary>
        public static Lojas Create(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco,
            string createdBy)
        {
            var loja = new Lojas(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco);
            loja.SetCreatedBy(createdBy);

            // Adicionar evento
            loja.AddDomainEvent(new Loja(
                loja.IdLoja,
                loja.NmLoja,
                loja.CdCnpj,
                loja.IdVarejista,
                createdBy));

            return loja;
        }

        /// <summary>
        /// Navegação virtual para o Lojista (Entity Framework)
        /// </summary>
        public virtual Lojista Lojista { get; protected set; }

        /// <summary>
        /// Navegação virtual para o Varejista (Entity Framework)
        /// </summary>
        public virtual Varejista Varejista { get; protected set; }
    }
}
