import '../styles/globals.css';
import React from 'react';

export default function Home() {
  return (
    <div className="home-container">
      <h1>Welcome to the Campaign Management App</h1>
      <p>Manage campaigns, leads, and more with ease!</p>
      <a href="/dashboard">
        <button className="primary-button">Go to Dashboard</button>
      </a>
    </div>
  );
}
