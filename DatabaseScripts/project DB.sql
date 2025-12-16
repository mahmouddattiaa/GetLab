/* 
 * =============================================================
 * PROJECT: GetLab - Lab Equipment Checkout System
 * MASTER DATABASE SCRIPT
 * DATE: December 10, 2025
 * 
 * INSTRUCTIONS:
 * 1. Open SQL Server Management Studio (SSMS).
 * 2. Connect to your local server.
 * 3. Copy/Paste this entire script into a New Query window.
 * 4. Click 'Execute'.
 * =============================================================
 */

USE master;
GO

-- 1. DROP DATABASE IF EXISTS (Start Fresh)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GetLabDB')
BEGIN
    ALTER DATABASE GetLabDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GetLabDB;
END
GO

CREATE DATABASE GetLabDB;
GO
USE GetLabDB;
GO

-- =============================================
-- 2. TABLES (DDL)
-- =============================================

-- Table: Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UniversityID NVARCHAR(20) NOT NULL UNIQUE, -- The Login ID (e.g. "4230175")
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL, 
    PasswordHash NVARCHAR(256) NOT NULL, 
    UserRole NVARCHAR(20) NOT NULL CHECK (UserRole IN ('Student', 'Teacher', 'Admin')),
    Major NVARCHAR(50) NULL,
    Department NVARCHAR(50) NULL
);

-- Table: Suppliers
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(100),
    Address NVARCHAR(200)
);

-- Table: Locations
CREATE TABLE Locations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    RoomName NVARCHAR(50) NOT NULL, 
    RoomType NVARCHAR(20) CHECK (RoomType IN ('Lab', 'Storage')),
    Capacity INT DEFAULT 30
);

-- Table: Equipment
CREATE TABLE Equipment (
    EquipmentID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentName NVARCHAR(100) NOT NULL, 
    ModelName NVARCHAR(100),
    SerialNumber NVARCHAR(50) UNIQUE NOT NULL,
    CurrentStatus NVARCHAR(20) DEFAULT 'Available' CHECK (CurrentStatus IN ('Available', 'Borrowed', 'Reserved', 'Maintenance', 'Lost')),
    SupplierID INT,
    LocationID INT, 
    CONSTRAINT FK_Equipment_Supplier FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    CONSTRAINT FK_Equipment_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
);

-- Table: EquipmentReservations
CREATE TABLE EquipmentReservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    EquipmentID INT NOT NULL,
    ReservationDate DATETIME DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL, 
    Status NVARCHAR(20) DEFAULT 'Active' CHECK (Status IN ('Active', 'Completed', 'Cancelled', 'Overdue')),
    CONSTRAINT FK_Res_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Res_Equipment FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID)
);

-- Table: MaintenanceReports
CREATE TABLE MaintenanceReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentID INT NOT NULL,
    ReportedByUserID INT NOT NULL,
    ReportDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(500) NOT NULL,
    IsResolved BIT DEFAULT 0, 
    CONSTRAINT FK_Report_Equipment FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID),
    CONSTRAINT FK_Report_User FOREIGN KEY (ReportedByUserID) REFERENCES Users(UserID)
);

-- =============================================
-- 3. STORED PROCEDURES (The API Layer)
-- =============================================
GO

-- SP: Check if User Exists (Step 1 of Login)
CREATE PROCEDURE sp_CheckUserExists
    @UniversityID NVARCHAR(20)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE UniversityID = @UniversityID)
        SELECT 1 AS UserExists;
    ELSE
        SELECT 0 AS UserExists;
END
GO

-- SP: Validate Password (Step 2 of Login)
CREATE PROCEDURE sp_UserLogin
    @UniversityID NVARCHAR(20),
    @PasswordHash NVARCHAR(256)
AS
BEGIN
    SELECT UserID, UniversityID, FullName, UserRole 
    FROM Users 
    WHERE UniversityID = @UniversityID AND PasswordHash = @PasswordHash;
END
GO

-- SP: Get Available Equipment (For Student Grid)
CREATE PROCEDURE sp_GetAvailableEquipment
AS
BEGIN
    SELECT 
        E.EquipmentID, 
        E.EquipmentName, 
        E.ModelName, 
        L.RoomName,
        E.CurrentStatus
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE E.CurrentStatus = 'Available';
END
GO

-- SP: Search Equipment
CREATE PROCEDURE sp_SearchEquipment
    @Keyword NVARCHAR(50)
AS
BEGIN
    SELECT E.EquipmentID, E.EquipmentName, E.ModelName, E.CurrentStatus, L.RoomName
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE (E.EquipmentName LIKE '%' + @Keyword + '%' OR E.ModelName LIKE '%' + @Keyword + '%')
      AND E.CurrentStatus = 'Available';
END
GO

-- SP: Reserve Equipment (Handles String ID -> Int ID conversion)
CREATE PROCEDURE sp_ReserveEquipment
    @UniversityID NVARCHAR(20), 
    @EquipmentID INT,
    @DueDate DATETIME
