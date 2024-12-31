import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom'; // Ensure jest-dom is globally available
import LeadRate from '../LeadRate'; // Adjust the import path as necessary

describe('LeadRate Component', () => {
  
  // Test 1: Loading state when no leadStatistics are passed
  test('displays loading message when no leadStatistics are provided', () => {
    render(<LeadRate leadStatistics={null} />); // Passing null to simulate loading state
    expect(screen.getByText('Loading lead statistics...')).toBeInTheDocument();
  });

  // Test 2: Displays lead statistics when leadStatistics are provided
  test('displays lead statistics when leadStatistics are provided', () => {
    const leadStatistics = {
      totalLeads: 1000,
      leadsWithPhones: 800,
      leadsWithEmails: 750,
      phoneRatio: 0.8,
      emailRatio: 0.75,
    };

    render(<LeadRate leadStatistics={leadStatistics} />);

    // Check if the statistics are rendered correctly
    expect(screen.getByText('Total Leads')).toBeInTheDocument();
    expect(screen.getByText('1000')).toBeInTheDocument();

    expect(screen.getByText('Leads with Phones')).toBeInTheDocument();
    expect(screen.getByText('800')).toBeInTheDocument();

    expect(screen.getByText('Leads with Emails')).toBeInTheDocument();
    expect(screen.getByText('750')).toBeInTheDocument();

    expect(screen.getByText('Phone Ratio')).toBeInTheDocument();
    expect(screen.getByText('80.00%')).toBeInTheDocument(); // Check if the phone ratio is rendered correctly

    expect(screen.getByText('Email Ratio')).toBeInTheDocument();
    expect(screen.getByText('75.00%')).toBeInTheDocument(); // Check if the email ratio is rendered correctly
  });

});
