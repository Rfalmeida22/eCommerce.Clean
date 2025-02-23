namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especificação para a entidade Brokers_Varejistas.
    /// Verifica se um relacionamento entre Broker e Varejista satisfaz os critérios de negócio definidos.
    /// </summary>
    public class BrokerVarejista : ISpecification<Entities.Brokers_Varejistas>
    {
        /// <summary>
        /// Verifica se o relacionamento entre Broker e Varejista satisfaz os critérios de negócio.
        /// </summary>
        /// <param name="relacionamento">Instância da entidade Brokers_Varejistas a ser verificada.</param>
        /// <returns>True se o relacionamento satisfaz os critérios, False caso contrário.</returns>
        public bool IsSatisfiedBy(Entities.Brokers_Varejistas relacionamento)
        {
            return relacionamento.IdBroker > 0
                && relacionamento.IdVarejista > 0
                && relacionamento.IsActive;
        }
    }
}

/*
 * O que é uma Especificação?
 * 
 * A especificação é um padrão de design que encapsula critérios de negócio ou regras de validação em uma classe separada.
 * Isso permite que as regras sejam reutilizadas e combinadas de forma flexível, promovendo a separação de responsabilidades
 * e a manutenção do código.
 * 
 * Exemplo de Utilização:
 * 
 * using eCommerce.Domain.Entities;
 * using eCommerce.Domain.Specifications;
 * 
 * public class BrokerVarejistaService
 * {
 *     private readonly IBrokerVarejistaRepository _brokerVarejistaRepository;
 * 
 *     public BrokerVarejistaService(IBrokerVarejistaRepository brokerVarejistaRepository)
 *     {
 *         _brokerVarejistaRepository = brokerVarejistaRepository;
 *     }
 * 
 *     public async Task<ValidationResult> AddBrokerVarejistaAsync(Brokers_Varejistas relacionamento)
 *     {
 *         // Verificação se o relacionamento satisfaz as especificações de negócio
 *         var brokerVarejistaSpecification = new Specifications.BrokerVarejista();
 *         if (!brokerVarejistaSpecification.IsSatisfiedBy(relacionamento))
 *         {
 *             return ValidationResult.Failure("Relacionamento entre Broker e Varejista não satisfaz as especificações de negócio.");
 *         }
 * 
 *         // Adiciona o relacionamento ao repositório
 *         await _brokerVarejistaRepository.AddAsync(relacionamento);
 * 
 *         return ValidationResult.Success();
 *     }
 * }
 */