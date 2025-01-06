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
    public class CampaignControllerTests
    {
        private readonly Mock<ICampaignService> _mockCampaignService;
        private readonly CampaignController _campaignController;

        public CampaignControllerTests()
        {
            _mockCampaignService = new Mock<ICampaignService>();
            _campaignController = new CampaignController(_mockCampaignService.Object);
        }

        [Fact]
        public void GetCampaigns_Should_Return_OkResult_With_Campaigns()
        {
            // Arrange
            var mockCampaigns = new List<Campaign>
            {
                new Campaign { 
                    CampaignId = 1, 
                    Name = "Campaign 1", 
                    TotalLeads = 100, 
                    FundedLeads = 50, 
                    TotalResponses = 75, 
                    PercentLeadsWithResponse = 75.0, 
                    PercentLeadsWithFunded = 50.0, 
                    PercentResponsesWithFunded = 66.7
                },
                new Campaign { 
                    CampaignId = 2, 
                    Name = "Campaign 2", 
                    TotalLeads = 200, 
                    FundedLeads = 120, 
                    TotalResponses = 180, 
                    PercentLeadsWithResponse = 90.0, 
                    PercentLeadsWithFunded = 60.0, 
                    PercentResponsesWithFunded = 66.7
                }
            };

            _mockCampaignService.Setup(service => service.GetAllCampaigns())
                .Returns(mockCampaigns);

            // Act
            var result = _campaignController.GetCampaigns();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Campaign>>>(result); // Check ActionResult type
            var okResult = actionResult.Result as OkObjectResult; // Unwrap OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<List<Campaign>>(okResult.Value);
            Assert.Equal(mockCampaigns.Count, returnValue.Count); // Verify count
        }

        [Fact]
        public void GetAverageFundedRate_Should_Return_OkResult_With_Statistics()
        {
            // Arrange
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            var mockStatistics = new CombinedLeadStatistics
            {
                TotalLeads = 300,
                LeadsWithPhones = 200,
                LeadsWithEmails = 150,
                LeadsWithBoth = 100,
                LeadsWithNeither = 50,
                TotalFundedLeads = 200,
                TotalUnfundedLeads = 100,
                OverallFundedPercentage = 66.7,
                LeadsWithinDateRange = 100,
                FundedLeadsWithinDateRange = 75,
                AverageFundedRate = 0.75,
                StartDate = startDate,
                EndDate = endDate
            };

            _mockCampaignService.Setup(service => service.GetEnhancedLeadStatistics(startDate, endDate))
                .Returns(mockStatistics);

            // Act
            var result = _campaignController.GetAverageFundedRate(startDate, endDate);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CombinedLeadStatistics>>(result); // Check ActionResult type
            var okResult = actionResult.Result as OkObjectResult; // Unwrap OkObjectResult
            Assert.NotNull(okResult);
            var returnValue = Assert.IsAssignableFrom<CombinedLeadStatistics>(okResult.Value);
            Assert.Equal(mockStatistics.AverageFundedRate, returnValue.AverageFundedRate); // Verify funded rate
            Assert.Equal(mockStatistics.LeadsWithPhones, returnValue.LeadsWithPhones); // Verify leads with phones
            Assert.Equal(mockStatistics.LeadsWithEmails, returnValue.LeadsWithEmails); // Verify leads with emails
            Assert.Equal(mockStatistics.LeadsWithBoth, returnValue.LeadsWithBoth); // Verify leads with both
            Assert.Equal(mockStatistics.LeadsWithNeither, returnValue.LeadsWithNeither); // Verify leads with neither
            Assert.Equal(mockStatistics.TotalFundedLeads, returnValue.TotalFundedLeads); // Verify total funded leads
            Assert.Equal(mockStatistics.TotalUnfundedLeads, returnValue.TotalUnfundedLeads); // Verify total unfunded leads
            Assert.Equal(mockStatistics.OverallFundedPercentage, returnValue.OverallFundedPercentage); // Verify overall funded percentage
        }


    }
}
