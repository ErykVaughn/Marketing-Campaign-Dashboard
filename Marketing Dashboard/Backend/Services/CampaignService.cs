using MySql.Data.MySqlClient;


namespace BackendAPI.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IGenericDatabaseService _dbService;

        public CampaignService(IGenericDatabaseService dbService)
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

        // Get number of leads for a specific campaign
        public int GetLeadsForCampaign(int campaignId)
        {
            string query = @"
                SELECT COUNT(*) 
                FROM Audiences 
                WHERE Campaign_ID = @CampaignId";

            var param = new MySqlParameter("@CampaignId", MySqlDbType.Int32) { Value = campaignId };
            return _dbService.ExecuteScalar<int>(query, param);
        }

        // Get lead rate for records with an email
        public LeadStatistics GetLeadStatistics()
        {
            // Query to get total leads
            string totalLeadsQuery = @"
        SELECT COUNT(*) AS TotalLeads
        FROM Audiences;";
            var totalLeads = _dbService.ExecuteScalar<int>(totalLeadsQuery);

            // Query to get leads with phone numbers
            string leadsWithPhonesQuery = @"
        SELECT COUNT(*) AS LeadsWithPhones
        FROM Audiences
        WHERE Has_Phone = 1;";
            var leadsWithPhones = _dbService.ExecuteScalar<int>(leadsWithPhonesQuery);

            // Query to get leads with emails
            string leadsWithEmailsQuery = @"
        SELECT COUNT(*) AS LeadsWithEmails
        FROM Audiences
        WHERE Has_Email = 1;";
            var leadsWithEmails = _dbService.ExecuteScalar<int>(leadsWithEmailsQuery);

            // Calculate ratios
            double phoneRatio = totalLeads == 0 ? 0 : (double)leadsWithPhones / totalLeads;
            double emailRatio = totalLeads == 0 ? 0 : (double)leadsWithEmails / totalLeads;

            // Combine results into a structured object
            return new LeadStatistics
            {
                TotalLeads = totalLeads,
                LeadsWithPhones = leadsWithPhones,
                LeadsWithEmails = leadsWithEmails,
                PhoneRatio = phoneRatio,
                EmailRatio = emailRatio
            };
        }

        // Get average funded rate for records with a Lead_Flag within a specific date range
        public double GetAverageFundedRate(DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT 
                    AVG(CASE WHEN r.Funded_Flag = 1 AND r.Funded_Timestamp BETWEEN @StartDate AND @EndDate THEN 1 ELSE 0 END) AS AverageFundedRate
                FROM Responses r
                WHERE r.Lead_Flag = 1 AND r.Lead_Timestamp BETWEEN @StartDate AND @EndDate";

            var parameters = new[]
            {
                new MySqlParameter("@StartDate", MySqlDbType.DateTime) { Value = startDate },
                new MySqlParameter("@EndDate", MySqlDbType.DateTime) { Value = endDate }
            };

            try
            {
                var result = _dbService.ExecuteScalar<double>(query, parameters);
                return result;
            }
            catch (Exception)
            {
                return 0; // Or handle error gracefully
            }
        }
    }
}
