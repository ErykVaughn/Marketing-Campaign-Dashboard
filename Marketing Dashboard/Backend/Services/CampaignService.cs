using MySql.Data.MySqlClient;
using BackendAPI.Database;


namespace BackendAPI.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IDatabaseWrapper _dbService;

        public CampaignService(IDatabaseWrapper dbService)
        {
            _dbService = dbService;
        }


        // Get Campaigns with basic metrics (leads and funded leads)
        public List<Campaign> GetAllCampaigns()
        {
            // 1. Get All Campaigns
            string campaignsQuery = "SELECT Campaign_ID, Name FROM Campaigns;";
            var campaigns = _dbService.ExecuteQuery(campaignsQuery, reader => new Campaign
            {
                CampaignId = reader.GetInt32("Campaign_ID"),
                Name = reader.GetString("Name")
            });

            // 2. Get Total Leads
            string totalLeadsQuery = @"
                SELECT Campaign_ID, COUNT(DISTINCT Record_ID) AS TotalLeads
                FROM Audiences
                GROUP BY Campaign_ID;";
            var totalLeads = _dbService.ExecuteQuery(totalLeadsQuery, reader => new
            {
                CampaignId = reader.GetInt32("Campaign_ID"),
                TotalLeads = reader.GetInt32("TotalLeads")
            }).ToDictionary(x => x.CampaignId, x => x.TotalLeads);

            // 3. Get Total Responses
            string totalResponsesQuery = @"
                SELECT Campaign_ID, COUNT(DISTINCT Record_ID) AS TotalResponses
                FROM Responses
                GROUP BY Campaign_ID;";
            var totalResponses = _dbService.ExecuteQuery(totalResponsesQuery, reader => new
            {
                CampaignId = reader.GetInt32("Campaign_ID"),
                TotalResponses = reader.GetInt32("TotalResponses")
            }).ToDictionary(x => x.CampaignId, x => x.TotalResponses);

            // 4. Get Funded Leads
            string fundedLeadsQuery = @"
                SELECT Campaign_ID, COUNT(*) AS FundedLeads
                FROM Responses
                WHERE Funded_Flag = 1
                GROUP BY Campaign_ID;";
            var fundedLeads = _dbService.ExecuteQuery(fundedLeadsQuery, reader => new
            {
                CampaignId = reader.GetInt32("Campaign_ID"),
                FundedLeads = reader.GetInt32("FundedLeads")
            }).ToDictionary(x => x.CampaignId, x => x.FundedLeads);

            // 5. Combine Results
            foreach (var campaign in campaigns)
            {
                var campaignId = campaign.CampaignId;

                campaign.TotalLeads = totalLeads.ContainsKey(campaignId) ? totalLeads[campaignId] : 0;
                campaign.TotalResponses = totalResponses.ContainsKey(campaignId) ? totalResponses[campaignId] : 0;
                campaign.FundedLeads = fundedLeads.ContainsKey(campaignId) ? fundedLeads[campaignId] : 0;

                // Calculate Percentages
                campaign.PercentLeadsWithResponse = campaign.TotalLeads == 0 ? 0 :
                    (double)campaign.TotalResponses / campaign.TotalLeads;

                campaign.PercentLeadsWithFunded = campaign.TotalLeads == 0 ? 0 :
                    (double)campaign.FundedLeads / campaign.TotalLeads;

                campaign.PercentResponsesWithFunded = campaign.TotalResponses == 0 ? 0 :
                    (double)campaign.FundedLeads / campaign.TotalResponses;
            }

            return campaigns;
        }

