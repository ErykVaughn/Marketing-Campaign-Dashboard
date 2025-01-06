import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom";
import CampaignList from "../CampaignList";
import { fetchResponseByCampaignId, fetchAudienceByCampaignId } from "../../lib/api";

// Mock the API calls
jest.mock("../../lib/api", () => ({
  fetchResponseByCampaignId: jest.fn(),
  fetchAudienceByCampaignId: jest.fn(),
}));

const mockCampaigns = [
  {
    campaignId: "123",
    name: "Test Campaign",
    totalLeads: 100,
    totalResponses: 50,
    fundedLeads: 30,
    percentLeadsWithResponse: 0.5,
    percentLeadsWithFunded: 0.3,
    percentResponsesWithFunded: 0.6,
  },
];

describe("CampaignList Component", () => {
  it("renders the CampaignList with campaigns", () => {
    render(<CampaignList campaigns={mockCampaigns} />);
    expect(screen.getByText("Campaign Statistics")).toBeInTheDocument();
    expect(screen.getByText("Test Campaign")).toBeInTheDocument();
    expect(screen.getByText("Total Leads")).toBeInTheDocument();
  });

  it("displays a message if no campaigns are available", () => {
    render(<CampaignList campaigns={[]} />);
    expect(screen.getByText("No campaigns available.")).toBeInTheDocument();
  });

  it("expands and loads data on clicking the expand button", async () => {
    const mockLeads = [
      { campaign_ID: "123", record_ID: "lead1", has_Phone: true, has_Email: true },
    ];
    const mockResponses = [
      { campaign_ID: "123", record_ID: "response1", lead_Flag: true, funded_Flag: false, lead_Timestamp: null, funded_Timestamp: null },
    ];

    fetchAudienceByCampaignId.mockResolvedValue(mockLeads);
    fetchResponseByCampaignId.mockResolvedValue(mockResponses);

    render(<CampaignList campaigns={mockCampaigns} />);

    // Expand the campaign row
    const expandButton = screen.getByLabelText("expand row");
    fireEvent.click(expandButton);

    // Wait for the API calls and data to load
    await waitFor(() => {
      expect(fetchAudienceByCampaignId).toHaveBeenCalledWith("123");
      expect(fetchResponseByCampaignId).toHaveBeenCalledWith("123");
    });

    // Wait for the state updates and rendering of the tables
    await waitFor(() => {
      expect(screen.getByText("Leads Table")).toBeInTheDocument();
      expect(screen.getByText("lead1")).toBeInTheDocument();
      expect(screen.getByText("response1")).toBeInTheDocument();
    });
  });
});
