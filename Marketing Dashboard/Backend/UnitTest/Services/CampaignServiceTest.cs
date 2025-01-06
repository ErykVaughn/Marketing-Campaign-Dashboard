// using BackendAPI.Models;
// using BackendAPI.Services;
// using Moq;
// using MySql.Data.MySqlClient;
// using Xunit;
// using System.Collections.Generic;
// using System;
// using BackendAPI.Database;

// namespace BackendAPI.Tests
// {
//     public class CampaignServiceTests
//     {
//         private readonly Mock<IDatabaseWrapper> _mockDatabaseWrapper;
//         private readonly ICampaignService _campaignService;

//         public CampaignServiceTests()
//         {
//             _mockDatabaseWrapper = new Mock<IDatabaseWrapper>();
//             _campaignService = new CampaignService(_mockDatabaseWrapper.Object);
//         }

//         [Fact]
//         public void GetAllCampaigns_Should_Return_List_Of_Campaigns()
//         {
//             // Arrange
//             var mockCampaigns = new List<Campaign>
//             {
//                 new Campaign { CampaignId = 1, Name = "Campaign 1" },
//                 new Campaign { CampaignId = 2, Name = "Campaign 2" }
//             };

//             var mockTotalLeads = new List<dynamic>
//             {
//                 new { CampaignId = 1, TotalLeads = 100 },
//                 new { CampaignId = 2, TotalLeads = 150 }
//             };

//             var mockTotalResponses = new List<dynamic>
//             {
//                 new { CampaignId = 1, TotalResponses = 80 },
//                 new { CampaignId = 2, TotalResponses = 120 }
//             };

//             var mockFundedLeads = new List<dynamic>
//             {
//                 new { CampaignId = 1, FundedLeads = 60 },
//                 new { CampaignId = 2, FundedLeads = 90 }
//             };

//             // Setting up the mock database wrapper to return the mock data
//             _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, Campaign>>()))
//                 .Returns(mockCampaigns);

//             _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, dynamic>>()))
//                 .Returns(mockTotalLeads);

//             _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, dynamic>>()))
//                 .Returns(mockTotalResponses);

//             _mockDatabaseWrapper.Setup(db => db.ExecuteQuery(It.IsAny<string>(), It.IsAny<Func<MySqlDataReader, dynamic>>()))
//                 .Returns(mockFundedLeads);

//             // Act
//             var result = _campaignService.GetAllCampaigns();

//             // Assert
//             Assert.NotNull(result);                // Ensure the result is not null
//             Assert.Equal(2, result.Count);         // Ensure the correct number of campaigns are returned
//             Assert.Equal(mockCampaigns[0].CampaignId, result[0].CampaignId);  // Ensure data matches
//             Assert.Equal(mockCampaigns[1].Name, result[1].Name);  // Ensure data matches
//             Assert.Equal(100, result[0].TotalLeads);  // Ensure TotalLeads matches
//             Assert.Equal(80, result[0].TotalResponses);  // Ensure TotalResponses matches
//             Assert.Equal(60, result[0].FundedLeads);  // Ensure FundedLeads matches
//         }

//         [Fact]
//         public void GetEnhancedLeadStatistics_Should_Return_Valid_Lead_Statistics()
//         {
//             // Arrange
//             DateTime startDate = DateTime.Now.AddDays(-30);
//             DateTime endDate = DateTime.Now;

//             var mockTotalLeads = 200;
//             var mockLeadsWithPhones = 150;
//             var mockLeadsWithEmails = 180;
//             var mockLeadsWithBoth = 120;
//             var mockFundedLeads = 80;
//             var mockLeadsWithinDateRange = 100;
//             var mockFundedLeadsWithinDateRange = 70;
//             var mockAverageFundedRate = 0.75;

//             var queueStuff = new Queue<int>();
//             queueStuff.Enqueue(mockTotalLeads);
//             queueStuff.Enqueue(mockLeadsWithPhones);
//             queueStuff.Enqueue(mockLeadsWithEmails);
//             queueStuff.Enqueue(mockLeadsWithBoth);
//             queueStuff.Enqueue(mockFundedLeads);
//             queueStuff.Enqueue(mockLeadsWithinDateRange);
//             queueStuff.Enqueue(mockFundedLeadsWithinDateRange);

//             // Setting up the mock database wrapper to return the mock data
//             _mockDatabaseWrapper.Setup(db => db.ExecuteScalar<int>(It.IsAny<string>()))
//                 .Returns(queueStuff.Dequeue);

//             _mockDatabaseWrapper.Setup(db => db.ExecuteScalar<double>(It.IsAny<string>()))
//                 .Returns(mockAverageFundedRate);

//             // Act
//             var result = _campaignService.GetEnhancedLeadStatistics(startDate, endDate);

//             // Assert
//             Assert.NotNull(result);   // Ensure the result is not null
//             Assert.Equal(mockTotalLeads, result.TotalLeads);  // Ensure TotalLeads matches
//             Assert.Equal(mockLeadsWithPhones, result.LeadsWithPhones);  // Ensure LeadsWithPhones matches
//             Assert.Equal(mockLeadsWithEmails, result.LeadsWithEmails);  // Ensure LeadsWithEmails matches
//             Assert.Equal(mockLeadsWithBoth, result.LeadsWithBoth);  // Ensure LeadsWithBoth matches
//             Assert.Equal(mockFundedLeads, result.TotalFundedLeads);  // Ensure FundedLeads matches
//             Assert.Equal(mockLeadsWithinDateRange, result.LeadsWithinDateRange);  // Ensure LeadsWithinDateRange matches
//             Assert.Equal(mockFundedLeadsWithinDateRange, result.FundedLeadsWithinDateRange);  // Ensure FundedLeadsWithinDateRange matches
//             Assert.Equal(mockAverageFundedRate, result.AverageFundedRate);  // Ensure AverageFundedRate matches
//         }
//      }
// }
