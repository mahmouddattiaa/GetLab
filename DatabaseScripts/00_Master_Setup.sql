/* 
 * =============================================================
 * GetLab - Master Database Setup Script
 * =============================================================
 * This script executes all database scripts in the correct order.
 * 
 * INSTRUCTIONS:
 * 1. Open SQL Server Management Studio (SSMS)
 * 2. Connect to your local SQL Server instance
 * 3. Open this file and click 'Execute' (F5)
 * 4. All scripts will run automatically
 * 
 * EXECUTION ORDER:
 * 1. Database Creation & Schema (Tables, Constraints)
 * 2. Stored Procedures (API Layer)
 * 3. Sample Data (Test Users & Equipment)
 * 
 * DATE: December 10, 2025
 * =============================================================
 */

USE master;
GO

-- Drop existing database if it exists (clean slate)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GetLabDB')
BEGIN
    PRINT 'Dropping existing GetLabDB database...';
    ALTER DATABASE GetLabDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GetLabDB;
    PRINT 'Database dropped successfully.';
END
GO

-- Create new database
PRINT 'Creating GetLabDB database...';
CREATE DATABASE GetLabDB;
GO

USE GetLabDB;
GO
PRINT 'Switched to GetLabDB context.';
PRINT '';

-- =============================================
-- STEP 1: Create Database Schema
-- =============================================
PRINT '========================================';
PRINT 'STEP 1: Creating Database Schema';
PRINT '========================================';
GO

:r "01_Database_Schema.sql"
GO

-- =============================================
-- STEP 2: Create Stored Procedures
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'STEP 2: Creating Stored Procedures';
PRINT '========================================';
GO

:r "02_Stored_Procedures.sql"
GO

-- =============================================
-- STEP 3: Insert Sample Data
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'STEP 3: Inserting Sample Data';
PRINT '========================================';
GO

:r "03_Sample_Data.sql"
GO

-- =============================================
-- SETUP COMPLETE
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'DATABASE SETUP COMPLETE!';
PRINT '========================================';
PRINT 'Database Name: GetLabDB';
PRINT 'Tables Created: 6';
PRINT 'Stored Procedures: 10';
PRINT 'Sample Users: 4';
PRINT 'Sample Equipment: 20 items';
PRINT '';
PRINT 'Test Credentials:';
PRINT '  Admin:     ADM001  / 1234';
PRINT '  Student:   4230175 / 1234';
PRINT '  Student:   1230256 / 1234';
PRINT '  Professor: PROF01  / 1234';
PRINT '';
PRINT 'You can now run the GetLab application!';
PRINT '========================================';
GO
