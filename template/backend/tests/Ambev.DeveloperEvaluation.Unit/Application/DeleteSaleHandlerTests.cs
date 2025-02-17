using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<DeleteSaleNotificationService> _notificationServiceMock;
        private readonly Mock<ILogger<DeleteSaleHandler>> _loggerMock;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _notificationServiceMock = new Mock<DeleteSaleNotificationService>();
            _loggerMock = new Mock<ILogger<DeleteSaleHandler>>();

            _handler = new DeleteSaleHandler(
                _saleRepositoryMock.Object,
                _notificationServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCancelSale_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);
            var sale = new Sale { Id = saleId, IsCancelled = false };

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);
            _saleRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _notificationServiceMock.Setup(service => service.Notify(It.IsAny<SaleCancelledEvent>()))
                .Returns("Notification Sent");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.False(result.Errors.Any());
            Assert.True(sale.IsCancelled);
            _saleRepositoryMock.Verify(repo => repo.UpdateAsync(sale, It.IsAny<CancellationToken>()), Times.Once);
            _notificationServiceMock.Verify(service => service.Notify(It.IsAny<SaleCancelledEvent>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleDoesNotExist()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Sale)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Resource (Sale) Not Found", result.Errors);
            _saleRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
            _notificationServiceMock.Verify(service => service.Notify(It.IsAny<SaleCancelledEvent>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Database error", result.Errors);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Database error")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once
            );
        }
    }

}
