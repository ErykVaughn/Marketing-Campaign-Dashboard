import React from 'react';
import '../styles/globals.css'; // Ensure the global styles are linked

export default function FundedRate({ fundedRates, startDate, endDate, handleStartDateChange, handleEndDateChange }) {
    console.log("FundedRate props:", { fundedRates });  // Debugging
  
    return (
      <section className="funded-rate-container">
        <h2>Percentage of Leads That Get Funding</h2>
        <div className="date-selector">
          <label>
            Start Date:
            <input
              type="date"
              value={startDate}
              onChange={handleStartDateChange}
            />
          </label>
          <label>
            End Date:
            <input
              type="date"
              value={endDate}
              onChange={handleEndDateChange}
            />
          </label>
        </div>
        <div className="stat-item">
          <h3>Average Funded Rates</h3>
          {fundedRates && fundedRates.length > 0 ? (
            fundedRates.map((rate, index) => (
              <div key={index} className="funded-rate-item">
                <p className="highlight">{(rate * 100).toFixed(2)}%</p>  {/* Convert to percentage */}
              </div>
            ))
          ) : (
            <p>Loading...</p>
          )}
        </div>
      </section>
    );
  }
  