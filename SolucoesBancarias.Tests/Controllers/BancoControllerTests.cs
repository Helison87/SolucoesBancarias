using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SolucoesBancarias.Controllers;
using SolucoesBancarias.Service;
using static SolucoesBancarias.DTOs.DTOs;
using Xunit;

namespace SolucoesBancarias.Tests.Controllers
{
    public class BancoControllerTests
    {
        private readonly Mock<IServicosBancarios> _servicoMock;
        private readonly BancoController _controller;

        public BancoControllerTests()
        {
            _servicoMock = new Mock<IServicosBancarios>();
            _controller = new BancoController(_servicoMock.Object);
        }

        [Fact]
        public void Criar_DeveRetornarOkComGuid_QuandoChamado()
        {
            // Arrange
            var dto = new CriarContaDto { Proprietario = "João Silva" };
            var contaId = Guid.NewGuid();
            _servicoMock.Setup(s => s.CriarConta(dto.Proprietario)).Returns(contaId);

            // Act
            var resultado = _controller.Criar(dto);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(contaId);
            _servicoMock.Verify(s => s.CriarConta(dto.Proprietario), Times.Once);
        }

        [Fact]
        public void Depositar_DeveRetornarOkComNovoSaldo_QuandoChamado()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var dto = new ValorDto { Valor = 100m };
            var novoSaldo = 100m;
            _servicoMock.Setup(s => s.Depositar(contaId, dto.Valor)).Returns(novoSaldo);

            // Act
            var resultado = _controller.Depositar(contaId, dto);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(novoSaldo);
            _servicoMock.Verify(s => s.Depositar(contaId, dto.Valor), Times.Once);
        }

        [Fact]
        public void Sacar_DeveRetornarOkComNovoSaldo_QuandoChamado()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var dto = new ValorDto { Valor = 50m };
            var novoSaldo = 50m;
            _servicoMock.Setup(s => s.Sacar(contaId, dto.Valor)).Returns(novoSaldo);

            // Act
            var resultado = _controller.Sacar(contaId, dto);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(novoSaldo);
            _servicoMock.Verify(s => s.Sacar(contaId, dto.Valor), Times.Once);
        }

        [Fact]
        public void Transferir_DeveRetornarNoContent_QuandoChamado()
        {
            // Arrange
            var contaOrigemId = Guid.NewGuid();
            var contaDestinoId = Guid.NewGuid();
            var dto = new TransferenciaDto 
            { 
                ContaDestino = contaDestinoId, 
                Valor = 75m 
            };

            // Act
            var resultado = _controller.Transferir(contaOrigemId, dto);

            // Assert
            resultado.Should().BeOfType<NoContentResult>();
            _servicoMock.Verify(s => s.Transferir(contaOrigemId, dto.ContaDestino, dto.Valor), Times.Once);
        }

        [Fact]
        public void ObterSaldo_DeveRetornarOkComSaldo_QuandoChamado()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var saldo = 200m;
            _servicoMock.Setup(s => s.Saldo(contaId)).Returns(saldo);

            // Act
            var resultado = _controller.ObterSaldo(contaId);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(saldo);
            _servicoMock.Verify(s => s.Saldo(contaId), Times.Once);
        }

        [Fact]
        public void Depositar_DeveAtualizarSaldoCorretamente_QuandoMultiplosDepositos()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var dto1 = new ValorDto { Valor = 100m };
            var dto2 = new ValorDto { Valor = 50m };
            
            _servicoMock.Setup(s => s.Depositar(contaId, dto1.Valor)).Returns(100m);
            _servicoMock.Setup(s => s.Depositar(contaId, dto2.Valor)).Returns(150m);

            // Act
            var resultado1 = _controller.Depositar(contaId, dto1);
            var resultado2 = _controller.Depositar(contaId, dto2);

            // Assert
            var okResult1 = resultado1.Should().BeOfType<OkObjectResult>().Subject;
            okResult1.Value.Should().Be(100m);
            
            var okResult2 = resultado2.Should().BeOfType<OkObjectResult>().Subject;
            okResult2.Value.Should().Be(150m);
        }

        [Fact]
        public void Sacar_DeveReduzirSaldoCorretamente_QuandoChamado()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var dto = new ValorDto { Valor = 30m };
            _servicoMock.Setup(s => s.Sacar(contaId, dto.Valor)).Returns(70m);

            // Act
            var resultado = _controller.Sacar(contaId, dto);

            // Assert
            var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(70m);
        }
    }
}