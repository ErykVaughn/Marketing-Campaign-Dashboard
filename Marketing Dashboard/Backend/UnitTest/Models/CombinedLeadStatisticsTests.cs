using Xunit;
using System;

public class CombinedLeadStatisticsTests
{
    [Fact]
    public void CombinedLeadStatistics_Should_SetAndRetrievePropertiesCorrectly()
    {
        // Arrange
        var statistics = new CombinedLeadStatistics
        {
            TotalLeads = 1000,
            LeadsWithPhones = 800,
            LeadsWithEmails = 700,
            LeadsWithBoth = 600,
            LeadsWithNeither = 200,
            TotalFundedLeads = 300,
            TotalUnfundedLeads = 700,
            OverallFundedPercentage = 30.0,
            LeadsWithinDateRange = 500,
            FundedLeadsWithinDateRange = 150,
            AverageFundedRate = 15.0,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 12, 31)
        };

        // Act & Assert
        Assert.Equal(1000, statistics.TotalLeads);
        Assert.Equal(800, statistics.LeadsWithPhones);
        Assert.Equal(700, statistics.LeadsWithEmails);
        Assert.Equal(600, statistics.LeadsWithBoth);
        Assert.Equal(200, statistics.LeadsWithNeither);
        Assert.Equal(300, statistics.TotalFundedLeads);
        Assert.Equal(700, statistics.TotalUnfundedLeads);
        Assert.Equal(30.0, statistics.OverallFundedPercentage);
        Assert.Equal(500, statistics.LeadsWithinDateRange);
        Assert.Equal(150, statistics.FundedLeadsWithinDateRange);
        Assert.Equal(15.0, statistics.AverageFundedRate);
        Assert.Equal(new DateTime(2023, 1, 1), statistics.StartDate);
        Assert.Equal(new DateTime(2023, 12, 31), statistics.EndDate);
    }
}
