using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class LogImportacaoVarejoDetalhe : BaseEntity
    {
        public int IdDetalhe { get; protected set; }
        public string Broker { get; protected set; }
        public string CdCartao { get; protected set; }
        public string CpfComprador { get; protected set; }
        public string DataCancelamento { get; protected set; }
        public string DataCriacao { get; protected set; }
        public string DataValidade { get; protected set; }
        public string DataVenda { get; protected set; }
        public int IdLog { get; protected set; }
        public string Loja { get; protected set; }
        public string Observacao { get; protected set; }
        public string Status { get; protected set; }
        public string Varejista { get; protected set; }
        public string Vendedor { get; protected set; }

        protected LogImportacaoVarejoDetalhe() { }

        public LogImportacaoVarejoDetalhe(
            string broker,
            string cdCartao,
            string cpfComprador,
            string dataCancelamento,
            string dataCriacao,
            string dataValidade,
            string dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor)
        {
            ValidarDados(
                broker,
                cdCartao,
                cpfComprador,
                dataCancelamento,
                dataCriacao,
                dataValidade,
                dataVenda,
                idLog,
                loja,
                observacao,
                status,
                varejista,
                vendedor);

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

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoDados = ValidarDados(
                Broker,
                CdCartao,
                CpfComprador,
                DataCancelamento,
                DataCriacao,
                DataValidade,
                DataVenda,
                IdLog,
                Loja,
                Observacao,
                Status,
                Varejista,
                Vendedor);

            if (!resultadoDados.EhValido)
                erros.AddRange(resultadoDados.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string broker,
            string cdCartao,
            string cpfComprador,
            string dataCancelamento,
            string dataCriacao,
            string dataValidade,
            string dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor)
        {
            var erros = new List<string>();

            if (!string.IsNullOrEmpty(broker) && broker.Length > 255)
                erros.Add("Broker deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(cdCartao) && cdCartao.Length > 255)
                erros.Add("CdCartao deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(cpfComprador) && cpfComprador.Length > 255)
                erros.Add("CpfComprador deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(dataCancelamento) && dataCancelamento.Length > 255)
                erros.Add("DataCancelamento deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(dataCriacao) && dataCriacao.Length > 255)
                erros.Add("DataCriacao deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(dataValidade) && dataValidade.Length > 255)
                erros.Add("DataValidade deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(dataVenda) && dataVenda.Length > 255)
                erros.Add("DataVenda deve ter no máximo 255 caracteres");

            if (idLog < 0)
                erros.Add("IdLog deve ser maior ou igual a zero");

            if (!string.IsNullOrEmpty(loja) && loja.Length > 255)
                erros.Add("Loja deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(observacao) && observacao.Length > 1255)
                erros.Add("Observacao deve ter no máximo 1255 caracteres");

            if (!string.IsNullOrEmpty(status) && status.Length > 100)
                erros.Add("Status deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(varejista) && varejista.Length > 255)
                erros.Add("Varejista deve ter no máximo 255 caracteres");

            if (!string.IsNullOrEmpty(vendedor) && vendedor.Length > 255)
                erros.Add("Vendedor deve ter no máximo 255 caracteres");

            return new ValidationResult(erros.Count == 0, erros);
        }

        public void AtualizarDados(
            string broker,
            string cdCartao,
            string cpfComprador,
            string dataCancelamento,
            string dataCriacao,
            string dataValidade,
            string dataVenda,
            int idLog,
            string loja,
            string observacao,
            string status,
            string varejista,
            string vendedor)
        {
            ValidarDados(
                broker,
                cdCartao,
                cpfComprador,
                dataCancelamento,
                dataCriacao,
                dataValidade,
                dataVenda,
                idLog,
                loja,
                observacao,
                status,
                varejista,
                vendedor);

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
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
