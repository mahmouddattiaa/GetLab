# GetLab - Lab Equipment Management System

## Project Description
GetLab is a Windows Forms application designed to manage laboratory equipment reservations for Cairo University. The system allows students to reserve equipment for lab sessions or take-home use, and enables lab assistants to track and manage equipment returns.

## Features
- **Student Portal**: Browse available equipment, reserve items for lab use or take-home
- **Lab Assistant Portal**: Process equipment returns, manage reservations
- **Time Slot Management**: Reserve equipment for specific hourly slots in labs
- **Equipment Tracking**: Monitor equipment status (Available, Borrowed, Maintenance)

## Technology Stack
- **Frontend**: C# Windows Forms (.NET Framework 4.8)
- **Backend**: SQL Server with Stored Procedures
- **Architecture**: 3-Tier (Presentation, Business Logic, Data Access)

## Database Setup
1. Open SQL Server Management Studio
2. Navigate to `DatabaseScripts` folder
3. Run `00_Master_Setup.sql` - this will create the entire database

The script will create:
- GetLabDB database
- All necessary tables
- Stored procedures
- Sample test data

## Test Credentials
- **Lab Assistant**: ADM001 / 1234
- **Student**: 4230175 / 1234  
- **Professor**: PROF01 / 1234

## Project Structure
```
GetLab/
├── Forms/              # UI Forms
│   ├── Authentication/
│   ├── Student/
│   ├── Assistant/
│   └── Professor/
├── Controller/         # Business Logic
├── Data/              # Database Access
├── Models/            # Data Models
└── DatabaseScripts/   # SQL Scripts
```

## Running the Project
1. Set up the database using the SQL scripts
2. Update connection string in `App.config` if needed
3. Build and run the project in Visual Studio
4. Login with test credentials

## Authors
- Mahmoud Attia
- Mariam Raafat

## Course
CMPS202 - Database Systems
Cairo University