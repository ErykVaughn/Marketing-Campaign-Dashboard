using Microsoft.AspNetCore.Mvc;
using Moq;
using BackendAPI.Controllers;
using BackendAPI.Services;
using BackendAPI.Models;
using System.Collections.Generic;
using Xunit;

namespace BackendAPI.Tests.Controllers
{
    public class AudienceControllerTests
    {
        private readonly Mock<IAudienceService> _mockAudienceService;
        private readonly AudienceController _audienceController;

        public AudienceControllerTests()
        {
            _mockAudienceService = new Mock<IAudienceService>();
            _audienceController = new AudienceController(_mockAudienceService.Object);
        }

        [Fact]
        public void GetAllAudiences_Should_Return_OkResult_With_Audiences()
        {
            // Arrange
            var mockAudiences = new List<Audience>
            {
                new Audience { Record_ID = 1, Campaign_ID = 1, Has_Phone = true, Has_Email = true },
                new Audience { Record_ID = 2, Campaign_ID = 2, Has_Phone = false, Has_Email = true }
            };

            _mockAudienceService.Setup(service => service.GetAllAudiences())
                .Returns(mockAudiences);

            // Act
            var result = _audienceController.GetAllAudiences();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Audience>>>(result); // Change here
            var okResult = actionResult.Result as OkObjectResult; // Unwrap the OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<List<Audience>>(okResult.Value);
            Assert.Equal(mockAudiences.Count, returnValue.Count);
        }

        [Fact]
        public void GetAudiencesByCampaignId_Should_Return_OkResult_With_Audiences()
        {
            // Arrange
            int campaignId = 1;
            var mockAudiences = new List<Audience>
            {
                new Audience { Record_ID = 1, Campaign_ID = campaignId, Has_Phone = true, Has_Email = true },
                new Audience { Record_ID = 2, Campaign_ID = campaignId, Has_Phone = false, Has_Email = true }
            };

            _mockAudienceService.Setup(service => service.GetAudiencesByCampaignId(campaignId))
                .Returns(mockAudiences);

            // Act
            var result = _audienceController.GetAudiencesByCampaignId(campaignId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Audience>>>(result); // Change here
            var okResult = actionResult.Result as OkObjectResult; // Unwrap the OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<List<Audience>>(okResult.Value);
            Assert.Equal(mockAudiences.Count, returnValue.Count);
            Assert.All(returnValue, audience => Assert.Equal(campaignId, audience.Campaign_ID));
        }
    }
}
