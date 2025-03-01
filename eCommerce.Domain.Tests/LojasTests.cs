using eCommerce.Domain.Entities;
using eCommerce.Domain.Events;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace eCommerce.Domain.Tests
{
    /// <summary>
    /// Classe de testes para a entidade Lojas.
    /// </summary>
    public class LojasTests
    {
        #region Lojas Entity Tests

        /// <summary>
        /// Testes para a entidade Lojas.
        /// </summary>
        public class LojasEntityTests
        {
            /// <summary>
            /// Testa a criação de uma Loja com dados válidos.
            /// </summary>
            [Fact]
            public void CreateLoja_ValidData_ShouldCreateLoja()
            {
                // Arrange
                var cdCnpj = "12345678000195";
                var cdLoja = "001";
                var idLojista = 1;
                var idVarejista = 1;
                var nmLoja = "Loja Teste";
                var txEndereco = "Endereço Teste";
                var createdBy = "Usuário Teste";

                // Act
                var loja = Lojas.Create(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco, createdBy);

                // Assert
                Assert.NotNull(loja);
                Assert.Equal(cdCnpj, loja.CdCnpj);
                Assert.Equal(cdLoja, loja.CdLoja);
                Assert.Equal(idLojista, loja.IdLojista);
                Assert.Equal(idVarejista, loja.IdVarejista);
                Assert.Equal(nmLoja, loja.NmLoja);
                Assert.Equal(txEndereco, loja.TxEndereco);
                Assert.Equal(createdBy, loja.CreatedBy);
            }

            /// <summary>
            /// Testa a criação de uma Loja com dados inválidos.
            /// </summary>
            [Fact]
            public void CreateLoja_InvalidData_ShouldThrowDomainException()
            {
                // Arrange
                var cdCnpj = ""; // CNPJ inválido
                var cdLoja = "001";
                var idLojista = 1;
                var idVarejista = 1;
                var nmLoja = "Loja Teste";
                var txEndereco = "Endereço Teste";
                var createdBy = "Usuário Teste";

                // Act & Assert
                Assert.Throws<DomainException>(() => Lojas.Create(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco, createdBy));
            }

            /// <summary>
            /// Testa a atualização dos dados de uma Loja com dados válidos.
            /// </summary>
            [Fact]
            public void AtualizarDados_ValidData_ShouldUpdateLoja()
            {
                // Arrange
                var loja = Lojas.Create("12345678000195", "001", 1, 1, "Loja Teste", "Endereço Teste", "Usuário Teste");
                var novoCnpj = "98765432000195";
                var novoCdLoja = "002";
                var novoIdLojista = 2;
                var novoIdVarejista = 2;
                var novoNmLoja = "Loja Teste Atualizada";
                var novoTxEndereco = "Endereço Teste Atualizado";
                var updatedBy = "Usuário Atualizado";

                // Act
                loja.AtualizarDados(novoCnpj, novoCdLoja, novoIdLojista, novoIdVarejista, novoNmLoja, novoTxEndereco, updatedBy);

                // Assert
                Assert.Equal(novoCnpj, loja.CdCnpj);
                Assert.Equal(novoCdLoja, loja.CdLoja);
                Assert.Equal(novoIdLojista, loja.IdLojista);
                Assert.Equal(novoIdVarejista, loja.IdVarejista);
                Assert.Equal(novoNmLoja, loja.NmLoja);
                Assert.Equal(novoTxEndereco, loja.TxEndereco);
                Assert.Equal(updatedBy, loja.UpdatedBy);
            }

            /// <summary>
            /// Testa a atualização dos dados de uma Loja com dados inválidos.
            /// </summary>
            [Fact]
            public void AtualizarDados_InvalidData_ShouldThrowDomainException()
            {
                // Arrange
                var loja = Lojas.Create("12345678000195", "001", 1, 1, "Loja Teste", "Endereço Teste", "Usuário Teste");
                var novoCnpj = ""; // CNPJ inválido
                var novoCdLoja = "002";
                var novoIdLojista = 2;
                var novoIdVarejista = 2;
                var novoNmLoja = "Loja Teste Atualizada";
                var novoTxEndereco = "Endereço Teste Atualizado";
                var updatedBy = "Usuário Atualizado";

                // Act & Assert
                Assert.Throws<DomainException>(() => loja.AtualizarDados(novoCnpj, novoCdLoja, novoIdLojista, novoIdVarejista, novoNmLoja, novoTxEndereco, updatedBy));
            }
        }

        #endregion

        #region Loja Service Tests

        /// <summary>
        /// Testes para o serviço de domínio LojaService.
        /// </summary>
        public class LojaServiceTests
        {
            private readonly Mock<ILoja> _lojaRepositoryMock;
            private readonly Mock<IEventDispatcher> _eventDispatcherMock;
            private readonly Mock<ILogger<Lojas>> _loggerMock;
            private readonly Services.Loja _lojaService;

            public LojaServiceTests()
            {
                _lojaRepositoryMock = new Mock<ILoja>();
                _eventDispatcherMock = new Mock<IEventDispatcher>();
                _loggerMock = new Mock<ILogger<Entities.Lojas>>();
                _lojaService = new Services.Loja(_lojaRepositoryMock.Object, _eventDispatcherMock.Object, new Mock<ILogger<Services.Loja>>().Object);
            }

            /// <summary>
            /// Testa a validação de vínculo de uma Loja com um Varejista e a publicação de um evento.
            /// </summary>
            [Fact]
            public async Task ValidarVinculoVarejistaAsync_ValidData_ShouldPublishEvent()
            {
                // Arrange
                var lojaId = 1;
                var varejistaId = 1;
                var loja = Lojas.Create("12345678000195", "001", 1, varejistaId, "Loja Teste", "Endereço Teste", "Usuário Teste");
                var idLojaProperty = loja.GetType().GetProperty("IdLoja");
                if (idLojaProperty != null)
                {
                    idLojaProperty.SetValue(loja, lojaId);
                }
                var lojas = new List<Lojas> { loja };
                _lojaRepositoryMock.Setup(repo => repo.GetByVarejistaIdAsync(varejistaId)).ReturnsAsync(lojas);

                // Act
                var result = await _lojaService.ValidarVinculoVarejistaAsync(lojaId, varejistaId);

                // Assert
                Assert.True(result);
                _eventDispatcherMock.Verify(dispatcher => dispatcher.PublishAsync(It.IsAny<Events.Loja>()), Times.Once);
            }

            /// <summary>
            /// Testa a validação de vínculo de uma Loja com um Varejista com um ID de Loja inválido.
            /// </summary>
            [Fact]
            public async Task ValidarVinculoVarejistaAsync_InvalidLojaId_ShouldThrowArgumentException()
            {
                // Arrange
                var lojaId = 0; // ID inválido
                var varejistaId = 1;

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentException>(() => _lojaService.ValidarVinculoVarejistaAsync(lojaId, varejistaId));
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
 * - LojasEntityTests: Testes para a entidade Lojas, incluindo criação e atualização.
 * 
 * Ferramentas Utilizadas:
 * 
 * - xUnit: Framework de testes utilizado para escrever e executar os testes.
 * - Moq: Biblioteca de mocking utilizada para criar mocks de dependências.
 * - Microsoft.Extensions.Logging: Biblioteca de logging utilizada para registrar logs durante os testes.
 */

