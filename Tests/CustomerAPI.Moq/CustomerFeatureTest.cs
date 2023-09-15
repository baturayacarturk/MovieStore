using Application.Features.Customers.Commands;
using Application.Features.Customers.Handlers;
using Application.Services.Repositories;
using Castle.Core.Configuration;
using CommonUseForTests;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;

namespace CustomerAPI.Moq
{
    public class CustomerFeatureTest
    {
        private readonly CreateCustomerCommandHandler _createCustomerCommandHandler;
        private readonly UpdateCustomerCommandHandler _updateCustomerCommandHandler;
        private readonly DeleteCustomerCommandHandler _deletedCustomerCommandHandler;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new Mock<ICustomerRepository>();
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;

        public CustomerFeatureTest()
        {
            _createCustomerCommandHandler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
            _updateCustomerCommandHandler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object);
            _deletedCustomerCommandHandler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object);
            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("Key", "fjdsfjd51asd5qw61c21gfsd3gsdhgewdfsetggfd")
                })
                .Build();
            EncryptionService.Initialize(configuration);

        }
        [Fact]
        public async Task CreateCommandHandler_ShouldReturnCreatedCustomerViewModel()
        {
            var request = new CreateCustomerCommand
            {
                FirstName = "John",
                LastName = "Doe",
            };

            _customerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Customer { Id = 1, FirstName = "John", LastName = "Doe" });

            var encrypted = EncryptionService.Encrypt(1);

            // Act
            var result = await _createCustomerCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(encrypted, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);

        }
        [Fact]
        public async Task UpdateCommandHandler_ShouldReturnUpdatedCustomerViewModel()
        {
            var request = new UpdateCustomerCommand
            {
                Id = "SGVsbG8gV29ybGQh",
                FirstName = "Baturay",
                LastName = "Acarturk"
            };
            var decryptId = EncryptionService.Decrypt(request.Id);
            _customerRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync(new Customer { Id = decryptId });

            _customerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName
                });

            var result = await _updateCustomerCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal("Customer information has updated", result.Message);
        }
        [Fact]
        public async Task DeleteCommandHandler_ShouldReturnDeletedCustomerViewModel()
        {
            var request = new DeleteCustomerCommand
            {
                Id = "SGVsbG8gV29ybGQh"
            };
            var decryptedId = EncryptionService.Decrypt(request.Id);
            var customerToUpdate = new Customer
            {
                Id = decryptedId,
                FirstName = "Baturay",
                LastName = "Acarturk",
                IsActive = true
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<Customer, bool>>>()))
                                .ReturnsAsync(customerToUpdate);
            _customerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Customer>()))
            .ReturnsAsync((Customer updatedCustomer) =>
          {
              updatedCustomer.IsActive = false;
              return updatedCustomer;
          });

            var result = await _deletedCustomerCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Baturay", result.FirstName);
            Assert.Equal("Acarturk", result.LastName);
            Assert.Equal(Messages.DeletedMessage, result.Message);
            Assert.False(customerToUpdate.IsActive);
        }
    }
}
