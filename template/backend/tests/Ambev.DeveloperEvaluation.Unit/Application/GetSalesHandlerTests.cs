using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{

    public class GetSalesHandlerTests
    {
        private readonly Mock<ISaleRepository> _mockSaleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetSalesHandler>> _mockLogger;
        private readonly GetSalesHandler _handler;

        public GetSalesHandlerTests()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetSalesHandler>>();
            _handler = new GetSalesHandler(_mockSaleRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSalesFound()
        {
            // Arrange
            var command = new GetSalesCommand(0, 10);
            var sales = new List<Sale>
        {
            new Sale { Id = Guid.NewGuid(), TotalAmount = 100 },
            new Sale { Id = Guid.NewGuid(), TotalAmount = 200 }
        };

            var mappedResults = new List<GetSaleCommandResult>
        {
            new GetSaleCommandResult(),
            new GetSaleCommandResult()
        };

            _mockSaleRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(sales);

            _mockMapper.Setup(m => m.Map<IEnumerable<GetSaleCommandResult>>(It.IsAny<IEnumerable<Sale>>()))
                       .Returns(mappedResults);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result?.Sales?.Count());
            _mockSaleRepository.Verify(repo => repo.GetAllAsync(command.Skip, command.Take, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<GetSaleCommandResult>>(sales), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenNoSalesFound()
        {
            // Arrange
            var command = new GetSalesCommand(0,10);

            _mockSaleRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync((List<Sale>)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Sales Not Found!", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            var command = new GetSalesCommand (0, 10);

            _mockSaleRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                               .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Database error", result.Errors);
        }
    }

}