AS
BEGIN
    DECLARE @InternalUserID INT;
    SELECT @InternalUserID = UserID FROM Users WHERE UniversityID = @UniversityID;

    IF @InternalUserID IS NULL
    BEGIN
        SELECT 0 AS Result; -- Fail (User not found)
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Equipment WHERE EquipmentID = @EquipmentID AND CurrentStatus = 'Available')
    BEGIN
        INSERT INTO EquipmentReservations (UserID, EquipmentID, DueDate, Status)
        VALUES (@InternalUserID, @EquipmentID, @DueDate, 'Active');

        UPDATE Equipment SET CurrentStatus = 'Borrowed' WHERE EquipmentID = @EquipmentID;
        SELECT 1 AS Result; -- Success
    END
    ELSE
    BEGIN
        SELECT 0 AS Result; -- Fail (Item taken)
    END
END
GO

-- SP: Get My Reservations (Student History)
CREATE PROCEDURE sp_GetMyReservations
    @UniversityID NVARCHAR(20)
AS
BEGIN
    SELECT 
        E.EquipmentName,
        E.ModelName,
        R.ReservationDate,
        R.DueDate,
        R.ReturnDate,
        R.Status
    FROM EquipmentReservations R
    JOIN Users U ON R.UserID = U.UserID
    JOIN Equipment E ON R.EquipmentID = E.EquipmentID
    WHERE U.UniversityID = @UniversityID
    ORDER BY R.ReservationDate DESC;
END
GO

-- SP: Get All Active Reservations (For Admin Dashboard)
CREATE PROCEDURE sp_GetAllActiveReservations
AS
BEGIN
    SELECT 
        E.EquipmentID,
        E.EquipmentName,
        U.FullName AS StudentName,
        U.UniversityID AS StudentID,
        ER.ReservationDate,
        ER.DueDate
    FROM EquipmentReservations ER
    JOIN Equipment E ON ER.EquipmentID = E.EquipmentID
    JOIN Users U ON ER.UserID = U.UserID
    WHERE ER.Status = 'Active';
END
GO

-- SP: Return Equipment (Admin Process)
CREATE PROCEDURE sp_ReturnEquipment
    @EquipmentID INT,
    @Condition NVARCHAR(20)
AS
BEGIN
    DECLARE @ResID INT;
    SELECT TOP 1 @ResID = ReservationID 
    FROM EquipmentReservations 
    WHERE EquipmentID = @EquipmentID AND Status = 'Active';

    IF @ResID IS NULL
    BEGIN
        SELECT 0 AS Result; -- Fail
        RETURN;
    END

    UPDATE EquipmentReservations 
    SET ReturnDate = GETDATE(), Status = 'Completed' 
    WHERE ReservationID = @ResID;

    IF @Condition = 'Damaged'
        UPDATE Equipment SET CurrentStatus = 'Maintenance' WHERE EquipmentID = @EquipmentID;
    ELSE
        UPDATE Equipment SET CurrentStatus = 'Available' WHERE EquipmentID = @EquipmentID;

    SELECT 1 AS Result;
END
GO

-- SP: Get Overdue Items Report
CREATE PROCEDURE sp_GetOverdueItems
AS
BEGIN
    SELECT 
        U.FullName AS StudentName,
        U.UniversityID AS StudentID,
        E.EquipmentName,
        ER.ReservationDate,
        ER.DueDate,
        DATEDIFF(day, ER.DueDate, GETDATE()) AS DaysLate
    FROM EquipmentReservations ER
    JOIN Users U ON ER.UserID = U.UserID
    JOIN Equipment E ON ER.EquipmentID = E.EquipmentID
    WHERE ER.Status = 'Active' AND ER.DueDate < GETDATE();
END
GO

USE GetLabDB;
GO

-- SP 19: Add a New Location
-- This respects your database constraint: RoomType must be 'Lab' or 'Storage'
CREATE PROCEDURE sp_AddLocation
    @RoomName NVARCHAR(50),
    @RoomType NVARCHAR(20), 
    @Capacity INT
