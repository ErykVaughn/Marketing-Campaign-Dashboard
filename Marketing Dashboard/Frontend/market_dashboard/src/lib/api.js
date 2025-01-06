// api.js - Backend API interaction file

const BASE_URL = "http://localhost:8443/api";

/**
 * Helper function to handle API requests.
 * @param {string} endpoint - The API endpoint to call.
 * @param {Object} options - The fetch options (method, headers, body, etc.).
 * @returns {Promise<any>} - The parsed JSON response.
 */
const apiRequest = async (endpoint, options = {}) => {
  try {
    const response = await fetch(`${BASE_URL}${endpoint}`, {
      headers: { "Content-Type": "application/json", ...options.headers },
      ...options,
    });
    if (!response.ok) {
      throw new Error(`Error: ${response.status} - ${response.statusText}`);
    }
    return response.json();
  } catch (error) {
    console.error("API Request Failed:", error);
    throw error;
  }
};

// Audience Endpoints

/** Fetch all audience data */
export const fetchAllAudiences = async () => {
  return apiRequest("/Audience");
};

/** Fetch audience data for a specific campaign */
export const fetchAudienceByCampaignId = async (campaignId) => {
  return apiRequest(`/Audience/${campaignId}`);
};

// Campaign Endpoints

/** Fetch all campaigns */
export const fetchAllCampaigns = async () => {
  return apiRequest("/Campaign");
};

/** Fetch average funded rate within a date range */
export const fetchAverageFundedRate = async (startDate, endDate) => {
  const queryParams = new URLSearchParams({ startDate, endDate }).toString();
  return apiRequest(`/Campaign/average-funded-rate?${queryParams}`);
};

// Response Endpoints

/** Fetch all responses */
export const fetchAllResponses = async () => {
  return apiRequest("/Response");
};

/** Fetch responses for a specific campaign */
export const fetchResponseByCampaignId = async (campaignId) => {
  return apiRequest(`/Response/${campaignId}`);
};

// Example Usage in a Component
// You can import these functions and use them in your React components as needed:

/*
import { fetchAllAudiences, fetchAllCampaigns, fetchAverageFundedRate } from './api';

const App = () => {
  useEffect(() => {
    const fetchData = async () => {
      const audiences = await fetchAllAudiences();
      console.log("Audiences:", audiences);

      const campaigns = await fetchAllCampaigns();
      console.log("Campaigns:", campaigns);

      const averageFundedRate = await fetchAverageFundedRate("2024-12-01", "2024-12-15");
      console.log("Average Funded Rate:", averageFundedRate);
    };

    fetchData();
  }, []);

  return <div>Check console for API results</div>;
};
*/
