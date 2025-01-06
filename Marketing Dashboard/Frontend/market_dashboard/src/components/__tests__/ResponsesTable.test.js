import * as React from 'react';
import { render, screen } from '@testing-library/react';
import ResponsesTable from '../ResponsesTable'; // Adjust the import path as necessary
import '@testing-library/jest-dom';

describe('ResponsesTable Component', () => {
  const mockResponsesData = [
    {
      campaign_ID: '123',
      record_ID: '456',
      lead_Flag: true,
      lead_Timestamp: '2024-01-01T12:00:00Z',
      funded_Flag: true,
      funded_Timestamp: '2024-01-02T12:00:00Z',
    },
    {
      campaign_ID: '789',
      record_ID: '101',
      lead_Flag: false,
      lead_Timestamp: null,
      funded_Flag: true,
      funded_Timestamp: '2024-01-03T12:00:00Z',
    },
    {
      campaign_ID: '112',
      record_ID: '131',
      lead_Flag: true,
      lead_Timestamp: '2024-01-04T12:00:00Z',
      funded_Flag: false,
      funded_Timestamp: null,
    },
  ];

  it('renders table headers correctly', () => {
    render(<ResponsesTable responses={mockResponsesData} />);
    
    // Check if the table headers are displayed correctly
    expect(screen.getByText('Campaign ID')).toBeInTheDocument();
    expect(screen.getByText('Record ID')).toBeInTheDocument();
    expect(screen.getByText('Lead Flag')).toBeInTheDocument();
    expect(screen.getByText('Lead Timestamp')).toBeInTheDocument();
    expect(screen.getByText('Funded Flag')).toBeInTheDocument();
    expect(screen.getByText('Funded Timestamp')).toBeInTheDocument();
  });

  it('renders response data correctly', () => {
    render(<ResponsesTable responses={mockResponsesData} />);
    
    // Check if the correct data for each response is displayed
    expect(screen.getByText('123')).toBeInTheDocument();
    expect(screen.getByText('456')).toBeInTheDocument();
    expect(screen.getByText('1/1/2024, 6:00:00 AM')).toBeInTheDocument();  // Adjust based on your locale
    expect(screen.getByText('1/2/2024, 6:00:00 AM')).toBeInTheDocument();  // Adjust based on your locale

    expect(screen.getByText('789')).toBeInTheDocument();
    expect(screen.getByText('101')).toBeInTheDocument();
    expect(screen.getByText('1/3/2024, 6:00:00 AM')).toBeInTheDocument();  // Adjust based on your locale

    expect(screen.getByText('112')).toBeInTheDocument();
    expect(screen.getByText('131')).toBeInTheDocument();
    expect(screen.getByText('1/4/2024, 6:00:00 AM')).toBeInTheDocument();  // Adjust based on your locale
  });


});
