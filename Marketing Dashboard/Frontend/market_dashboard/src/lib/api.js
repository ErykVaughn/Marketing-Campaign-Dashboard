const API_BASE = "http://localhost:8443/api/Campaign";

export const fetchCampaigns = async () => {
    const response = await fetch(`${API_BASE}`);
    if (!response.ok) {
        throw new Error("Failed to fetch campaigns");
    }
    return await response.json();
};

export const fetchLeadsForCampaign = async (campaignId) => {
    const response = await fetch(`${API_BASE}/${campaignId}/leads`);
    if (!response.ok) {
        throw new Error("Failed to fetch leads for campaign");
    }
    return await response.json();
};

// Add this function to fetch the lead statistics
export const fetchLeadStatistics = async () => {
    const response = await fetch(`${API_BASE}/lead-rate`);  // Adjust this URL if necessary
    if (!response.ok) {
        throw new Error("Failed to fetch lead statistics");
    }
    return await response.json();
};

export const fetchAverageFundedRate = async (startDate, endDate) => {
    const response = await fetch(
        `${API_BASE}/average-funded-rate?startDate=${startDate}&endDate=${endDate}`
    );
    if (!response.ok) {
        throw new Error("Failed to fetch average funded rate");
    }
    return await response.json();
};
