import * as React from 'react';
import { act } from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import Dashboard from "../index"; // Adjust the path as needed
import { fetchAllCampaigns, fetchAverageFundedRate } from "../../lib/api";
import '@testing-library/jest-dom';

// Mock the API calls
jest.mock("../../lib/api", () => ({
  fetchAllCampaigns: jest.fn(),
  fetchAverageFundedRate: jest.fn(),
}));

describe("Dashboard Component", () => {
  beforeEach(() => {
    // Mock successful API responses
    fetchAllCampaigns.mockResolvedValue([
      { id: 1, name: "Campaign 1" },
      { id: 2, name: "Campaign 2" },
    ]);
    fetchAverageFundedRate.mockResolvedValue([
      { campaignId: 1, fundedRate: 0.5 },
      { campaignId: 2, fundedRate: 0.75 },
    ]);
  });

  it("calls the API with the correct date range when start and end dates are changed", async () => {
    render(<Dashboard />);
  
    // Wait for the Start Date input to appear
    const startDateInput = await screen.findByLabelText(/start date/i); 
    const endDateInput = await screen.findByLabelText(/end date/i);
  
    fireEvent.change(startDateInput, { target: { value: "2024-12-10" } });
    fireEvent.change(endDateInput, { target: { value: "2024-12-20" } });
  
    // Wait for the new API call to be made
    await waitFor(() => {
      expect(fetchAverageFundedRate).toHaveBeenCalledWith("2024-12-10", "2024-12-20");
    });
  });

  it("displays error message when API call fails", async () => {
    // Mock API to throw error
    fetchAllCampaigns.mockRejectedValueOnce(new Error("Failed to fetch campaigns"));
  
    // Spy on console.error
    const consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
  
    render(<Dashboard />);
  
    // Wait for the error to be logged
    await waitFor(() => {
      expect(consoleErrorSpy).toHaveBeenCalledWith("Error in loadData:", "Failed to fetch campaigns");
    });
  
    // Clean up the spy after the test
    consoleErrorSpy.mockRestore();
  });
});
