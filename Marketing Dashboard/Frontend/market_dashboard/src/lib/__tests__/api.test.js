import { fetchAllAudiences, fetchAudienceByCampaignId, fetchAllCampaigns, fetchAverageFundedRate, fetchAllResponses, fetchResponseByCampaignId } from '../api';
import fetchMock from 'jest-fetch-mock';

fetchMock.enableMocks();

describe('API Functions', () => {
  beforeEach(() => {
    fetchMock.resetMocks(); // Reset mocks before each test
  });

  it('fetchAllAudiences should fetch all audiences successfully', async () => {
    // Mock the response
    fetchMock.mockResponseOnce(JSON.stringify([{ id: 1, name: 'Audience 1' }, { id: 2, name: 'Audience 2' }]));

    const data = await fetchAllAudiences();
    
    // Check that fetch was called with correct URL
    expect(fetchMock).toHaveBeenCalledWith('http://localhost:8443/api/Audience', expect.any(Object));
    expect(data).toEqual([{ id: 1, name: 'Audience 1' }, { id: 2, name: 'Audience 2' }]);
  });

  it('fetchAudienceByCampaignId should fetch audience by campaign ID', async () => {
    const campaignId = '123';
    fetchMock.mockResponseOnce(JSON.stringify({ campaignId, name: 'Campaign Audience' }));

    const data = await fetchAudienceByCampaignId(campaignId);
    
    expect(fetchMock).toHaveBeenCalledWith(`http://localhost:8443/api/Audience/${campaignId}`, expect.any(Object));
    expect(data).toEqual({ campaignId, name: 'Campaign Audience' });
  });

  it('fetchAllCampaigns should fetch all campaigns successfully', async () => {
    fetchMock.mockResponseOnce(JSON.stringify([{ id: 1, name: 'Campaign 1' }, { id: 2, name: 'Campaign 2' }]));

    const data = await fetchAllCampaigns();
    
    expect(fetchMock).toHaveBeenCalledWith('http://localhost:8443/api/Campaign', expect.any(Object));
    expect(data).toEqual([{ id: 1, name: 'Campaign 1' }, { id: 2, name: 'Campaign 2' }]);
  });

  it('fetchAverageFundedRate should fetch average funded rate', async () => {
    const startDate = '2024-12-01';
    const endDate = '2024-12-15';
    const queryParams = new URLSearchParams({ startDate, endDate }).toString();
    
    fetchMock.mockResponseOnce(JSON.stringify({ averageFundedRate: 0.75 }));

    const data = await fetchAverageFundedRate(startDate, endDate);
    
    expect(fetchMock).toHaveBeenCalledWith(`http://localhost:8443/api/Campaign/average-funded-rate?${queryParams}`, expect.any(Object));
    expect(data).toEqual({ averageFundedRate: 0.75 });
  });

  it('fetchAllResponses should fetch all responses successfully', async () => {
    fetchMock.mockResponseOnce(JSON.stringify([{ id: 1, status: 'Response 1' }, { id: 2, status: 'Response 2' }]));

    const data = await fetchAllResponses();
    
    expect(fetchMock).toHaveBeenCalledWith('http://localhost:8443/api/Response', expect.any(Object));
    expect(data).toEqual([{ id: 1, status: 'Response 1' }, { id: 2, status: 'Response 2' }]);
  });

  it('fetchResponseByCampaignId should fetch responses for a specific campaign', async () => {
    const campaignId = '123';
    fetchMock.mockResponseOnce(JSON.stringify([{ id: 1, status: 'Response 1' }]));

    const data = await fetchResponseByCampaignId(campaignId);
    
    expect(fetchMock).toHaveBeenCalledWith(`http://localhost:8443/api/Response/${campaignId}`, expect.any(Object));
    expect(data).toEqual([{ id: 1, status: 'Response 1' }]);
  });

  it('should throw an error if fetch fails (for any API call)', async () => {
    // Simulate a failed response
    fetchMock.mockRejectOnce(new Error('API request failed'));

    try {
      await fetchAllAudiences();
    } catch (error) {
      expect(error).toEqual(new Error('API request failed'));
    }
  });
});
