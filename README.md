# Marketing Dashboard Database Setup

This project sets up a MySQL database for a marketing dashboard. The database contains three tables: `Campaigns`, `Audiences`, and `Responses`, with data loaded from CSV files.

## Table of Contents

- [Overview](#overview)
- [Database Schema](#database-schema)
- [Setup Instructions](#setup-instructions)
- [How to Download MySQL Server](#how-to-download-mysql-server)
- [Using the Script](#using-the-script)
- [Troubleshooting](#troubleshooting)
- [Running the Backend](#running-the-backend)

## Overview

The database includes:

- **Campaigns**: Information about marketing campaigns.
- **Audiences**: Audience details for each campaign.
- **Responses**: Records of responses and funding statuses.

## Database Schema

The schema is as follows:

- **Campaigns**

  - `Campaign_ID`: Primary key
  - `Name`: Campaign name

- **Audiences**

  - `Campaign_ID`: Foreign key to Campaigns
  - `Record_ID`: Unique identifier for audience members
  - `Has_Phone`: Boolean indicating phone availability
  - `Has_Email`: Boolean indicating email availability

- **Responses**
  - `Campaign_ID`: Foreign key to Campaigns
  - `Record_ID`: Foreign key to Audiences
  - `Lead_Flag`: Boolean indicating a lead
  - `Lead_Timestamp`: Timestamp of the lead
  - `Funded_Flag`: Boolean indicating funding status
  - `Funded_Timestamp`: Timestamp of funding

## Setup Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repo/marketing-dashboard.git
   cd marketing-dashboard
   ```

2. Place the CSV files (`Campaigns.csv`, `Audiences.csv`, `Responses.csv`) in a directory of your choice.

3. Edit the `database_setup.sql` file:
   - Locate the `LOAD DATA INFILE` statements.
   - Replace the file paths with the absolute path to your CSV files. For example:
     ```sql
     LOAD DATA INFILE 'C:/path/to/your/Campaigns.csv' INTO TABLE Campaigns ...
     ```

## How to Download MySQL Server

1. **Download MySQL Community Server**:

   - Visit [MySQL Community Server Downloads](https://dev.mysql.com/downloads/mysql/).
   - Choose the version appropriate for your operating system and download it.

2. **Install MySQL Server**:

   - Run the installer and follow the setup wizard instructions.
   - Choose the default server configuration for general use.
   - Set a root password during the installation.

3. **Configure MySQL Server**:
   - Open the MySQL client or MySQL Workbench.
   - Enable `local_infile` to allow loading data from files:
     ```sql
     SET GLOBAL local_infile = 1;
     ```

## Using the Script

1. **Prepare the Environment**:

   - Ensure MySQL is running.
   - Open the MySQL command line client or MySQL Workbench.

2. **Run the Script**:

   - Copy text from `sql command.txt` and paste it into the SQL terminal.

3. **Verify the Data**:
   - Check that data is loaded correctly:
     ```sql
     USE marketing_dashboard;
     SELECT * FROM Campaigns;
     SELECT * FROM Audiences;
     SELECT * FROM Responses;
     ```

## Troubleshooting

### MySQL Doesn't Allow File Paths

If MySQL doesn't permit loading files from your specified location:

1. Verify the `local_infile` setting:

   ```sql
   SHOW VARIABLES LIKE 'local_infile';
   ```

   If it's disabled, enable it:

   ```sql
   SET GLOBAL local_infile = 1;
   ```

2. Move the CSV files to the MySQL `Uploads` directory:

   - On Windows: `C:/ProgramData/MySQL/MySQL Server <version>/Uploads/`
   - On Linux: `/var/lib/mysql-files/`

3. Update the file paths in the script to match the `Uploads` directory:
   ```sql
   LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Campaigns.csv' ...
   ```

### File Permissions

Ensure the MySQL server has read access to the directory containing the CSV files. On Linux, you can adjust permissions:

```bash
sudo chmod 644 /path/to/your/Campaigns.csv
sudo chown mysql:mysql /path/to/your/Campaigns.csv
```

### CSV Format Issues

If the script fails to load data:

1. Check that your CSV files use the correct delimiter (`,` by default).
2. Verify that each column matches the schema defined in the `CREATE TABLE` statements.
3. Ensure there are no blank lines or invalid rows in the CSV files.

### Other Issues

- If you encounter an error stating "File not found", ensure the file path is absolute and properly formatted for your operating system.
- Restart MySQL if configuration changes do not take effect:
  ```bash
  sudo systemctl restart mysql  # For Linux
  ```
  Or restart the MySQL Windows service via the Services Manager.

## Running the Backend

1. **Install .NET SDK**:

   - Download and install the .NET SDK from the [official .NET website](https://dotnet.microsoft.com/download).
   - Verify the installation by running:
     ```bash
     dotnet --version
     ```

2. **Navigate to the Backend Directory**:

   - Ensure you are in the project directory containing the backend code.
   - If the backend is in a subdirectory, navigate to it:
     ```bash
     cd backend
     ```

3. **Update `appsettings.json`**:

   - Locate the `appsettings.json` file in the project directory.
   - Update the `DefaultConnection` string with your database details. For example:
     ```json
     "DefaultConnection": "Server=localhost;Database=marketing_dashboard;User=root;Password=testpassword1!;"
     ```

4. **Restore Dependencies**:

   - Run the following command to restore the required NuGet packages:
     ```bash
     dotnet restore
     ```

5. **Build the Project**:

   - Build the backend to ensure there are no errors:
     ```bash
     dotnet build
     ```

6. **Run the Backend**:

   - Start the backend in development mode:
     ```bash
     dotnet run --environment Development
     ```

7. **Access the Backend**:

   - By default, the backend will run on `http://localhost:5000` or `https://localhost:5001`.
   - You can customize the port by editing the `launchSettings.json` file or passing a `--urls` argument:
     ```bash
     dotnet run --urls "http://localhost:4000"
     ```

8. **Stop the Backend**:

   - Press `Ctrl+C` to stop the backend process.

9. **Deploy to Production** (Optional):

   - To publish for production, use:
     ```bash
     dotnet publish -c Release -o ./publish
     ```
   - Deploy the contents of the `publish` directory to your server.

### Troubleshooting the Backend

- **Port Already in Use**: If you encounter an error indicating the port is in use, change the port:

  ```bash
  dotnet run --urls "http://localhost:4000"
  ```

- **Missing Dependencies**: Ensure all required dependencies are restored:

  ```bash
  dotnet restore
  ```

- **Build Errors**: Fix any errors shown during the `dotnet build` process. Check for missing files or incorrect configurations.

- **Database Connection Issues**: Ensure that the `appsettings.json` file has the correct database details and that the database is running.

- **Environment-Specific Issues**: Ensure the correct environment is set (`Development`, `Staging`, `Production`):
  ```bash
  dotnet run --environment Development
  ```
