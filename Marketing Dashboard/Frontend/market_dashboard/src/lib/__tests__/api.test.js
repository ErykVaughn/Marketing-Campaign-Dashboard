// api.test.js
import { fetchCampaigns, fetchLeadsForCampaign, fetchLeadStatistics, fetchAverageFundedRate } from '../api';

global.fetch = jest.fn();  // Mock global fetch function

describe('API Functions', () => {
    beforeEach(() => {
        fetch.mockClear();  // Reset mock before each test
    });

    test('fetchCampaigns should return data on success', async () => {
        const mockResponse = { campaigns: [{ id: 1, name: 'Campaign 1' }] };
        fetch.mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const data = await fetchCampaigns();

        expect(fetch).toHaveBeenCalledWith('http://localhost:8443/api/Campaign');
        expect(data).toEqual(mockResponse);
    });

    test('fetchCampaigns should throw an error on failure', async () => {
        fetch.mockResolvedValueOnce({ ok: false });

        try {
            await fetchCampaigns();
        } catch (error) {
            expect(error).toEqual(new Error('Failed to fetch campaigns'));
        }
    });

    test('fetchLeadsForCampaign should return data on success', async () => {
        const campaignId = 1;
        const mockResponse = { leads: [{ id: 101, name: 'Lead 1' }] };
        fetch.mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const data = await fetchLeadsForCampaign(campaignId);

        expect(fetch).toHaveBeenCalledWith(`http://localhost:8443/api/Campaign/${campaignId}/leads`);
        expect(data).toEqual(mockResponse);
    });

    test('fetchLeadsForCampaign should throw an error on failure', async () => {
        const campaignId = 1;
        fetch.mockResolvedValueOnce({ ok: false });

        try {
            await fetchLeadsForCampaign(campaignId);
        } catch (error) {
            expect(error).toEqual(new Error('Failed to fetch leads for campaign'));
        }
    });

    test('fetchLeadStatistics should return data on success', async () => {
        const mockResponse = { statistics: { totalLeads: 100, totalFunded: 50 } };
        fetch.mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const data = await fetchLeadStatistics();

        expect(fetch).toHaveBeenCalledWith('http://localhost:8443/api/Campaign/lead-rate');
        expect(data).toEqual(mockResponse);
    });

    test('fetchLeadStatistics should throw an error on failure', async () => {
        fetch.mockResolvedValueOnce({ ok: false });

        try {
            await fetchLeadStatistics();
        } catch (error) {
            expect(error).toEqual(new Error('Failed to fetch lead statistics'));
        }
    });

    test('fetchAverageFundedRate should return data on success', async () => {
        const startDate = '2023-01-01';
        const endDate = '2023-12-31';
        const mockResponse = { averageFundedRate: 0.5 };
        fetch.mockResolvedValueOnce({
            ok: true,
            json: async () => mockResponse,
        });

        const data = await fetchAverageFundedRate(startDate, endDate);

        expect(fetch).toHaveBeenCalledWith(`http://localhost:8443/api/Campaign/average-funded-rate?startDate=${startDate}&endDate=${endDate}`);
        expect(data).toEqual(mockResponse);
    });

    test('fetchAverageFundedRate should throw an error on failure', async () => {
        const startDate = '2023-01-01';
        const endDate = '2023-12-31';
        fetch.mockResolvedValueOnce({ ok: false });

        try {
            await fetchAverageFundedRate(startDate, endDate);
        } catch (error) {
            expect(error).toEqual(new Error('Failed to fetch average funded rate'));
        }
    });
});
