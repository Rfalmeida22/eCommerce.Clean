namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especifica��o para a entidade Brokers_Varejistas.
    /// Verifica se um relacionamento entre Broker e Varejista satisfaz os crit�rios de neg�cio definidos.
    /// </summary>
    public class BrokerVarejista : ISpecification<Entities.Brokers_Varejistas>
    {
        /// <summary>
        /// Verifica se o relacionamento entre Broker e Varejista satisfaz os crit�rios de neg�cio.
        /// </summary>
        /// <param name="relacionamento">Inst�ncia da entidade Brokers_Varejistas a ser verificada.</param>
        /// <returns>True se o relacionamento satisfaz os crit�rios, False caso contr�rio.</returns>
        public bool IsSatisfiedBy(Entities.Brokers_Varejistas relacionamento)
        {
            return relacionamento.IdBroker > 0
                && relacionamento.IdVarejista > 0
                && relacionamento.IsActive;
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
 *         // Verifica��o se o relacionamento satisfaz as especifica��es de neg�cio
 *         var brokerVarejistaSpecification = new Specifications.BrokerVarejista();
 *         if (!brokerVarejistaSpecification.IsSatisfiedBy(relacionamento))
 *         {
 *             return ValidationResult.Failure("Relacionamento entre Broker e Varejista n�o satisfaz as especifica��es de neg�cio.");
 *         }
 * 
 *         // Adiciona o relacionamento ao reposit�rio
 *         await _brokerVarejistaRepository.AddAsync(relacionamento);
 * 
 *         return ValidationResult.Success();
 *     }
 * }
 */