using Moq;
using Xunit;
using BackendAPI.Services;
using BackendAPI.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BackendAPI.Tests
{
    public class CampaignServiceTests
    {
        private readonly Mock<IGenericDatabaseService> _mockDbService;
        private readonly CampaignService _campaignService;

        public CampaignServiceTests()
        {
            _mockDbService = new Mock<IGenericDatabaseService>();
            _campaignService = new CampaignService(_mockDbService.Object);
        }

        //     [Fact]
        //     public void GetAllCampaigns_ShouldReturnListOfCampaigns_WhenExecuted()
        //     {
        //         // Arrange
        //         var campaigns = new List<Campaign>
        // {
        //     new Campaign
        //     {
        //         CampaignId = 1,
        //         Name = "Campaign 1",
        //         TotalLeads = 500,
        //         FundedLeads = 250,
        //         TotalResponses = 400,
        //         PercentLeadsWithResponse = 0.80, // 80% of the leads have a response
        //         PercentLeadsWithFunded = 0.50,   // 50% of the leads are funded
        //         PercentResponsesWithFunded = 0.625 // 62.5% of the responses are funded
        //     }
        // };

        //         // Mock ExecuteQuery to return a List<Campaign> instead of List<object>
        //         _mockDbService.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, Campaign>>()))
        //             .Returns(campaigns);

        //         // Act
        //         var result = _campaignService.GetAllCampaigns();

        //         // Assert
        //         Assert.Equal(2, result.Count);
        //         Assert.Equal("Campaign 1", result[0].Name);
        //         Assert.Equal("Campaign 2", result[1].Name);
        //     }

        [Fact]
        public void GetLeadsForCampaign_ShouldReturnLeadCount_WhenExecuted()
        {
            // Arrange
            var campaignId = 1;
            var expectedLeadCount = 100;

            _mockDbService.Setup(db => db.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()))
                .Returns(expectedLeadCount);

            // Act
            var result = _campaignService.GetLeadsForCampaign(campaignId);

            // Assert
            Assert.Equal(expectedLeadCount, result);
        }

        [Fact]
        public void GetLeadStatistics_ShouldReturnCorrectStatistics_WhenExecuted()
        {
            // Arrange
            var totalLeads = 200;
            var leadsWithPhones = 150;
            var leadsWithEmails = 180;

            _mockDbService.Setup(db => db.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()))
                .Returns<string, MySqlParameter[]>((query, parameters) =>
                {
                    if (query.Contains("Has_Phone"))
                        return leadsWithPhones;
                    if (query.Contains("Has_Email"))
                        return leadsWithEmails;
                    return totalLeads;  // Default return value
                });

            // Act
            var result = _campaignService.GetLeadStatistics();

            // Assert
            Assert.Equal(totalLeads, result.TotalLeads);
            Assert.Equal(leadsWithPhones, result.LeadsWithPhones);
            Assert.Equal(leadsWithEmails, result.LeadsWithEmails);
            Assert.Equal((double)leadsWithPhones / totalLeads, result.PhoneRatio);
            Assert.Equal((double)leadsWithEmails / totalLeads, result.EmailRatio);
        }

        [Fact]
        public void GetAverageFundedRate_ShouldReturnFundedRate_WhenExecuted()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);
            var expectedFundedRate = 0.5;

            _mockDbService.Setup(db => db.ExecuteScalar<double>(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()))
                .Returns(expectedFundedRate);

            // Act
            var result = _campaignService.GetAverageFundedRate(startDate, endDate);

            // Assert
            Assert.Equal(expectedFundedRate, result);
        }
    }
}
