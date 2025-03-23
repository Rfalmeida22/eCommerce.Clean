using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Broker
    /// </summary>
    public interface IBroker : IRepository<Broker>
    {
        /// <summary>
        /// Busca broker por nome
        /// </summary>
        Task<Broker> GetByNomeAsync(string nome);

        /// <summary>
        /// Verifica se existe broker com o mesmo nome
        /// </summary>
        Task<bool> ExistsByNomeAsync(string nome, int? ignorarId = null);

        /// <summary>
        /// Obtém brokers por varejista
        /// </summary>
        Task<IEnumerable<Broker>> GetByVarejistaIdAsync(int varejistaId);
    }
}

/*
 * Interface IBroker (eCommerce.Domain/Interfaces/IBroker.cs)
 * 
 * Objetivo:
 * Define um contrato para operações de persistência relacionadas à entidade Broker.
 * Herda de IRepository<Broker> para operações CRUD básicas e adiciona operações específicas
 * para a entidade Broker.
 * 
 * Características:
 * 1. Herda IRepository<Broker>:
 *    - GetByIdAsync(int id)
 *    - GetAllAsync()
 *    - AddAsync(Broker entity)
 *    - UpdateAsync(Broker entity)
 *    - DeleteAsync(int id)
 *    - ExistsAsync(int id)
 * 
 * 2. Operações Específicas:
 *    - GetByNomeAsync: Busca broker pelo nome
 *    - ExistsByNomeAsync: Verifica duplicidade de nome
 *    - GetByVarejistaIdAsync: Lista brokers por varejista
 * 
 * Exemplos de Uso:
 * 
 * 1. Injeção de Dependência (Services/Broker.cs):
 *    public class BrokerService
 *    {
 *        private readonly IBroker _brokerRepository;
 *        
 *        public BrokerService(IBroker brokerRepository)
 *        {
 *            _brokerRepository = brokerRepository;
 *        }
 *    }
 * 
 * 2. Verificar Duplicidade de Nome:
 *    // Verifica se existe outro broker com mesmo nome (ignorando o próprio registro)
 *    public async Task<bool> ValidarNomeUnico(string nome, int brokerId)
 *    {
 *        return !await _brokerRepository.ExistsByNomeAsync(nome, ignorarId: brokerId);
 *    }
 * 
 * 3. Buscar Broker por Nome:
 *    // Busca um broker específico pelo nome
 *    public async Task<Broker> ObterPorNome(string nome)
 *    {
 *        return await _brokerRepository.GetByNomeAsync(nome);
 *    }
 * 
 * 4. Listar Brokers por Varejista:
 *    // Lista todos os brokers associados a um varejista
 *    public async Task<IEnumerable<Broker>> ListarPorVarejista(int varejistaId)
 *    {
 *        return await _brokerRepository.GetByVarejistaIdAsync(varejistaId);
 *    }
 * 
 * Implementação:
 * - A interface é implementada pela classe Broker no projeto Infrastructure
 * - Utiliza Entity Framework Core para operações no banco de dados
 * - Todas as operações são assíncronas (retornam Task)
 * 
 * Observações:
 * - Segue o padrão Repository para abstração do acesso a dados
 * - Permite mock em testes unitários
 * - Facilita a substituição da implementação do repositório
 * - Mantém a separação de responsabilidades (SRP)
 * 
 * Esta interface é fundamental para:
 * 1. Manter a independência entre domínio e infraestrutura
 * 2. Facilitar testes unitários através de mocks
 * 3. Permitir diferentes implementações de persistência
 * 4. Definir um contrato claro para operações com Brokers
 */
