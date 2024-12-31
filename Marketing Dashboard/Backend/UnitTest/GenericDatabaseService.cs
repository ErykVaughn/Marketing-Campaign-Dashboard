using Moq;
using Xunit;
using BackendAPI.Services;
using BackendAPI.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BackendAPI.Tests
{
    public class GenericDatabaseServiceTests
    {
        private readonly Mock<IDatabaseWrapper> _mockDatabaseWrapper;
        private readonly GenericDatabaseService _genericDatabaseService;

        public GenericDatabaseServiceTests()
        {
            _mockDatabaseWrapper = new Mock<IDatabaseWrapper>();
            _genericDatabaseService = new GenericDatabaseService(_mockDatabaseWrapper.Object);
        }

        [Fact]
        public void ExecuteQuery_ShouldReturnListOfEntities_WhenQueryIsExecuted()
        {
            // Arrange
            var query = "SELECT * FROM SomeTable";
            var expectedList = new List<string> { "Entity1", "Entity2" };

            _mockDatabaseWrapper
                .Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, string>>()))
                .Returns(expectedList);

            // Act
            var result = _genericDatabaseService.ExecuteQuery(query, reader => reader.GetString(0));

            // Assert
            Assert.Equal(expectedList, result);
            _mockDatabaseWrapper.Verify(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, string>>()), Times.Once);
        }

        [Fact]
        public void ExecuteScalar_ShouldReturnSingleValue_WhenQueryIsExecuted()
        {
            // Arrange
            var query = "SELECT COUNT(*) FROM SomeTable";
            var expectedValue = 10;

            _mockDatabaseWrapper
                .Setup(db => db.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()))
                .Returns(expectedValue);

            // Act
            var result = _genericDatabaseService.ExecuteScalar<int>(query);

            // Assert
            Assert.Equal(expectedValue, result);
            _mockDatabaseWrapper.Verify(db => db.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()), Times.Once);
        }

        [Fact]
        public void GetEntitiesByQuery_ShouldReturnListOfEntities_WhenQueryIsExecuted()
        {
            // Arrange
            var query = "SELECT * FROM SomeTable WHERE Id = @Id";
            var parameters = new MySqlParameter[] { new MySqlParameter("@Id", 1) };
            var expectedList = new List<string> { "Entity1" };

            _mockDatabaseWrapper
                .Setup(db => db.GetEntitiesByQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, string>>(), It.IsAny<MySqlParameter[]>()))
                .Returns(expectedList);

            // Act
            var result = _genericDatabaseService.GetEntitiesByQuery(query, reader => reader.GetString(0), parameters);

            // Assert
            Assert.Equal(expectedList, result);
            _mockDatabaseWrapper.Verify(db => db.GetEntitiesByQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, string>>(), It.IsAny<MySqlParameter[]>()), Times.Once);
        }
    }
}
