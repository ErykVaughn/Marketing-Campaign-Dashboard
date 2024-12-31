import '../styles/globals.css'; // Ensure the global styles are linked
import React from "react";

export default function LeadRate({ leadStatistics }) {
  if (!leadStatistics) {
    return <p>Loading lead statistics...</p>;
  }

  const { totalLeads, leadsWithPhones, leadsWithEmails, phoneRatio, emailRatio } = leadStatistics;

  return (
    <div className="lead-rate-container">
      <h2>Lead Statistics</h2>
      <div className="stat-item">
        <h3>Total Leads</h3>
        <p>{totalLeads}</p>
      </div>
      <div className="stat-item">
        <h3>Leads with Phones</h3>
        <p>{leadsWithPhones}</p>
      </div>
      <div className="stat-item">
        <h3>Leads with Emails</h3>
        <p>{leadsWithEmails}</p>
      </div>
      <div className="stat-item">
        <h3>Phone Ratio</h3>
        <p>{(phoneRatio * 100).toFixed(2)}%</p>
      </div>
      <div className="stat-item">
        <h3>Email Ratio</h3>
        <p>{(emailRatio * 100).toFixed(2)}%</p>
      </div>
    </div>
  );
}
