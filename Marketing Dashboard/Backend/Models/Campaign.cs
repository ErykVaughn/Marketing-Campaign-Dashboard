public class Campaign
{
    public int CampaignId { get; set; }
    public required string Name { get; set; }
    public int TotalLeads { get; set; }
    public int FundedLeads { get; set; }
    public int TotalResponses { get; set; }  // Added to track the total number of responses
    public double PercentLeadsWithResponse { get; set; }  // Added to track the percentage of leads that got responses
    public double PercentLeadsWithFunded { get; set; }  // Added to track the percentage of leads that got funded
    public double PercentResponsesWithFunded { get; set; }  // Added to track the percentage of responses that got funded
}
