namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especifica��o para a entidade Broker.
    /// Verifica se um Broker satisfaz os crit�rios de neg�cio definidos.
    /// </summary>
    public class Broker : ISpecification<Entities.Broker>
    {
        /// <summary>
        /// Verifica se a entidade Broker satisfaz os crit�rios de neg�cio.
        /// </summary>
        /// <param name="broker">Inst�ncia da entidade Broker a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os crit�rios, False caso contr�rio.</returns>
        public bool IsSatisfiedBy(Entities.Broker broker)
        {
            return !string.IsNullOrEmpty(broker.NmBroker)
                && broker.IsActive;
        }
    }
}

/*
 * O que � uma Especifica��o?
 * 
 * A especifica��o � um padr�o de design que encapsula crit�rios de neg�cio ou regras de valida��o em uma classe separada.
 * Isso permite que as regras sejam reutilizadas e combinadas de forma flex�vel, promovendo a separa��o de responsabilidades
 * e a manuten��o do c�digo.
 * 
 * Exemplo de Utiliza��o:
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
 *         // Verifica��o se o broker satisfaz as especifica��es de neg�cio
 *         var brokerSpecification = new Specifications.Broker();
 *         if (!brokerSpecification.IsSatisfiedBy(broker))
 *         {
 *             return ValidationResult.Failure("Broker does not satisfy business specifications.");
 *         }
 * 
 *         // Adiciona o broker ao reposit�rio
 *         await _brokerRepository.AddAsync(broker);
 * 
 *         return ValidationResult.Success();
 *     }
 * }
 */