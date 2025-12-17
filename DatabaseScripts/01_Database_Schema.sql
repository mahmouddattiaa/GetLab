/* 
 * =============================================================
 * GetLab - Database Schema (DDL)
 * =============================================================
 * This file contains all table definitions and constraints.
 * DO NOT run this file independently - use 00_Master_Setup.sql
 * 
 * TABLES:
 * - Users: Students, Professors, Admins
 * - Suppliers: Equipment vendors
 * - Locations: Labs and storage rooms
 * - Equipment: All lab equipment items
 * - EquipmentReservations: Checkout/return records
 * - RoomReservations: Room booking records
 * - MaintenanceReports: Damaged equipment tracking
 * =============================================================
 */

USE GetLabDB;
GO

-- =============================================
-- TABLE: Users
-- =============================================
-- Stores all system users with role-based access
PRINT 'Creating table: Users';
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UniversityID NVARCHAR(20) NOT NULL UNIQUE,  -- Login ID (e.g., "4230175", "ADM001")
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL, 
    PasswordHash NVARCHAR(256) NOT NULL,        -- SHA-256 hashed password
    UserRole NVARCHAR(20) NOT NULL CHECK (UserRole IN ('Student', 'Teacher', 'Admin')),
    Major NVARCHAR(50) NULL,                    -- For students only
    Department NVARCHAR(50) NULL                -- For teachers/admins
);
GO

-- =============================================
-- TABLE: Suppliers
-- =============================================
-- Equipment vendors and manufacturers
PRINT 'Creating table: Suppliers';
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(100),
    Address NVARCHAR(200)
);
GO

-- =============================================
-- TABLE: Locations
-- =============================================
-- Physical locations where equipment is stored
PRINT 'Creating table: Locations';
CREATE TABLE Locations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    RoomName NVARCHAR(50) NOT NULL,             -- e.g., "Lab 301 - Electronics"
    RoomType NVARCHAR(20) CHECK (RoomType IN ('Lab', 'Storage')),
    Capacity INT DEFAULT 30                     -- Number of students the lab can hold
);
GO

-- =============================================
-- TABLE: Equipment
-- =============================================
-- Individual equipment items with tracking information
PRINT 'Creating table: Equipment';
CREATE TABLE Equipment (
    EquipmentID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentName NVARCHAR(100) NOT NULL,       -- e.g., "Digital Oscilloscope"
    ModelName NVARCHAR(100),                    -- e.g., "TBS1052B"
    SerialNumber NVARCHAR(50) UNIQUE NOT NULL,  -- Unique identifier per physical item
    CurrentStatus NVARCHAR(20) DEFAULT 'Available' 
        CHECK (CurrentStatus IN ('Available', 'Borrowed', 'Reserved', 'Maintenance', 'Lost')),
    SupplierID INT,
    LocationID INT, 
    CONSTRAINT FK_Equipment_Supplier FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    CONSTRAINT FK_Equipment_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
);
GO

-- =============================================
-- TABLE: EquipmentReservations
-- =============================================
-- Tracks all checkout and return transactions
PRINT 'Creating table: EquipmentReservations';
CREATE TABLE EquipmentReservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    EquipmentID INT NOT NULL,
    ReservationDate DATETIME DEFAULT GETDATE(), -- When item was checked out
    DueDate DATETIME NOT NULL,                  -- When item should be returned
    ReturnDate DATETIME NULL,                   -- Actual return date (NULL if still borrowed)
    Status NVARCHAR(20) DEFAULT 'Active' 
        CHECK (Status IN ('Active', 'Completed', 'Cancelled', 'Overdue')),
    CONSTRAINT FK_Res_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Res_Equipment FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID)
);
GO

-- =============================================
-- TABLE: RoomReservations
-- =============================================
-- Tracks room/lab reservations
PRINT 'Creating table: RoomReservations';
CREATE TABLE RoomReservations (
    RoomReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    LocationID INT NOT NULL,
    StartTime DATETIME NOT NULL,     -- When the room booking starts
    EndTime DATETIME NOT NULL,           -- When the room booking ends
    Purpose NVARCHAR(100) NULL, -- Purpose of the reservation
    Status NVARCHAR(20) DEFAULT 'Active' 
    CHECK (Status IN ('Active', 'Completed', 'Cancelled')),
    CreatedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_RoomRes_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_RoomRes_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
);
GO

-- =============================================
-- TABLE: MaintenanceReports
-- =============================================
-- Tracks damaged or broken equipment
PRINT 'Creating table: MaintenanceReports';
CREATE TABLE MaintenanceReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentID INT NOT NULL,
    ReportedByUserID INT NOT NULL,
    ReportDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(500) NOT NULL,         -- What's wrong with the equipment
    IsResolved BIT DEFAULT 0,                   -- 0 = Pending, 1 = Fixed
    CONSTRAINT FK_Report_Equipment FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID),
    CONSTRAINT FK_Report_User FOREIGN KEY (ReportedByUserID) REFERENCES Users(UserID)
);
GO

PRINT 'All tables created successfully!';
PRINT '';
