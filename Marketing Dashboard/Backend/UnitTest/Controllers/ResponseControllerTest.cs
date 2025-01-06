using Microsoft.AspNetCore.Mvc;
using Moq;
using BackendAPI.Controllers;
using BackendAPI.Services;
using BackendAPI.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BackendAPI.Tests.Controllers
{
    public class ResponseControllerTests
    {
        private readonly Mock<IResponseService> _mockResponseService;
        private readonly ResponseController _responseController;

        public ResponseControllerTests()
        {
            _mockResponseService = new Mock<IResponseService>();
            _responseController = new ResponseController(_mockResponseService.Object);
        }

        [Fact]
        public void GetAllResponses_Should_Return_OkResult_With_Responses()
        {
            // Arrange
            var mockResponses = new List<Response>
            {
                new Response 
                { 
                    Campaign_ID = 1, 
                    Record_ID = 1, 
                    Lead_Flag = true, 
                    Lead_Timestamp = DateTime.Now, 
                    Funded_Flag = false, 
                    Funded_Timestamp = null 
                },
                new Response 
                { 
                    Campaign_ID = 2, 
                    Record_ID = 2, 
                    Lead_Flag = false, 
                    Lead_Timestamp = null, 
                    Funded_Flag = true, 
                    Funded_Timestamp = DateTime.Now 
                }
            };

            _mockResponseService.Setup(service => service.GetAllResponses())
                .Returns(mockResponses);

            // Act
            var result = _responseController.GetAllResponses();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Response>>>(result); // Check ActionResult type
            var okResult = actionResult.Result as OkObjectResult; // Unwrap OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<List<Response>>(okResult.Value);
            Assert.Equal(mockResponses.Count, returnValue.Count); // Verify count
        }

        [Fact]
        public void GetResponsesByCampaignId_Should_Return_OkResult_With_Responses()
        {
            // Arrange
            int campaignId = 1;
            var mockResponses = new List<Response>
            {
                new Response 
                { 
                    Campaign_ID = campaignId, 
                    Record_ID = 1, 
                    Lead_Flag = true, 
                    Lead_Timestamp = DateTime.Now, 
                    Funded_Flag = false, 
                    Funded_Timestamp = null 
                },
                new Response 
                { 
                    Campaign_ID = campaignId, 
                    Record_ID = 2, 
                    Lead_Flag = false, 
                    Lead_Timestamp = null, 
                    Funded_Flag = true, 
                    Funded_Timestamp = DateTime.Now 
                }
            };

            _mockResponseService.Setup(service => service.GetResponsesByCampaignId(campaignId))
                .Returns(mockResponses);

            // Act
            var result = _responseController.GetResponsesByCampaignId(campaignId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Response>>>(result); // Check ActionResult type
            var okResult = actionResult.Result as OkObjectResult; // Unwrap OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<List<Response>>(okResult.Value);
            Assert.Equal(mockResponses.Count, returnValue.Count); // Verify count
            Assert.All(returnValue, response => Assert.Equal(campaignId, response.Campaign_ID)); // Verify campaignId
        }
    }
}
