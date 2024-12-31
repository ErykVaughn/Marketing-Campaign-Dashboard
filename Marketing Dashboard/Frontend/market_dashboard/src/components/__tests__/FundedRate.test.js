import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom'; // Ensure jest-dom is globally available
import FundedRate from '../FundedRate'; // Adjust the import path as necessary

// Mock functions for the date change handlers
const mockHandleStartDateChange = jest.fn();
const mockHandleEndDateChange = jest.fn();

// Test: renders "Loading..." when no fundedRates are provided
it('renders loading message when no funded rates are provided', () => {
  render(<FundedRate fundedRates={[]} startDate="2024-01-01" endDate="2024-12-31" handleStartDateChange={mockHandleStartDateChange} handleEndDateChange={mockHandleEndDateChange} />);

  expect(screen.getByText('Loading...')).toBeInTheDocument();
});

// Test: renders funded rates when provided
it('displays funded rates when provided', () => {
  const fundedRates = [0.25, 0.5, 0.75];

  render(<FundedRate fundedRates={fundedRates} startDate="2024-01-01" endDate="2024-12-31" handleStartDateChange={mockHandleStartDateChange} handleEndDateChange={mockHandleEndDateChange} />);

  // Check if the funded rates are displayed as percentages
  expect(screen.getByText('25.00%')).toBeInTheDocument();
  expect(screen.getByText('50.00%')).toBeInTheDocument();
  expect(screen.getByText('75.00%')).toBeInTheDocument();
});

// Test: handles start date change
it('calls handleStartDateChange when start date is changed', () => {
  render(<FundedRate fundedRates={[0.25]} startDate="2024-01-01" endDate="2024-12-31" handleStartDateChange={mockHandleStartDateChange} handleEndDateChange={mockHandleEndDateChange} />);

  const startDateInput = screen.getByLabelText(/Start Date:/);
  fireEvent.change(startDateInput, { target: { value: '2024-06-01' } });

  expect(mockHandleStartDateChange).toHaveBeenCalledTimes(1);
  expect(mockHandleStartDateChange).toHaveBeenCalledWith(expect.anything());
});

// Test: handles end date change
it('calls handleEndDateChange when end date is changed', () => {
  render(<FundedRate fundedRates={[0.25]} startDate="2024-01-01" endDate="2024-12-31" handleStartDateChange={mockHandleStartDateChange} handleEndDateChange={mockHandleEndDateChange} />);

  const endDateInput = screen.getByLabelText(/End Date:/);
  fireEvent.change(endDateInput, { target: { value: '2024-12-01' } });

  expect(mockHandleEndDateChange).toHaveBeenCalledTimes(1);
  expect(mockHandleEndDateChange).toHaveBeenCalledWith(expect.anything());
});
