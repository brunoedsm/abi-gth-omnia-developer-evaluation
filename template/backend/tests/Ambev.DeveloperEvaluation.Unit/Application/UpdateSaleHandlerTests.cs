using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class UpdateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _mockSaleRepository;
        private readonly Mock<UpdateSaleNotificationService> _mockNotificationService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<UpdateSaleHandler>> _mockLogger;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockNotificationService = new Mock<UpdateSaleNotificationService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UpdateSaleHandler>>();
            _handler = new UpdateSaleHandler(_mockSaleRepository.Object, _mockNotificationService.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSaleUpdated()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new UpdateSaleCommand
            {
                Id = saleId,
                Items =
                [
                  new UpdateSaleItemCommand() { Product = "Product One", Quantity = 5, Discount = 10 },
                  new UpdateSaleItemCommand() { Product = "Product One", Quantity = 5, Discount = 10 },
                  new UpdateSaleItemCommand() { Product = "Product One", Quantity = 5, Discount = 10 },
                  new UpdateSaleItemCommand() { Product = "Product One", Quantity = 5, Discount = 10 }
                ]
            };
            var existingSale = SaleTestData.FeedSale(saleId, 4, 10, 5);
            var mappedSale = existingSale;

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(existingSale);
            _mockMapper.Setup(m => m.Map<Sale>(It.IsAny<UpdateSaleCommand>()))
                       .Returns(mappedSale);
            _mockSaleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
                               .Returns(Task.CompletedTask);
            _mockNotificationService.Setup(service => service.Notify(It.IsAny<SaleModifiedEvent>()))
                                    .Returns("Notification sent");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockSaleRepository.Verify(repo => repo.UpdateAsync(mappedSale, It.IsAny<CancellationToken>()), Times.Once);
            _mockNotificationService.Verify(service => service.Notify(It.IsAny<SaleModifiedEvent>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleNotFound()
        {
            // Arrange
            var command = new UpdateSaleCommand { Id = Guid.NewGuid() };

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync((Sale)null); // Sale not found 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Resource Not Found", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleItemLimitSpecFails()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new UpdateSaleCommand
            {
                Id = saleId,
                Items =
                 [
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 }
                ]
            };
            Sale existingSale = SaleTestData.FeedSale(saleId, 20, 1, 10);

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(existingSale);
            _mockMapper.Setup(m => m.Map<Sale>(It.IsAny<UpdateSaleCommand>()))
                       .Returns(existingSale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Cannot sell more than 20 items per product.", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleItemDiscountSpecFails()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new UpdateSaleCommand
            {
                Id = saleId,
                Items =
                 [
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, Discount = 10 },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, Discount = 10  },
                   new UpdateSaleItemCommand(){ Product = "Product 1", Quantity = 1, Discount = 10  }
                 ]
            };

            var existingSale = SaleTestData.FeedSale(saleId, 3, 10, 1);

            _mockSaleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingSale);
            _mockMapper.Setup(m => m.Map<Sale>(It.IsAny<UpdateSaleCommand>()))
                   .Returns(existingSale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Cannot apply discount to less than 4 items.", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            var command = new UpdateSaleCommand { Id = Guid.NewGuid() };

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
