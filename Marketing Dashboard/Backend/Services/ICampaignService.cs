using System;
using System.Collections.Generic;

namespace BackendAPI.Services
{
    public interface ICampaignService
    {
        List<Campaign> GetAllCampaigns();
        int GetLeadsForCampaign(int campaignId);
        LeadStatistics GetLeadStatistics();
        double GetAverageFundedRate(DateTime startDate, DateTime endDate);
    }
}
