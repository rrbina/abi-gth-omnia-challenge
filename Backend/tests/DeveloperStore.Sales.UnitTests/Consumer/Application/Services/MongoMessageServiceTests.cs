using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Consumer.Domain.Entities;
using DeveloperStore.Sales.Consumer.Application.Contracts;

namespace DeveloperStore.Sales.UnitTests.Consumer.Application.Services
{
    public class MongoMessageServiceTests
    {
        [Fact]
        public async Task PersistMessageAsync_Should_Call_Repository_AddAsync_With_Correct_Data()
        {
            var repositoryMock = new Mock<IMongoMessageRepository>();
            var service = new MongoMessageService(repositoryMock.Object);

            var id = Guid.NewGuid();
            var message = "{ \"test\": true }";
            var timestamp = DateTime.UtcNow;

            await service.PersistMessageAsync(message, id, timestamp);

            repositoryMock.Verify(r =>
                r.AddAsync(It.Is<MongoMessage>(m =>
                    m.Id == id &&
                    m.Message == message &&
                    m.Timestamp == timestamp
                )),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Call_Repository_And_Return_Result()
        {
            var repositoryMock = new Mock<IMongoMessageRepository>();
            var expected = new MongoMessage { Id = Guid.NewGuid(), Message = "msg", Timestamp = DateTime.UtcNow };

            repositoryMock.Setup(r => r.GetByIdAsync(expected.Id)).ReturnsAsync(expected);

            var service = new MongoMessageService(repositoryMock.Object);
            var result = await service.GetByIdAsync(expected.Id);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Messages()
        {
            var repositoryMock = new Mock<IMongoMessageRepository>();
            var expected = new List<MongoMessage> { new MongoMessage(), new MongoMessage() };

            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            var service = new MongoMessageService(repositoryMock.Object);
            var result = await service.GetAllAsync();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_Repository_DeleteAsync()
        {
            var repositoryMock = new Mock<IMongoMessageRepository>();
            var id = Guid.NewGuid();

            var service = new MongoMessageService(repositoryMock.Object);
            await service.DeleteAsync(id);

            repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}