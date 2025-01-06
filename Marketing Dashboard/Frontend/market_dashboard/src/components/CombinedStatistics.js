import React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import CardActionArea from "@mui/material/CardActionArea";

export default function CombinedStatistics({
  statisticsData,
  startDate,
  endDate,
  handleStartDateChange,
  handleEndDateChange,
}) {
  if (!statisticsData) {
    return <Typography variant="h6">Loading lead statistics...</Typography>;
  }

  const {
    totalLeads,
    leadsWithPhones,
    leadsWithEmails,
    leadsWithBoth,
    leadsWithNeither,
    totalFundedLeads,
    totalUnfundedLeads,
    overallFundedPercentage,
    leadsWithinDateRange,
    fundedLeadsWithinDateRange,
    averageFundedRate,
    startDate: dataStartDate,
    endDate: dataEndDate,
  } = statisticsData;

  return (
    <Box
      sx={{
        display: "grid",
        gap: 2,
        gridTemplateColumns: "repeat(auto-fill, minmax(250px, 1fr))",
        padding: 2,
      }}
    >
      {/* Lead Statistics Section */}
      <Typography variant="h4" sx={{ gridColumn: "1 / -1", marginBottom: 2 }}>
        Lead Statistics
      </Typography>
      {[
        { title: "Total Leads", value: totalLeads },
        { title: "Leads with Phones", value: leadsWithPhones },
        { title: "Leads with Emails", value: leadsWithEmails },
        { title: "Leads with Both", value: leadsWithBoth },
        { title: "Leads with Neither", value: leadsWithNeither },
        { title: "Overall Funded Percentage", value: `${overallFundedPercentage}%` },
        { title: "Total Funded Leads", value: totalFundedLeads },
        { title: "Total Unfunded Leads", value: totalUnfundedLeads },
      ].map((stat, index) => (
        <Card key={index}>
          <CardContent>
            <Typography variant="h6">{stat.title}</Typography>
            <Typography variant="body1">{stat.value}</Typography>
          </CardContent>
        </Card>
      ))}

      {/* Funded Rates Section */}
      <Typography variant="h4" sx={{ gridColumn: "1 / -1", marginTop: 4 }}>
        Percentage of Responses That Get Funding
      </Typography>
      <Card sx={{ gridColumn: "1 / -1" }}>
        <CardContent>
          <Typography variant="h6">Select Date Range</Typography>
          <Box sx={{ display: "flex", gap: 2, marginTop: 2 }}>
            <Box>
              <label>
                Start Date:
                <input
                  type="date"
                  value={startDate || dataStartDate.split("T")[0]}
                  onChange={handleStartDateChange}
                  style={{ display: "block", marginTop: 4 }}
                />
              </label>
            </Box>
            <Box>
              <label>
                End Date:
                <input
                  type="date"
                  value={endDate || dataEndDate.split("T")[0]}
                  onChange={handleEndDateChange}
                  style={{ display: "block", marginTop: 4 }}
                />
              </label>
            </Box>
          </Box>
        </CardContent>
      </Card>
      {[
        {
          title: "Average Funded Rate of Responses",
          value: `${(averageFundedRate * 100).toFixed(2)}%`,
        },
        { title: "Responses within Date Range", value: leadsWithinDateRange },
        {
          title: "Funded Responses within Date Range",
          value: fundedLeadsWithinDateRange,
        },
      ].map((stat, index) => (
        <Card key={index}>
          <CardContent>
            <Typography variant="h6">{stat.title}</Typography>
            <Typography variant="body1">{stat.value}</Typography>
          </CardContent>
        </Card>
      ))}
    </Box>
  );
}
