using Xunit;

namespace BackendAPI.Tests
{
    public class CampaignTests
    {
        [Fact]
        public void Campaign_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var campaign = new Campaign
            {
                CampaignId = 1,
                Name = "Test Campaign", // Ensure required property is initialized
                TotalLeads = 100,
                FundedLeads = 75,
                TotalResponses = 90,
                PercentLeadsWithResponse = 90.0,
                PercentLeadsWithFunded = 75.0,
                PercentResponsesWithFunded = 83.3
            };

            // Act & Assert
            Assert.Equal(1, campaign.CampaignId);
            Assert.Equal("Test Campaign", campaign.Name);
            Assert.Equal(100, campaign.TotalLeads);
            Assert.Equal(75, campaign.FundedLeads);
            Assert.Equal(90, campaign.TotalResponses);
            Assert.Equal(90.0, campaign.PercentLeadsWithResponse);
            Assert.Equal(75.0, campaign.PercentLeadsWithFunded);
            Assert.Equal(83.3, campaign.PercentResponsesWithFunded);
        }

        [Fact]
        public void Campaign_CanSetAndGetValues()
        {
            // Arrange
            var campaign = new Campaign
            {
                Name = string.Empty // Default value for required property
            };

            // Act
            campaign.CampaignId = 2;
            campaign.Name = "Campaign 2"; // Ensure Name is set
            campaign.TotalLeads = 200;
            campaign.FundedLeads = 150;
            campaign.TotalResponses = 180;
            campaign.PercentLeadsWithResponse = 90.0;
            campaign.PercentLeadsWithFunded = 75.0;
            campaign.PercentResponsesWithFunded = 83.3;

            // Assert
            Assert.Equal(2, campaign.CampaignId);
            Assert.Equal("Campaign 2", campaign.Name);
            Assert.Equal(200, campaign.TotalLeads);
            Assert.Equal(150, campaign.FundedLeads);
            Assert.Equal(180, campaign.TotalResponses);
            Assert.Equal(90.0, campaign.PercentLeadsWithResponse);
            Assert.Equal(75.0, campaign.PercentLeadsWithFunded);
            Assert.Equal(83.3, campaign.PercentResponsesWithFunded);
        }
    }
}
