import React, { useEffect, useState } from "react";
import {
    fetchCampaigns,
    fetchLeadStatistics,  // Ensure this is imported
    fetchAverageFundedRate,
} from "../lib/api";
import CampaignList from '../components/CampaignList';  // Adjust the path as needed
import FundedRate from '../components/FundedRate';  // Adjust the path as needed
import LeadRate from "../components/LeadRate"; // Import the updated component
import '../styles/globals.css'; // Ensure the global styles are linked

const Dashboard = () => {
    const [campaigns, setCampaigns] = useState([]);
    const [leadStatistics, setLeadStatistics] = useState(null);
    const [fundedRates, setFundedRates] = useState([]); // Updated to handle multiple funded rates
    const [startDate, setStartDate] = useState("2024-12-01");
    const [endDate, setEndDate] = useState("2024-12-15");

    useEffect(() => {
        const loadData = async () => {
            try {
                const campaignsData = await fetchCampaigns();
                setCampaigns(campaignsData);
        
                const leadStatisticsData = await fetchLeadStatistics();
                setLeadStatistics(leadStatisticsData);
        
                const fundedRateData = await fetchAverageFundedRate(startDate, endDate);
                console.log("Fetched Funded Rate Data:", fundedRateData);  // Inspecting API response
                setFundedRates([fundedRateData]);  // Store the returned rate in an array
            } catch (error) {
                console.error("Error in loadData:", error.message);
            }
        };

        loadData();
    }, [startDate, endDate]);

    const handleStartDateChange = (event) => setStartDate(event.target.value);
    const handleEndDateChange = (event) => setEndDate(event.target.value);

    return (
        <div className="page-wrapper"> {/* Added wrapper for padding */}
            <div className="dashboard-container">
                <header className="dashboard-header">
                    <h1>Marketing Dashboard</h1>
                    <div className="stats">
                        <div>
                            <LeadRate leadStatistics={leadStatistics} /> {/* Pass lead statistics */}
                        </div>
                        <div>
                            <FundedRate
                                fundedRates={fundedRates}  // Pass multiple funded rates
                                startDate={startDate}
                                endDate={endDate}
                                handleStartDateChange={handleStartDateChange}
                                handleEndDateChange={handleEndDateChange}
                            />
                        </div>
                    </div>
                </header>

                <section className="campaigns-section">
                    <CampaignList campaigns={campaigns} />
                </section>
            </div>
        </div>
    );
};

export default Dashboard;