public CombinedLeadStatistics GetEnhancedLeadStatistics(DateTime startDate, DateTime endDate)
{
    // Overall data queries
    string totalLeadsQuery = "SELECT COUNT(*) AS TotalLeads FROM Audiences;";
    var totalLeads = _dbService.ExecuteScalar<int>(totalLeadsQuery);

    string leadsWithPhonesQuery = "SELECT COUNT(*) AS LeadsWithPhones FROM Audiences WHERE Has_Phone = 1;";
    var leadsWithPhones = _dbService.ExecuteScalar<int>(leadsWithPhonesQuery);

    string leadsWithEmailsQuery = "SELECT COUNT(*) AS LeadsWithEmails FROM Audiences WHERE Has_Email = 1;";
    var leadsWithEmails = _dbService.ExecuteScalar<int>(leadsWithEmailsQuery);

    string leadsWithBothQuery = "SELECT COUNT(*) AS LeadsWithBoth FROM Audiences WHERE Has_Phone = 1 AND Has_Email = 1;";
    var leadsWithBoth = _dbService.ExecuteScalar<int>(leadsWithBothQuery);

    string fundedLeadsQuery = "SELECT COUNT(*) AS FundedLeads FROM Responses WHERE Funded_Flag = 1;";
    var totalFundedLeads = _dbService.ExecuteScalar<int>(fundedLeadsQuery);

    int leadsWithNeither = totalLeads - (leadsWithPhones + leadsWithEmails - leadsWithBoth);
    int totalUnfundedLeads = totalLeads - totalFundedLeads;
    double overallFundedPercentage = totalLeads == 0 ? 0 : (double)totalFundedLeads / totalLeads * 100;

    // Date range-specific queries
    string leadsWithinDateRangeQuery = @"
        SELECT COUNT(*) AS LeadsWithinDateRange
        FROM Responses
        WHERE Lead_Timestamp BETWEEN @StartDate AND @EndDate;";
    var leadsWithinDateRange = _dbService.ExecuteScalar<int>(leadsWithinDateRangeQuery, 
        new[]
        {
            new MySqlParameter("@StartDate", MySqlDbType.DateTime) { Value = startDate },
            new MySqlParameter("@EndDate", MySqlDbType.DateTime) { Value = endDate }
        });

    string fundedLeadsWithinDateRangeQuery = @"
        SELECT COUNT(*) AS FundedLeadsWithinDateRange
        FROM Responses
        WHERE Funded_Flag = 1 AND Lead_Timestamp BETWEEN @StartDate AND @EndDate;";
    var fundedLeadsWithinDateRange = _dbService.ExecuteScalar<int>(fundedLeadsWithinDateRangeQuery, 
        new[]
        {
            new MySqlParameter("@StartDate", MySqlDbType.DateTime) { Value = startDate },
            new MySqlParameter("@EndDate", MySqlDbType.DateTime) { Value = endDate }
        });

    string averageFundedRateQuery = @"
        SELECT AVG(CASE WHEN Funded_Flag = 1 THEN 1 ELSE 0 END) AS AverageFundedRate
        FROM Responses
        WHERE Lead_Flag = 1 AND Lead_Timestamp BETWEEN @StartDate AND @EndDate;";
    var averageFundedRate = _dbService.ExecuteScalar<double>(averageFundedRateQuery, 
        new[]
        {
            new MySqlParameter("@StartDate", MySqlDbType.DateTime) { Value = startDate },
            new MySqlParameter("@EndDate", MySqlDbType.DateTime) { Value = endDate }
        });

    // Combine into structured result
    return new CombinedLeadStatistics
    {
        // Overall metrics
        TotalLeads = totalLeads,
        LeadsWithPhones = leadsWithPhones,
        LeadsWithEmails = leadsWithEmails,
        LeadsWithBoth = leadsWithBoth,
        LeadsWithNeither = leadsWithNeither,
        TotalFundedLeads = totalFundedLeads,
        TotalUnfundedLeads = totalUnfundedLeads,
        OverallFundedPercentage = overallFundedPercentage,

        // Date-range-specific metrics
        LeadsWithinDateRange = leadsWithinDateRange,
        FundedLeadsWithinDateRange = fundedLeadsWithinDateRange,
        AverageFundedRate = averageFundedRate,
        StartDate = startDate,
        EndDate = endDate
    };
}
    }
}
