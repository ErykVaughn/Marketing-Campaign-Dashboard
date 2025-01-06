using Xunit;

public class AudienceTests
{
    [Fact]
    public void Audience_Should_SetAndRetrievePropertiesCorrectly()
    {
        // Arrange
        var audience = new Audience();
        int expectedCampaignId = 101;
        int expectedRecordId = 202;
        bool expectedHasPhone = true;
        bool expectedHasEmail = false;

        // Act
        audience.Campaign_ID = expectedCampaignId;
        audience.Record_ID = expectedRecordId;
        audience.Has_Phone = expectedHasPhone;
        audience.Has_Email = expectedHasEmail;

        // Assert
        Assert.Equal(expectedCampaignId, audience.Campaign_ID);
        Assert.Equal(expectedRecordId, audience.Record_ID);
        Assert.Equal(expectedHasPhone, audience.Has_Phone);
        Assert.Equal(expectedHasEmail, audience.Has_Email);
    }
}
