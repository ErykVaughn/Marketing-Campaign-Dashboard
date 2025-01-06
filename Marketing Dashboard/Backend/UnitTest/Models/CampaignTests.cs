using Xunit;

public class CampaignTests
{
    [Fact]
    public void Campaign_Should_SetAndRetrievePropertiesCorrectly()
    {
        // Arrange
        var campaign = new Campaign
        {
            CampaignId = 1,
            Name = "Test Campaign",
            TotalLeads = 100,
            FundedLeads = 25,
            TotalResponses = 50,
        };

        // Calculate expected percentages
        double expectedPercentLeadsWithResponse = (double)campaign.TotalResponses / campaign.TotalLeads * 100;
        double expectedPercentLeadsWithFunded = (double)campaign.FundedLeads / campaign.TotalLeads * 100;
        double expectedPercentResponsesWithFunded = (double)campaign.FundedLeads / campaign.TotalResponses * 100;

        // Set calculated properties
        campaign.PercentLeadsWithResponse = expectedPercentLeadsWithResponse;
        campaign.PercentLeadsWithFunded = expectedPercentLeadsWithFunded;
        campaign.PercentResponsesWithFunded = expectedPercentResponsesWithFunded;

        // Assert
        Assert.Equal(1, campaign.CampaignId);
        Assert.Equal("Test Campaign", campaign.Name);
        Assert.Equal(100, campaign.TotalLeads);
        Assert.Equal(25, campaign.FundedLeads);
        Assert.Equal(50, campaign.TotalResponses);
        Assert.Equal(expectedPercentLeadsWithResponse, campaign.PercentLeadsWithResponse);
        Assert.Equal(expectedPercentLeadsWithFunded, campaign.PercentLeadsWithFunded);
        Assert.Equal(expectedPercentResponsesWithFunded, campaign.PercentResponsesWithFunded);
    }
}
