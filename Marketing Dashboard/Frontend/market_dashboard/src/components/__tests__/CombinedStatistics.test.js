import * as React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import CombinedStatistics from '../CombinedStatistics'; // Adjust the import path as necessary
import '@testing-library/jest-dom/';

describe('CombinedStatistics Component', () => {
  const mockHandleStartDateChange = jest.fn();
  const mockHandleEndDateChange = jest.fn();

  const mockStatisticsData = {
    totalLeads: 100,
    leadsWithPhones: 10,
    leadsWithEmails: 20,
    leadsWithBoth: 30,
    leadsWithNeither: 40,
    totalFundedLeads: 50,
    totalUnfundedLeads: 60,
    overallFundedPercentage: 25,  // Unique percentage value
    leadsWithinDateRange: 70,
    fundedLeadsWithinDateRange: 80,
    averageFundedRate: 0.75,  // Unique rate
    startDate: "2024-01-01T00:00:00Z",
    endDate: "2024-12-31T23:59:59Z",
  };

  it('displays loading message when statisticsData is not provided', () => {
    render(<CombinedStatistics statisticsData={null} />);
    const loadingMessage = screen.getByText('Loading lead statistics...');
    expect(loadingMessage).toBeInTheDocument();
  });

  it('renders the statistics data correctly', () => {
    render(
      <CombinedStatistics
        statisticsData={mockStatisticsData}
        startDate="2024-01-01"
        endDate="2024-12-31"
        handleStartDateChange={mockHandleStartDateChange}
        handleEndDateChange={mockHandleEndDateChange}
      />
    );

    // Check that the title "Lead Statistics" is rendered
    expect(screen.getByText('Lead Statistics')).toBeInTheDocument();

    // Check that each statistic is rendered with the correct value
    expect(screen.getByText('Total Leads')).toBeInTheDocument();
    expect(screen.getByText('100')).toBeInTheDocument();

    expect(screen.getByText('Leads with Phones')).toBeInTheDocument();
    expect(screen.getByText('10')).toBeInTheDocument();

    expect(screen.getByText('Leads with Emails')).toBeInTheDocument();
    expect(screen.getByText('20')).toBeInTheDocument();

    expect(screen.getByText('Leads with Both')).toBeInTheDocument();
    expect(screen.getByText('30')).toBeInTheDocument();

    expect(screen.getByText('Leads with Neither')).toBeInTheDocument();
    expect(screen.getByText('40')).toBeInTheDocument();

    expect(screen.getByText('Overall Funded Percentage')).toBeInTheDocument();
    expect(screen.getByText('25%')).toBeInTheDocument();

    expect(screen.getByText('Total Funded Leads')).toBeInTheDocument();
    expect(screen.getByText('50')).toBeInTheDocument();

    expect(screen.getByText('Total Unfunded Leads')).toBeInTheDocument();
    expect(screen.getByText('60')).toBeInTheDocument();
  });

  it('calls handleStartDateChange when start date is changed', () => {
    render(
      <CombinedStatistics
        statisticsData={mockStatisticsData}
        startDate="2024-01-01"
        endDate="2024-12-31"
        handleStartDateChange={mockHandleStartDateChange}
        handleEndDateChange={mockHandleEndDateChange}
      />
    );

    // Simulate changing the start date input
    const startDateInput = screen.getByLabelText(/start date/i);
    fireEvent.change(startDateInput, { target: { value: '2025-01-01' } });

    // Verify that the handler function was called
    expect(mockHandleStartDateChange).toHaveBeenCalledWith(expect.anything());
  });

  it('calls handleEndDateChange when end date is changed', () => {
    render(
      <CombinedStatistics
        statisticsData={mockStatisticsData}
        startDate="2024-01-01"
        endDate="2024-12-31"
        handleStartDateChange={mockHandleStartDateChange}
        handleEndDateChange={mockHandleEndDateChange}
      />
    );

    // Simulate changing the end date input
    const endDateInput = screen.getByLabelText(/end date/i);
    fireEvent.change(endDateInput, { target: { value: '2025-01-01' } });

    // Verify that the handler function was called
    expect(mockHandleEndDateChange).toHaveBeenCalledWith(expect.anything());
  });
});
