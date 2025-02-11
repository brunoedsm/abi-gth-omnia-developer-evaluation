using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CreateSaleHandler>> _loggerMock;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>());
            _mapper = new Mapper(mapperConfig);
            _loggerMock = new Mock<ILogger<CreateSaleHandler>>();
            _handler = new CreateSaleHandler(_saleRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateSaleSuccessfully_WhenValidCommandIsGiven()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = "Customer 1",
                TotalAmount = 12.5m,
                Branch = "Branch 1",
                Date = DateTime.Now,
                IsCancelled = false,
                Items =
                  [
                    new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 2.5m },
                    new CreateSaleItemCommand(){ Product = "Product 2", Quantity = 1, UnitPrice = 2.5m },
                    new CreateSaleItemCommand(){ Product = "Product 3", Quantity = 1, UnitPrice = 2.5m },
                    new CreateSaleItemCommand(){ Product = "Product 4", Quantity = 1, UnitPrice = 2.5m },
                    new CreateSaleItemCommand(){ Product = "Product 5", Quantity = 1, UnitPrice = 2.5m }
                ]
            };

            var sale = new Sale() { Id = Guid.NewGuid() };
            _saleRepositoryMock.Setup(repo => repo.CreateAsync(sale, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotEqual(result.Id, Guid.Empty);
            _saleRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenSaleExceedsItemLimit()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = "Customer 1",
                TotalAmount = 220,
                Branch = "Branch 1",
                Date = DateTime.Now,
                IsCancelled = false,
                Items =
                 [
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 },
                   new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 10 }
               ]
            };
            var sale = new Sale();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Cannot sell more than 20 items per product.", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenDiscountConditionFails()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = "Customer 1",
                TotalAmount = 7.5m,
                Branch = "Branch 1",
                Date = DateTime.Now,
                IsCancelled = false,
                Items =
                [
                    new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 2.5m, Discount = 1 },
                    new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 2.5m, Discount = 1 },
                    new CreateSaleItemCommand(){ Product = "Product 1", Quantity = 1, UnitPrice = 2.5m, Discount = 1 },
                ]
            };
            var sale = new Sale();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Cannot apply discount to less than 4 items.", result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = "Customer 1",
                TotalAmount = 10,
                Branch = "Branch 1",
                Date = DateTime.Now,
                IsCancelled = false,
                Items = null
            };

            _saleRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
                                .Throws(new Exception());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Exception of type 'System.Exception' was thrown.", result.Errors);
        }
    }

}
