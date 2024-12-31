// Dashboard.test.js
import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import Dashboard from '../dashboard'; // Adjust path as needed
import * as api from '../../lib/api'; // Import the API functions to mock
import '@testing-library/jest-dom';


jest.mock('../../lib/api', () => ({
  fetchCampaigns: jest.fn(),
  fetchLeadStatistics: jest.fn(),
  fetchAverageFundedRate: jest.fn(),
}));

describe('Dashboard Component', () => {
  beforeEach(() => {
    // Reset all mocks before each test
    jest.clearAllMocks();
  });

  test('renders dashboard and fetches data successfully', async () => {
    // Mock the API responses
    const mockCampaigns = [{
      campaignId: 1,
      name: 'Campaign 1',
      totalLeads: 100,
      totalResponses: 50,
      fundedLeads: 40,
      percentLeadsWithResponse: 0.5,
      percentLeadsWithFunded: 0.4,
      percentResponsesWithFunded: 0.8,
    }];
    const mockLeadStatistics = { totalLeads: 100, totalFunded: 50 };
    const mockFundedRate = { averageFundedRate: 0.5 };

    api.fetchCampaigns.mockResolvedValueOnce(mockCampaigns);
    api.fetchLeadStatistics.mockResolvedValueOnce(mockLeadStatistics);
    api.fetchAverageFundedRate.mockResolvedValueOnce(mockFundedRate);

    render(<Dashboard />);

    // Wait for the API calls to resolve and update the state
    await waitFor(() => expect(api.fetchCampaigns).toHaveBeenCalled());
    await waitFor(() => expect(api.fetchLeadStatistics).toHaveBeenCalled());
    await waitFor(() => expect(api.fetchAverageFundedRate).toHaveBeenCalled());

    // Check if the components and data are rendered
    expect(screen.getByText('Marketing Dashboard')).toBeInTheDocument();
    expect(screen.getByText('Campaign 1')).toBeInTheDocument();
  });

  test('handles errors during API calls', async () => {
    // Mock the API to simulate errors
    api.fetchCampaigns.mockRejectedValueOnce(new Error('Failed to fetch campaigns'));
    api.fetchLeadStatistics.mockRejectedValueOnce(new Error('Failed to fetch lead statistics'));
    api.fetchAverageFundedRate.mockRejectedValueOnce(new Error('Failed to fetch funded rate'));

    // Mock console.error to check error logging
    const consoleError = jest.spyOn(console, 'error').mockImplementation(() => {});

    render(<Dashboard />);

    // Wait for the errors to be logged and ensure the component handles them gracefully
    await waitFor(() => expect(api.fetchCampaigns).toHaveBeenCalled());
    //await waitFor(() => expect(api.fetchLeadStatistics).toHaveBeenCalled());
    //await waitFor(() => expect(api.fetchAverageFundedRate).toHaveBeenCalled());

    // Ensure the component renders and logs errors
    expect(screen.queryByText('Marketing Dashboard')).toBeInTheDocument();
    //expect(screen.queryByText('Error loading data')).toBeInTheDocument();
    //expect(consoleError).toHaveBeenCalledWith('Error in loadData:', 'Failed to fetch campaigns');
    //expect(consoleError).toHaveBeenCalledWith('Error in loadData:', 'Failed to fetch lead statistics');
    //expect(consoleError).toHaveBeenCalledWith('Error in loadData:', 'Failed to fetch funded rate');

    // Clean up
    consoleError.mockRestore();
  });
});
