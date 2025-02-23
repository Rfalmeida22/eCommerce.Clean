using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa os detalhes de importação do varejo
    /// </summary>
    public class LogImportacaoVarejoDetalhe : BaseEntity
    {
        private const int BROKER_MAX_LENGTH = 255;
        private const int CARTAO_MAX_LENGTH = 255;
        private const int CPF_MAX_LENGTH = 11;
        private const int LOJA_MAX_LENGTH = 255;
        private const int OBSERVACAO_MAX_LENGTH = 1255;
        private const int STATUS_MAX_LENGTH = 100;
        private const int VAREJISTA_MAX_LENGTH = 255;
        private const int VENDEDOR_MAX_LENGTH = 255;

        /// <summary>
        /// Identificador único do detalhe da importação
        /// </summary>
        public int IdDetalhe { get; protected set; }

        /// <summary>
        /// Nome do Broker
        /// </summary>
        public string Broker { get; protected set; }

        /// <summary>
        /// Código do cartão
        /// </summary>
        public string CdCartao { get; protected set; }

        /// <summary>
        /// CPF do comprador
        /// </summary>
        public string CpfComprador 
        { 
            get => Cpf?.Value; 
            protected set => Cpf = Cpf.Create(value); 
        }

        // Value Object interno
        private Cpf Cpf { get; set; }

        /// <summary>
        /// Data de cancelamento, se houver
        /// </summary>
        public DateTime? DataCancelamento { get; protected set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; protected set; }

        /// <summary>
        /// Data de validade
        /// </summary>
        public DateTime DataValidade { get; protected set; }

        /// <summary>
        /// Data da venda
        /// </summary>
        public DateTime DataVenda { get; protected set; }

        /// <summary>
        /// ID do log principal
        /// </summary>
        public int IdLog { get; protected set; }

        /// <summary>
        /// Nome da loja
        /// </summary>
        public string Loja { get; protected set; }

        /// <summary>
        /// Observações adicionais
        /// </summary>
        public string Observacao { get; protected set; }

        /// <summary>
        /// Status do registro
        /// </summary>
        public string Status { get; protected set; }

        /// <summary>
        /// Nome do varejista
        /// </summary>
        public string Varejista { get; protected set; }

        /// <summary>
        /// Nome do vendedor
        /// </summary>
        public string Vendedor { get; protected set; }

        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected LogImportacaoVarejoDetalhe() { }

        /// <summary>
        /// Cria uma nova instância de LogImportacaoVarejoDetalhe
        /// </summary>
        public LogImportacaoVarejoDetalhe(
            string broker,
            string cdCartao,
            string cpfComprador,
            DateTime? dataCancelamento,
            DateTime dataCriacao,
            DateTime dataValidade,
            DateTime dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor)
        {
            ValidarDados(broker, cdCartao, cpfComprador, dataCancelamento, dataCriacao,
                dataValidade, dataVenda, idLog, loja, observacao, status, varejista, vendedor);

            Broker = broker;
            CdCartao = cdCartao;
            CpfComprador = cpfComprador;
            DataCancelamento = dataCancelamento;
            DataCriacao = dataCriacao;
            DataValidade = dataValidade;
            DataVenda = dataVenda;
            IdLog = idLog;
            Loja = loja;
            Observacao = observacao;
            Status = status;
            Varejista = varejista;
            Vendedor = vendedor;
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
                ValidateStringLength(Broker, "Broker", BROKER_MAX_LENGTH, erros);
                ValidateStringLength(CdCartao, "CdCartao", CARTAO_MAX_LENGTH, erros);
                ValidateCpf(CpfComprador, erros);
                ValidateLogId(IdLog, erros);
                ValidateStringLength(Loja, "Loja", LOJA_MAX_LENGTH, erros);
                ValidateStringLength(Observacao, "Observacao", OBSERVACAO_MAX_LENGTH, erros);
                ValidateStringLength(Status, "Status", STATUS_MAX_LENGTH, erros);
                ValidateStringLength(Varejista, "Varejista", VAREJISTA_MAX_LENGTH, erros);
                ValidateStringLength(Vendedor, "Vendedor", VENDEDOR_MAX_LENGTH, erros);
                ValidateDates(DataCriacao, DataValidade, DataVenda, DataCancelamento, erros);

                // Revalidar usando Value Object
                if (!string.IsNullOrEmpty(CpfComprador))
                    Cpf = Cpf.Create(CpfComprador);

                return new ValidationResult(erros.Count == 0, erros);
            }
            catch (DomainException ex)
            {
                erros.Add(ex.Message);
                return new ValidationResult(false, erros);
            }
        }

        private void ValidateLogId(int logId, List<string> errors)
        {
            if (logId < 0)
                errors.Add("IdLog deve ser maior ou igual a zero");
        }

        private void ValidateStringLength(string value, string fieldName, int maxLength, List<string> errors)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                errors.Add($"{fieldName} deve ter no máximo {maxLength} caracteres");
        }

        private void ValidateCpf(string cpf, List<string> errors)
        {
            if (!string.IsNullOrEmpty(cpf))
            {
                if (cpf.Length > CPF_MAX_LENGTH)
                    errors.Add($"CPF deve ter no máximo {CPF_MAX_LENGTH} caracteres");

                if (!IsCpfValid(cpf))
                    errors.Add("CPF inválido");
            }
        }

        private bool IsCpfValid(string cpf)
        {
            // Implementar validação específica de CPF
            return true; // Temporário
        }

        private void ValidateDates(DateTime dataCriacao, DateTime dataValidade, DateTime dataVenda, 
            DateTime? dataCancelamento, List<string> errors)
        {
            if (dataValidade < dataCriacao)
                errors.Add("Data de validade não pode ser menor que a data de criação");

            if (dataVenda > DateTime.UtcNow)
                errors.Add("Data de venda não pode ser futura");

            if (dataCancelamento.HasValue && dataCancelamento.Value < dataVenda)
                errors.Add("Data de cancelamento não pode ser menor que a data de venda");
        }

        /// <summary>
        /// Atualiza os dados do registro
        /// </summary>
        public void AtualizarDados(
            string broker,
            string cdCartao,
            string cpfComprador,
            DateTime? dataCancelamento,
            DateTime dataCriacao,
            DateTime dataValidade,
            DateTime dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor,
            string updatedBy)
        {
            ValidarDados(broker, cdCartao, cpfComprador, dataCancelamento, dataCriacao,
                dataValidade, dataVenda, idLog, loja, observacao, status, varejista, vendedor);

            Broker = broker;
            CdCartao = cdCartao;
            CpfComprador = cpfComprador;
            DataCancelamento = dataCancelamento;
            DataCriacao = dataCriacao;
            DataValidade = dataValidade;
            DataVenda = dataVenda;
            IdLog = idLog;
            Loja = loja;
            Observacao = observacao;
            Status = status;
            Varejista = varejista;
            Vendedor = vendedor;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância de LogImportacaoVarejoDetalhe
        /// </summary>
        public static LogImportacaoVarejoDetalhe Create(
            string broker,
            string cdCartao,
            string cpfComprador,
            DateTime? dataCancelamento,
            DateTime dataCriacao,
            DateTime dataValidade,
            DateTime dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor,
            string createdBy)
        {
            var log = new LogImportacaoVarejoDetalhe(broker, cdCartao, cpfComprador, dataCancelamento,
                dataCriacao, dataValidade, dataVenda, idLog, loja, observacao, status, varejista, vendedor);
            log.SetCreatedBy(createdBy);
            return log;
        }
    }
}
