using eCommerce.Domain.Entities;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.Services;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Events;
using eCommerce.Domain.EventHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace eCommerce.Domain.Tests
{
    /// <summary>
    /// Classe de testes para a entidade Broker, serviço de domínio Broker e manipulador de eventos Broker.
    /// </summary>
    public class BrokerTests
    {
        #region Broker Entity Tests

        /// <summary>
        /// Testes para a entidade Broker.
        /// </summary>
        public class BrokerEntityTests
        {
            /// <summary>
            /// Testa a criação de um Broker com dados válidos.
            /// </summary>
            [Fact]
            public void CreateBroker_ValidData_ShouldCreateBroker()
            {
                // Arrange
                var nome = "Broker Teste";
                var createdBy = "Usuário Teste";

                // Act
                var broker = Entities.Broker.Create(nome, createdBy);

                // Assert
                Assert.NotNull(broker);
                Assert.Equal(nome, broker.NmBroker);
                Assert.Equal(createdBy, broker.CreatedBy);
                Assert.True(broker.IsActive);
            }

            /// <summary>
            /// Testa a criação de um Broker com dados inválidos.
            /// </summary>
            [Fact]
            public void CreateBroker_InvalidData_ShouldThrowDomainException()
            {
                // Arrange
                var nome = ""; // Nome inválido
                var createdBy = "Usuário Teste";

                // Act & Assert
                Assert.Throws<DomainException>(() => Entities.Broker.Create(nome, createdBy));
            }
        }

        #endregion

        #region Broker Service Tests

        /// <summary>
        /// Testes para o serviço de domínio Broker.
        /// </summary>
        public class BrokerServiceTests
        {
            private readonly Mock<IBroker> _brokerRepositoryMock;
            private readonly Mock<IEventDispatcher> _eventDispatcherMock;
            private readonly Mock<ILogger<Entities.Broker>> _loggerMock;
            private readonly Services.Broker _brokerService;

            public BrokerServiceTests()
            {
                _brokerRepositoryMock = new Mock<IBroker>();
                _eventDispatcherMock = new Mock<IEventDispatcher>();
                _loggerMock = new Mock<ILogger<Entities.Broker>>();
                _brokerService = new Services.Broker(_brokerRepositoryMock.Object, _eventDispatcherMock.Object, new Mock<ILogger<Services.Broker>>().Object);
            }

            /// <summary>
            /// Testa a validação de vínculo de um Broker com um Varejista e a publicação de um evento.
            /// </summary>
            [Fact]
            public async Task ValidarVinculoVarejistaAsync_ValidData_ShouldPublishEvent()
            {
                // Arrange
                var brokerId = 1;
                var varejistaId = 1;
                var broker = Entities.Broker.Create("Broker Teste", "Usuário Teste");
                var brokerProperty = broker.GetType().GetProperty("IdBroker");
                if (brokerProperty != null)
                {
                    brokerProperty.SetValue(broker, brokerId);
                }
                var brokers = new List<Entities.Broker> { broker };
                _brokerRepositoryMock.Setup(repo => repo.GetByVarejistaIdAsync(varejistaId)).ReturnsAsync(brokers);

                // Act
                var result = await _brokerService.ValidarVinculoVarejistaAsync(brokerId, varejistaId);

                // Assert
                Assert.True(result);
                _eventDispatcherMock.Verify(dispatcher => dispatcher.PublishAsync(It.IsAny<Events.Broker>()), Times.Once);
            }

            /// <summary>
            /// Testa a validação de vínculo de um Broker com um Varejista com um ID de Broker inválido.
            /// </summary>
            [Fact]
            public async Task ValidarVinculoVarejistaAsync_InvalidBrokerId_ShouldThrowArgumentException()
            {
                // Arrange
                var brokerId = 0; // ID inválido
                var varejistaId = 1;

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentException>(() => _brokerService.ValidarVinculoVarejistaAsync(brokerId, varejistaId));
            }
        }

        #endregion

        #region Broker Event Handler Tests

        /// <summary>
        /// Testes para o manipulador de eventos BrokerEventHandler.
        /// </summary>
        public class BrokerEventHandlerTests
        {
            private readonly Mock<ILogger<EventHandlers.Broker>> _loggerMock;
            private readonly EventHandlers.Broker _handler;

            public BrokerEventHandlerTests()
            {
                _loggerMock = new Mock<ILogger<EventHandlers.Broker>>();
                _handler = new EventHandlers.Broker(_loggerMock.Object);
            }

            /// <summary>
            /// Testa o tratamento de um evento Broker e a geração de logs.
            /// </summary>
            [Fact]
            public async Task HandleAsync_ValidEvent_ShouldLogInformation()
            {
                // Arrange
                var brokerEvent = new Events.Broker(1, "Broker Teste", "Usuário Teste");

                // Act
                await _handler.HandleAsync(brokerEvent);

                // Assert
                _loggerMock.Verify(logger => logger.LogInformation("Broker atualizado: {BrokerId}, Nome: {Nome}", brokerEvent.BrokerId, brokerEvent.Nome), Times.Once);
            }
        }

        #endregion
    }
}

/*
 * Como Executar os Testes:
 * 
 * 1. Certifique-se de que o projeto de testes (eCommerce.Domain.Tests) esteja configurado corretamente.
 * 2. Abra o Test Explorer no Visual Studio (menu Test > Test Explorer).
 * 3. Compile a solução para garantir que todos os testes sejam descobertos.
 * 4. No Test Explorer, você verá uma lista de todos os testes disponíveis.
 * 5. Clique com o botão direito em um teste específico ou em um grupo de testes e selecione "Run" para executar os testes.
 * 6. Verifique os resultados dos testes no Test Explorer.
 * 
 * Estrutura dos Testes:
 * 
 * - BrokerEntityTests: Testes para a entidade Broker, incluindo criação e validação.
 * - BrokerServiceTests: Testes para o serviço de domínio Broker, incluindo validação de vínculo e publicação de eventos.
 * - BrokerEventHandlerTests: Testes para o manipulador de eventos BrokerEventHandler, verificando o tratamento de eventos e geração de logs.
 * 
 * Ferramentas Utilizadas:
 * 
 * - xUnit: Framework de testes utilizado para escrever e executar os testes.
 * - Moq: Biblioteca de mocking utilizada para criar mocks de dependências.
 * - Microsoft.Extensions.Logging: Biblioteca de logging utilizada para registrar logs durante os testes.
 */