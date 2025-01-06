using Xunit;
using System;
using BackendAPI.Models;  // Add this line to import the Response class

namespace BackendAPI.Tests
{
    public class ResponseTests
    {
        [Fact]
        public void Response_Should_SetAndRetrievePropertiesCorrectly()
        {
            // Arrange
            var response = new Response();
            int expectedCampaignId = 1;
            int expectedRecordId = 100;
            bool expectedLeadFlag = true;
            DateTime expectedLeadTimestamp = DateTime.Now;
            bool expectedFundedFlag = false;
            DateTime expectedFundedTimestamp = DateTime.Now.AddDays(-1);

            // Act
            response.Campaign_ID = expectedCampaignId;
            response.Record_ID = expectedRecordId;
            response.Lead_Flag = expectedLeadFlag;
            response.Lead_Timestamp = expectedLeadTimestamp;
            response.Funded_Flag = expectedFundedFlag;
            response.Funded_Timestamp = expectedFundedTimestamp;

            // Assert
            Assert.Equal(expectedCampaignId, response.Campaign_ID);
            Assert.Equal(expectedRecordId, response.Record_ID);
            Assert.Equal(expectedLeadFlag, response.Lead_Flag);
            Assert.Equal(expectedLeadTimestamp, response.Lead_Timestamp);
            Assert.Equal(expectedFundedFlag, response.Funded_Flag);
            Assert.Equal(expectedFundedTimestamp, response.Funded_Timestamp);
        }
    }
}
