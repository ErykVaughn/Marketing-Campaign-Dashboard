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
    public class ResponseServiceTests
    {
        private readonly Mock<IDatabaseWrapper> _mockDatabaseWrapper;
        private readonly ResponseService _responseService;

        public ResponseServiceTests()
        {
            _mockDatabaseWrapper = new Mock<IDatabaseWrapper>();
            _responseService = new ResponseService(_mockDatabaseWrapper.Object);
        }

        [Fact]
        public void GetAllResponses_Should_Return_List_Of_Responses()
        {
            // Arrange
            var mockResponses = new List<Response>
            {
                new Response { Campaign_ID = 1, Record_ID = 100, Lead_Flag = true, Lead_Timestamp = DateTime.Now, Funded_Flag = false, Funded_Timestamp = null },
                new Response { Campaign_ID = 1, Record_ID = 101, Lead_Flag = false, Lead_Timestamp = DateTime.Now.AddHours(-1), Funded_Flag = true, Funded_Timestamp = DateTime.Now.AddMinutes(-30) }
            };

            _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, Response>>() ))
                .Returns(mockResponses);

            // Act
            var result = _responseService.GetAllResponses();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(mockResponses[0].Campaign_ID, result[0].Campaign_ID);
            Assert.Equal(mockResponses[1].Record_ID, result[1].Record_ID);
        }

        [Fact]
        public void GetResponsesByCampaignId_Should_Return_Responses_For_Specific_Campaign()
        {
            // Arrange
            int campaignId = 1;
            var mockResponses = new List<Response>
            {
                new Response { Campaign_ID = campaignId, Record_ID = 100, Lead_Flag = true, Lead_Timestamp = DateTime.Now, Funded_Flag = false, Funded_Timestamp = null }
            };

            // Adjusting the mock setup to correctly match the parameters and return a list of responses
            _mockDatabaseWrapper.Setup(db => db.GetEntitiesByQuery(
                It.IsAny<string>(), 
                It.IsAny<Func<MySqlDataReader, Response>>(), 
                It.IsAny<MySqlParameter[]>()
            )).Returns(mockResponses);

            // Act
            var result = _responseService.GetResponsesByCampaignId(campaignId);

            // Assert
            Assert.NotNull(result);               // Ensure the result is not null
            Assert.Single(result);                // Ensure only 1 response is returned
            Assert.Equal(campaignId, result[0].Campaign_ID); // Ensure the response has the expected campaign ID
        }
    }
}
