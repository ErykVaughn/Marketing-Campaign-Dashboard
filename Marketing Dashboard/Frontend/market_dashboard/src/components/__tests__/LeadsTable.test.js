import * as React from 'react';
import { render, screen } from '@testing-library/react';
import LeadsTable from '../LeadsTable'; // Adjust the import path as necessary
import '@testing-library/jest-dom';

describe('LeadsTable Component', () => {
  const mockLeadsData = [
    {
      campaign_ID: '123',
      record_ID: '456',
      has_Phone: true,
      has_Email: false,
    },
    {
      campaign_ID: '789',
      record_ID: '101',
      has_Phone: false,
      has_Email: true,
    },
    {
      campaign_ID: '112',
      record_ID: '131',
      has_Phone: true,
      has_Email: true,
    },
  ];

  it('renders table headers correctly', () => {
    render(<LeadsTable leads={mockLeadsData} />);
    
    // Check if the table headers are displayed correctly
    expect(screen.getByText('Campaign ID')).toBeInTheDocument();
    expect(screen.getByText('Record ID')).toBeInTheDocument();
    expect(screen.getByText('Has Phone')).toBeInTheDocument();
    expect(screen.getByText('Has Email')).toBeInTheDocument();
  });

  it('renders lead data correctly', () => {
    render(<LeadsTable leads={mockLeadsData} />);
    
    // Check if the table rows contain the correct lead data
    expect(screen.getByText('123')).toBeInTheDocument();
    expect(screen.getByText('456')).toBeInTheDocument();

    expect(screen.getByText('789')).toBeInTheDocument();
    expect(screen.getByText('101')).toBeInTheDocument();

    expect(screen.getByText('112')).toBeInTheDocument();
    expect(screen.getByText('131')).toBeInTheDocument();
  });
});
