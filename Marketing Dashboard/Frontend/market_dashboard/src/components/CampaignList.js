import React from 'react';
import '../styles/globals.css'; // Ensure the global styles are linked

export default function CampaignList({ campaigns }) {
  return (
    <section className="campaigns-section">
      <h2>Campaign Statistics</h2>
      <div className="campaign-container">
        {campaigns.length === 0 ? (
          <p>No campaigns available.</p>
        ) : (
          campaigns.map((campaign) => (
            <div className="card" key={campaign.campaignId}>
              <h3>Campaign ID: {campaign.campaignId}</h3>

              <div className="campaign-stat-item">
                <p><strong>Name:</strong> {campaign.name}</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Total Leads:</strong> {campaign.totalLeads}</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Total Responses:</strong> {campaign.totalResponses}</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Funded Leads:</strong> {campaign.fundedLeads}</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Percent Leads with Response:</strong> {(campaign.percentLeadsWithResponse * 100).toFixed(2)}%</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Percent Leads with Funded:</strong> {(campaign.percentLeadsWithFunded * 100).toFixed(2)}%</p>
              </div>

              <div className="campaign-stat-item">
                <p><strong>Percent Responses with Funded:</strong> {(campaign.percentResponsesWithFunded * 100).toFixed(2)}%</p>
              </div>
            </div>
          ))
        )}
      </div>
    </section>
  );
}
