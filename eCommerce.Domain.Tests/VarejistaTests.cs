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
    /// Classe de testes para a entidade Varejista, serviço de domínio Varejista e manipulador de eventos Varejista.
    /// </summary>
    public class VarejistaTests
    {
        #region Varejista Entity Tests

        /// <summary>
        /// Testes para a entidade Varejista.
        /// </summary>
        public class VarejistaEntityTests
        {
            /// <summary>
            /// Testa a criação de um Varejista com dados válidos.
            /// </summary>
            [Fact]
            public void CreateVarejista_ValidData_ShouldCreateVarejista()
            {
                // Arrange
                var cnpj = "12345678901234";
                var banner = "Banner Teste";
                var corFundo = "#FFFFFF";
                var codigo = "COD123";
                var nome = "Varejista Teste";
                var site = "https://teste.com";
                var createdBy = "Usuário Teste";

                // Act
                var varejista = Entities.Varejista.Create(cnpj, banner, corFundo, codigo, nome, site, createdBy);

                // Assert
                Assert.NotNull(varejista);
                Assert.Equal(cnpj, varejista.CdCnpj);
                Assert.Equal(nome, varejista.NmVarejista);
                Assert.Equal(createdBy, varejista.CreatedBy);
                Assert.True(varejista.IsActive);
            }

            /// <summary>
            /// Testa a criação de um Varejista com CNPJ inválido.
            /// </summary>
            [Fact]
            public void CreateVarejista_InvalidCnpj_ShouldThrowDomainException()
            {
                // Arrange
                var cnpj = "123"; // CNPJ inválido
                var banner = "Banner Teste";
                var corFundo = "#FFFFFF";
                var codigo = "COD123";
                var nome = "Varejista Teste";
                var site = "https://teste.com";
                var createdBy = "Usuário Teste";

                // Act & Assert
                Assert.Throws<DomainException>(() =>
                    Entities.Varejista.Create(cnpj, banner, corFundo, codigo, nome, site, createdBy));
            }
        }

        #endregion

        #region Varejista Service Tests

        /// <summary>
        /// Testes para o serviço de domínio Varejista.
        /// </summary>
        public class VarejistaServiceTests
        {
            private readonly Mock<IVarejista> _varejistaRepositoryMock;
            private readonly Mock<IEventDispatcher> _eventDispatcherMock;
            private readonly Mock<ILogger<Services.Varejista>> _loggerMock;
            private readonly Services.Varejista _varejistaService;

            public VarejistaServiceTests()
            {
                _varejistaRepositoryMock = new Mock<IVarejista>();
                _eventDispatcherMock = new Mock<IEventDispatcher>();
                _loggerMock = new Mock<ILogger<Services.Varejista>>();
                _varejistaService = new Services.Varejista(_varejistaRepositoryMock.Object,
                    _eventDispatcherMock.Object,
                    _loggerMock.Object
                );
            }

            /// <summary>
            /// Testa o cadastro de um Varejista com dados válidos.
            /// </summary>
            [Fact]
            public async Task CadastrarVarejistaAsync_ValidData_ShouldCreateVarejista()
            {
                // Arrange
                var cnpj = "12345678901234";
                var banner = "Banner Teste";
                var corFundo = "#FFFFFF";
                var codigo = "COD123";
                var nome = "Varejista Teste";
                var site = "https://teste.com";
                var createdBy = "Usuário Teste";

                _varejistaRepositoryMock.Setup(repo => repo.ExistsByNomeAsync(nome, null)).ReturnsAsync(false);
                _varejistaRepositoryMock.Setup(repo => repo.GetByCnpjAsync(cnpj)).ReturnsAsync((Domain.Entities.Varejista)null);
                _varejistaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Varejista>())).Returns(Task.CompletedTask);

                // Act
                var result = await _varejistaService.CadastrarVarejistaAsync(
                    cnpj, banner, corFundo, codigo, nome, site, createdBy);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(nome, result.NmVarejista);
                Assert.Equal(cnpj, result.CdCnpj);
                _varejistaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.Varejista>()), Times.Once);
            }

            /// <summary>
            /// Testa a atualização de um Varejista com dados válidos.
            /// </summary>
            [Fact]
            public async Task AtualizarVarejistaAsync_ValidData_ShouldUpdateVarejista()
            {
                // Arrange
                var varejistaId = 1;
                var cnpj = "12345678901234";
                var banner = "Banner Atualizado";
                var corFundo = "#000000";
                var codigo = "COD456";
                var nome = "Varejista Atualizado";
                var site = "https://teste-atualizado.com";
                var updatedBy = "Usuário Teste";

                var varejistaExistente = Entities.Varejista.Create(
                    "98765432109876", "Banner Original", "#FFFFFF",
                    "COD123", "Varejista Original", "https://teste.com",
                    "Usuário Original"
                );

                _varejistaRepositoryMock.Setup(repo => repo.GetByIdAsync(varejistaId)).ReturnsAsync(varejistaExistente);
                _varejistaRepositoryMock.Setup(repo => repo.ExistsByNomeAsync(nome, varejistaId)).ReturnsAsync(false);
                _varejistaRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Varejista>())).Returns(Task.CompletedTask);

                // Act
                var result = await _varejistaService.AtualizarVarejistaAsync(varejistaId, cnpj, banner, corFundo, codigo, nome, site, updatedBy);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(nome, result.NmVarejista);
                Assert.Equal(cnpj, result.CdCnpj);

                _varejistaRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Varejista>()), Times.Once);
            }

            
        }

        #endregion

        #region Varejista Event Handler Tests

        /// <summary>
        /// Testes para o manipulador de eventos VarejistaEventHandler.
        /// </summary>
        public class VarejistaEventHandlerTests
        {
            private readonly Mock<ILogger<EventHandlers.Varejista>> _loggerMock;
            private readonly EventHandlers.Varejista _handler;

            public VarejistaEventHandlerTests()
            {
                _loggerMock = new Mock<ILogger<EventHandlers.Varejista>>();
                _handler = new EventHandlers.Varejista(_loggerMock.Object);
            }

            /// <summary>
            /// Testa o tratamento de um evento Varejista e a geração de logs.
            /// </summary>
            [Fact]
            public async Task HandleAsync_ValidEvent_ShouldLogInformation()
            {
                // Arrange
                var varejistaEvent = new Events.Varejista(1, "Varejista Teste", "12345678901234", "Usuário Teste");

                // Act
                await _handler.HandleAsync(varejistaEvent);

                // Assert
                _loggerMock.Verify(
                    logger => logger.LogInformation(
                        "Varejista atualizado: {VarejistaId}, Nome: {Nome}, CNPJ: {Cnpj}",
                        varejistaEvent.VarejistaId,
                        varejistaEvent.Nome,
                        varejistaEvent.Cnpj
                    ),
                    Times.Once
                );
            }
        }

        #endregion
    }
}
