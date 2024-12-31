using Xunit;

namespace BackendAPI.Tests
{
    public class LeadStatisticsTests
    {
        [Fact]
        public void LeadStatistics_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var leadStatistics = new LeadStatistics
            {
                TotalLeads = 100,
                LeadsWithPhones = 80,
                LeadsWithEmails = 60,
                PhoneRatio = 0.8,
                EmailRatio = 0.6
            };

            // Act & Assert
            Assert.Equal(100, leadStatistics.TotalLeads);
            Assert.Equal(80, leadStatistics.LeadsWithPhones);
            Assert.Equal(60, leadStatistics.LeadsWithEmails);
            Assert.Equal(0.8, leadStatistics.PhoneRatio);
            Assert.Equal(0.6, leadStatistics.EmailRatio);
        }

        [Fact]
        public void LeadStatistics_CanSetAndGetValues()
        {
            // Arrange
            var leadStatistics = new LeadStatistics();

            // Act
            leadStatistics.TotalLeads = 150;
            leadStatistics.LeadsWithPhones = 120;
            leadStatistics.LeadsWithEmails = 100;
            leadStatistics.PhoneRatio = 0.8;
            leadStatistics.EmailRatio = 0.67;

            // Assert
            Assert.Equal(150, leadStatistics.TotalLeads);
            Assert.Equal(120, leadStatistics.LeadsWithPhones);
            Assert.Equal(100, leadStatistics.LeadsWithEmails);
            Assert.Equal(0.8, leadStatistics.PhoneRatio);
            Assert.Equal(0.67, leadStatistics.EmailRatio);
        }
    }
}
