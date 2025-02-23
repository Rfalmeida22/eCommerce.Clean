namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especificação para a entidade Broker.
    /// Verifica se um Broker satisfaz os critérios de negócio definidos.
    /// </summary>
    public class Broker : ISpecification<Entities.Broker>
    {
        /// <summary>
        /// Verifica se a entidade Broker satisfaz os critérios de negócio.
        /// </summary>
        /// <param name="broker">Instância da entidade Broker a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os critérios, False caso contrário.</returns>
        public bool IsSatisfiedBy(Entities.Broker broker)
        {
            return !string.IsNullOrEmpty(broker.NmBroker)
                && broker.IsActive;
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
 * public class BrokerService
 * {
 *     private readonly IBrokerRepository _brokerRepository;
 * 
 *     public BrokerService(IBrokerRepository brokerRepository)
 *     {
 *         _brokerRepository = brokerRepository;
 *     }
 * 
 *     public async Task<ValidationResult> AddBrokerAsync(Broker broker)
 *     {
 *         // Verificação se o broker satisfaz as especificações de negócio
 *         var brokerSpecification = new Specifications.Broker();
 *         if (!brokerSpecification.IsSatisfiedBy(broker))
 *         {
 *             return ValidationResult.Failure("Broker does not satisfy business specifications.");
 *         }
 * 
 *         // Adiciona o broker ao repositório
 *         await _brokerRepository.AddAsync(broker);
 * 
 *         return ValidationResult.Success();
 *     }
 * }
 */