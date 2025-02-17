using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _mockSaleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetSaleHandler>> _mockLogger;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetSaleHandler>>();
            _handler = new GetSaleHandler(_mockSaleRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new GetSaleCommand(saleId);
            var sale = new Sale { Id = saleId, TotalAmount = 1000 };
            var mappedResult = new GetSaleCommandResult();

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(sale);

            _mockMapper.Setup(m => m.Map<GetSaleCommandResult>(It.IsAny<Sale>()))
                       .Returns(mappedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockSaleRepository.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<GetSaleCommandResult>(sale), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleNotFound()
        {
            // Arrange
            var command = new GetSaleCommand(Guid.NewGuid());

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync((Sale)null); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Sale Not Found!", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            var command = new GetSaleCommand(Guid.NewGuid());

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Database error", result.Errors);
           }
    }

}