AS
BEGIN
    -- 1. Check for duplicates (Two rooms can't have the exact same name)
    IF EXISTS (SELECT 1 FROM Locations WHERE RoomName = @RoomName)
    BEGIN
        SELECT 0 AS Result; -- Fail (Duplicate Name)
    END
    ELSE
    BEGIN
        -- 2. Insert the new room
        INSERT INTO Locations (RoomName, RoomType, Capacity)
        VALUES (@RoomName, @RoomType, @Capacity);
        
        SELECT 1 AS Result; -- Success
    END
END
GO

-- SP 20: Get All Locations (To show in the grid)
CREATE PROCEDURE sp_GetLocationsList
AS
BEGIN
    SELECT LocationID, RoomName, RoomType, Capacity 
    FROM Locations
    ORDER BY RoomType, RoomName; -- Group by type, then name
END
GO
USE GetLabDB;
GO

-- SP 21: Get All Equipment for Search Dropdown
CREATE PROCEDURE sp_GetAllEquipmentList
AS
BEGIN
    SELECT 
        EquipmentID, 
        -- Combine Name, Model, and ID into one string for easy searching
        EquipmentName + ' - ' + ISNULL(ModelName, '') + ' (ID: ' + CAST(EquipmentID AS NVARCHAR(20)) + ')' AS DisplayName
    FROM Equipment
    ORDER BY EquipmentName;
END
GO
USE GetLabDB;
GO

-- SP 23: Get Available LAB Equipment (Hourly / In-Lab)
CREATE PROCEDURE sp_GetAvailableLabEquipment
AS
BEGIN
    SELECT 
        E.EquipmentID, 
        E.EquipmentName, 
        E.ModelName, 
        L.RoomName 
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE E.CurrentStatus = 'Available' 
      AND L.RoomType = 'Lab'; -- ONLY Labs
END
GO

-- SP 24: Get Available STORAGE Equipment (Daily / Take-Home)
CREATE PROCEDURE sp_GetAvailableStorageEquipment
AS
BEGIN
    SELECT 
        E.EquipmentID, 
        E.EquipmentName, 
        E.ModelName, 
        L.RoomName 
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE E.CurrentStatus = 'Available' 
      AND L.RoomType = 'Storage'; -- ONLY Storage
END
GO
-- =============================================
-- 4. SAMPLE DATA
-- =============================================
-- Password for everyone is '1234' (Hashed)
INSERT INTO Users (UniversityID, FullName, Email, PasswordHash, UserRole, Major, Department) VALUES 
('ADM001', 'Mahmoud Attia (Admin)', 'admin@getlab.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Admin', NULL, NULL),
('4230175', 'Mahmoud Attia (Student)', 'mahmoud@student.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Student', 'Computer Eng', NULL),
('1230256', 'Mariam Raafat', 'mariam@student.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Student', 'Computer Eng', NULL),
('PROF01', 'Dr. Hisham', 'hisham@cairo.edu', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Teacher', NULL, 'Computer Eng');

INSERT INTO Suppliers (SupplierName, ContactInfo, Address) VALUES 
('Tektronix Inc', 'contact@tek.com', 'USA'),
('RadioShack Egypt', 'sales@radioshack.eg', 'Cairo, Egypt'),
('Future Electronics', 'support@future.com', 'Alexandria, Egypt');

INSERT INTO Locations (RoomName, RoomType) VALUES 
('Lab 301 - Electronics', 'Lab'),
('Lab 302 - Embedded', 'Lab'),
('Storage Room A', 'Storage');

INSERT INTO Equipment (EquipmentName, ModelName, SerialNumber, SupplierID, LocationID, CurrentStatus) VALUES 
('Digital Oscilloscope', 'TBS1052B', 'TEK-001', 1, 1, 'Available'),
('Digital Oscilloscope', 'TBS1052B', 'TEK-002', 1, 1, 'Borrowed'),
('Digital Oscilloscope', 'TBS1052B', 'TEK-003', 1, 1, 'Available'),
('Digital Oscilloscope', 'TBS1052B', 'TEK-004', 1, 1, 'Maintenance'),
('Function Generator', 'AFG1022', 'FG-100', 1, 1, 'Available'),
('Function Generator', 'AFG1022', 'FG-101', 1, 1, 'Available'),
('Digital Multimeter', 'Fluke 179', 'FL-550', 2, 1, 'Available'),
('Digital Multimeter', 'Fluke 179', 'FL-551', 2, 1, 'Borrowed'),
('Digital Multimeter', 'Fluke 179', 'FL-552', 2, 1, 'Available'),
('Soldering Station', 'Weller WE1010', 'WL-990', 2, 2, 'Available'),
('Soldering Station', 'Weller WE1010', 'WL-991', 2, 2, 'Available'),
('Arduino Uno Kit', 'R3', 'ARD-001', 3, 2, 'Available'),
('Arduino Uno Kit', 'R3', 'ARD-002', 3, 2, 'Lost'),
('Arduino Uno Kit', 'R3', 'ARD-003', 3, 2, 'Available'),
('Raspberry Pi 4', 'Model B 4GB', 'RPI-777', 3, 2, 'Reserved'),
('Raspberry Pi 4', 'Model B 4GB', 'RPI-778', 3, 2, 'Available'),
('Logic Analyzer', 'Saleae 8', 'LOG-001', 1, 2, 'Available'),
('DC Power Supply', 'KA3005D', 'PS-200', 2, 1, 'Available'),
('DC Power Supply', 'KA3005D', 'PS-201', 2, 1, 'Available'),
('DC Power Supply', 'KA3005D', 'PS-202', 2, 1, 'Maintenance');
GO