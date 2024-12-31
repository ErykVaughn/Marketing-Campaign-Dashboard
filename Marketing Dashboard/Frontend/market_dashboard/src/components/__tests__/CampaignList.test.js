import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom'; // Ensure jest-dom is globally available
import CampaignList from '../CampaignList'; // Adjust the import path as necessary

// Test: displays loading message when no campaigns are provided
it('displays loading message when no campaigns are provided', () => {
  render(<CampaignList campaigns={[]} />);
  expect(screen.getByText('No campaigns available.')).toBeInTheDocument();
});

// Test: displays campaign statistics when campaigns are provided
it('displays campaign statistics when campaigns are provided', () => {
  const campaigns = [
    {
      campaignId: 1,
      name: 'Campaign 1',
      totalLeads: 100,
      totalResponses: 50,
      fundedLeads: 40,
      percentLeadsWithResponse: 0.5,
      percentLeadsWithFunded: 0.4,
      percentResponsesWithFunded: 0.8,
    },
  ];

  render(<CampaignList campaigns={campaigns} />);

  // Check if the statistics are rendered correctly
  expect(screen.getByText(/Campaign ID:/)).toBeInTheDocument();
  expect(screen.getByText('Campaign 1')).toBeInTheDocument();
  
  // Use regular expression to match 'Total Leads' text more flexibly
  expect(screen.getByText(/Total Leads/)).toBeInTheDocument();

  // Check if the value of 'Total Leads' is displayed
  expect(screen.getByText('100')).toBeInTheDocument();
});

// Test: renders empty list when no campaigns are passed
it('renders empty list when no campaigns are passed', () => {
  render(<CampaignList campaigns={[]} />);
  expect(screen.getByText('No campaigns available.')).toBeInTheDocument();
});
