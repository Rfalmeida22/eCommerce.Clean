using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class Broker : BaseEntity
{
    public int IdBroker { get; protected set; }
    public string NmBroker { get; protected set; }

    // Construtor para Entity Framework
    protected Broker() { }

    // Construtor para uso normal
    public Broker(string nmBroker)
    {
        ValidarNome(nmBroker);
        NmBroker = nmBroker;
    }

    // Método de validação
    public ValidationResult Validar()
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(NmBroker))
            erros.Add("Nome do broker é obrigatório");

        if (NmBroker?.Length > 100)
            erros.Add("Nome do broker deve ter no máximo 100 caracteres");

        return new ValidationResult(erros.Count == 0, erros);
    }

    // Método para atualizar o nome
    public void AtualizarNome(string novoNome)
    {
        ValidarNome(novoNome);
        NmBroker = novoNome;
        DataAtualizacao = DateTime.UtcNow;
    }

    // Método privado para validação do nome
    private void ValidarNome(string nome)
    {
        if (nome == null)
            throw new DomainException("Nome do broker não pode ser nulo");

        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome do broker é obrigatório");

        if (nome.Length > 100)
            throw new DomainException("Nome do broker deve ter no máximo 100 caracteres");
    }
}
}
