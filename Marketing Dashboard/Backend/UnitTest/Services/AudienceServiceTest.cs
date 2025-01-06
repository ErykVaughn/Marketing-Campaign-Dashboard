using BackendAPI.Models;
using BackendAPI.Services;
using Moq;
using MySql.Data.MySqlClient;
using Xunit;
using System.Collections.Generic;
using System;
using BackendAPI.Database;  // Ensure this is here

namespace BackendAPI.Tests
{
    public class AudienceServiceTests
    {
        private readonly Mock<IDatabaseWrapper> _mockDatabaseWrapper;
        private readonly IAudienceService _audienceService;

        public AudienceServiceTests()
        {
            _mockDatabaseWrapper = new Mock<IDatabaseWrapper>();
            _audienceService = new AudienceService(_mockDatabaseWrapper.Object);
        }

        [Fact]
        public void GetAllAudiences_Should_Return_List_Of_Audiences()
        {
            // Arrange
            var mockAudiences = new List<Audience>
            {
                new Audience { Campaign_ID = 1, Record_ID = 100, Has_Phone = true, Has_Email = true },
                new Audience { Campaign_ID = 1, Record_ID = 101, Has_Phone = false, Has_Email = true }
            };

            _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, Audience>>()))
                .Returns(mockAudiences);

            // Act
            var result = _audienceService.GetAllAudiences();

            // Assert
            Assert.NotNull(result);                // Ensure the result is not null
            Assert.Equal(2, result.Count);         // Ensure the correct number of audiences are returned
            Assert.Equal(mockAudiences[0].Campaign_ID, result[0].Campaign_ID);  // Ensure data matches
            Assert.Equal(mockAudiences[1].Record_ID, result[1].Record_ID);     // Ensure data matches
        }

        [Fact]
        public void GetAudiencesByCampaignId_Should_Return_Audiences_For_Specific_Campaign()
        {
            // Arrange
            int campaignId = 1;
            var mockAudiences = new List<Audience>
            {
                new Audience { Campaign_ID = campaignId, Record_ID = 100, Has_Phone = true, Has_Email = true }
            };

            var parameters = new[] { new MySqlParameter("@CampaignId", campaignId) };

            // Adjusting the mock setup to correctly match the parameters and return a list of audiences
            _mockDatabaseWrapper.Setup(db => db.GetEntitiesByQuery(
                It.IsAny<string>(),
                It.IsAny<Func<MySqlDataReader, Audience>>(),
                It.IsAny<MySqlParameter[]>()
            )).Returns(mockAudiences);

            // Act
            var result = _audienceService.GetAudiencesByCampaignId(campaignId);

            // Assert
            Assert.NotNull(result);                // Ensure the result is not null
            Assert.Single(result);                 // Ensure only 1 audience is returned
            Assert.Equal(campaignId, result[0].Campaign_ID); // Ensure the audience has the expected campaign ID
        }
    }
}
