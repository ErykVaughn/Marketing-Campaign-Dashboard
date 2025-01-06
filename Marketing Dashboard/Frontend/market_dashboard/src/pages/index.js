import React, { useEffect, useState } from "react";
import {fetchAllCampaigns, fetchAverageFundedRate } from "../lib/api";
import CampaignList from "../components/CampaignList"; // Adjust the path as needed
import CombinedStatistics from "../components/CombinedStatistics"; // Import the new CombinedStatistics component
import "../styles/globals.css"; // Ensure the global styles are linked

const Dashboard = () => {
    const [campaigns, setCampaigns] = useState([]);
    const [statisticsData, setStatisticsData] = useState([]); // Updated to handle multiple funded rates
    const [startDate, setStartDate] = useState("2024-12-01");
    const [endDate, setEndDate] = useState("2024-12-15");

    useEffect(() => {
        const loadData = async () => {
            try {
                const campaignsData = await fetchAllCampaigns();
                setCampaigns(campaignsData);

                const statisticsDatas = await fetchAverageFundedRate(startDate, endDate);
                console.log("Fetched Funded Rate Data:", statisticsDatas); // Inspecting API response
                setStatisticsData(statisticsDatas); // Store the returned rate in an array
            } catch (error) {
                console.error("Error in loadData:", error.message);
            }
        };

        loadData();
    }, [startDate, endDate]);

    const handleStartDateChange = (event) => setStartDate(event.target.value);
    const handleEndDateChange = (event) => setEndDate(event.target.value);

    return (
        <div
            className="page-wrapper"
            style={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                minHeight: "100vh",
                padding: "20px",
                backgroundColor: "#f9f9f9", // Optional background color for visual clarity
            }}
        >
            <div
                className="dashboard-container"
                style={{
                    width: "100%",
                    maxWidth: "1200px",
                    padding: "20px",
                    backgroundColor: "white",
                    borderRadius: "8px",
                    boxShadow: "0 4px 10px rgba(0, 0, 0, 0.1)",
                }}
            >
                <header
                    className="dashboard-header"
                    style={{ textAlign: "center", marginBottom: "20px" }}
                >
                    <h1>Marketing Dashboard</h1>
                </header>

                <section
                    className="stats"
                    style={{ marginBottom: "40px" }}
                >
                    <CombinedStatistics
                        statisticsData={statisticsData}
                        startDate={startDate}
                        endDate={endDate}
                        handleStartDateChange={handleStartDateChange}
                        handleEndDateChange={handleEndDateChange}
                    />
                </section>

                <section className="campaigns-section">
                    <CampaignList campaigns={campaigns} />
                </section>
            </div>
        </div>
    );
};

export default Dashboard;
