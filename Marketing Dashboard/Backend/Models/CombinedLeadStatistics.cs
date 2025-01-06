public class CombinedLeadStatistics
{
    // Overall metrics
    public int TotalLeads { get; set; }
    public int LeadsWithPhones { get; set; }
    public int LeadsWithEmails { get; set; }
    public int LeadsWithBoth { get; set; }
    public int LeadsWithNeither { get; set; }
    public int TotalFundedLeads { get; set; }
    public int TotalUnfundedLeads { get; set; }
    public double OverallFundedPercentage { get; set; }

    // Date-range-specific metrics
    public int LeadsWithinDateRange { get; set; }
    public int FundedLeadsWithinDateRange { get; set; }
    public double AverageFundedRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
