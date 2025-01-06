using System;
using System.Collections.Generic;

namespace BackendAPI.Services
{
    public interface ICampaignService
    {
        List<Campaign> GetAllCampaigns();
        CombinedLeadStatistics GetEnhancedLeadStatistics(DateTime startDate, DateTime endDate);
    }
}
