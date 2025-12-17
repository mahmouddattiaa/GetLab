/* 
 * =============================================================
 * PROJECT: GetLab - Lab Equipment Checkout System
 * MASTER DATABASE SCRIPT - FINAL VERSION
 * DATE: December 2025
 * 
 * DESCRIPTION:
 * This script creates the entire GetLabDB database, including all
 * tables, constraints, stored procedures, and initial sample data.
 * It is designed to be run once to set up a new environment.
 * =============================================================
 */

-- PRE-SCRIPT: Drop the database if it exists to ensure a clean start
USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GetLabDB')
BEGIN
    ALTER DATABASE GetLabDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GetLabDB;
    PRINT 'Old GetLabDB dropped.';
END
GO

-- 1. DATABASE CREATION
CREATE DATABASE GetLabDB;
GO
USE GetLabDB;
GO
PRINT 'Database GetLabDB created and selected.';

-- =============================================
-- 2. TABLE DEFINITIONS (DDL)
-- =============================================

-- Users: Stores all user accounts (Students, Teachers, Admins)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UniversityID NVARCHAR(20) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    UserRole NVARCHAR(20) NOT NULL CHECK (UserRole IN ('Student', 'Teacher', 'Admin')),
    Major NVARCHAR(50) NULL,       -- For Students
    Department NVARCHAR(50) NULL   -- For Teachers
);

-- Suppliers: Stores vendor information
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(100),
    Address NVARCHAR(200)
);

-- Locations: Stores physical labs and storage rooms
CREATE TABLE Locations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    RoomName NVARCHAR(50) NOT NULL,
    RoomType NVARCHAR(20) NOT NULL CHECK (RoomType IN ('Lab', 'Storage')),
    Capacity INT DEFAULT 30,
    LabStatus NVARCHAR(50) DEFAULT 'Available' -- For Professor Module
);

-- Equipment: The core inventory table
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

-- Courses: Academic courses taught by professors
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    TeacherID INT NOT NULL,
    CONSTRAINT FK_Courses_Teacher FOREIGN KEY (TeacherID) REFERENCES Users(UserID)
);

-- EquipmentReservations: Tracks hourly and daily loans of equipment
CREATE TABLE EquipmentReservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    EquipmentID INT NOT NULL,
    ReservationDate DATETIME NOT NULL, -- Start Time for slots
    DueDate DATETIME NOT NULL,         -- End Time for slots
    ReturnDate DATETIME NULL,
    Status NVARCHAR(20) DEFAULT 'Active' CHECK (Status IN ('Active', 'Completed', 'Cancelled', 'Overdue')),
    CONSTRAINT FK_Res_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Res_Equipment FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID)
);

-- RoomReservations: Tracks professor bookings of entire labs
CREATE TABLE RoomReservations (
    RoomReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    LocationID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Purpose NVARCHAR(100),
    CONSTRAINT FK_RoomRes_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_RoomRes_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
);

-- MaintenanceReports: Tracks reported issues with equipment
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

-- EquipmentRequests: Tracks professor requests for new equipment
CREATE TABLE EquipmentRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    EquipmentCode NVARCHAR(100) NOT NULL,
    SupplierID INT NULL,
    Justification NVARCHAR(500) NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Denied')),
    RequestDate DATETIME DEFAULT GETDATE(),
    ApprovedByUserID INT NULL,
    CONSTRAINT FK_EquipmentRequests_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_EquipmentRequests_Supplier FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    CONSTRAINT FK_EquipmentRequests_Approver FOREIGN KEY (ApprovedByUserID) REFERENCES Users(UserID)
);
PRINT 'All tables created successfully.';
GO

-- =============================================
-- 3. STORED PROCEDURES
-- =============================================

-- --- AUTHENTICATION & USER MANAGEMENT ---
CREATE PROCEDURE sp_CheckUserExists @UniversityID NVARCHAR(20) AS BEGIN IF EXISTS (SELECT 1 FROM Users WHERE UniversityID = @UniversityID) SELECT 1 AS UserExists; ELSE SELECT 0 AS UserExists; END;
GO
CREATE PROCEDURE sp_UserLogin @UniversityID NVARCHAR(20), @PasswordHash NVARCHAR(256) AS BEGIN SELECT UserID, UniversityID, FullName, UserRole FROM Users WHERE UniversityID = @UniversityID AND PasswordHash = @PasswordHash; END;
GO
CREATE PROCEDURE sp_RegisterUser @UniversityID NVARCHAR(20), @FullName NVARCHAR(100), @Email NVARCHAR(100), @PasswordHash NVARCHAR(256), @UserRole NVARCHAR(20) AS BEGIN IF EXISTS (SELECT 1 FROM Users WHERE UniversityID = @UniversityID) BEGIN SELECT -1 AS Result; RETURN; END; IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email AND @Email IS NOT NULL) BEGIN SELECT -2 AS Result; RETURN; END; INSERT INTO Users (UniversityID, FullName, Email, PasswordHash, UserRole) VALUES (@UniversityID, @FullName, @Email, @PasswordHash, @UserRole); SELECT 1 AS Result; END;
GO

-- --- STUDENT RESERVATION LOGIC ---
CREATE PROCEDURE sp_GetAvailableLabEquipment AS BEGIN SELECT E.EquipmentID, E.EquipmentName, E.ModelName, L.RoomName FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID WHERE E.CurrentStatus = 'Available' AND L.RoomType = 'Lab'; END;
GO
CREATE PROCEDURE sp_GetAvailableStorageEquipment AS BEGIN SELECT E.EquipmentID, E.EquipmentName, E.ModelName, L.RoomName FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID WHERE E.CurrentStatus = 'Available' AND L.RoomType = 'Storage'; END;
GO
CREATE PROCEDURE sp_GetEquipmentBusyTimes @EquipmentID INT, @SelectedDate DATE AS BEGIN SELECT ReservationDate AS StartTime, DueDate AS EndTime FROM EquipmentReservations WHERE EquipmentID = @EquipmentID AND CAST(ReservationDate AS DATE) = @SelectedDate AND Status = 'Active' UNION SELECT RR.StartTime, RR.EndTime FROM RoomReservations RR JOIN Equipment E ON RR.LocationID = E.LocationID WHERE E.EquipmentID = @EquipmentID AND CAST(RR.StartTime AS DATE) = @SelectedDate; END;
GO
CREATE PROCEDURE sp_ReserveSlot @UniversityID NVARCHAR(20), @EquipmentID INT, @StartTime DATETIME, @EndTime DATETIME AS BEGIN DECLARE @InternalUserID INT; SELECT @InternalUserID = UserID FROM Users WHERE UniversityID = @UniversityID; IF @InternalUserID IS NOT NULL BEGIN INSERT INTO EquipmentReservations (UserID, EquipmentID, ReservationDate, DueDate, Status) VALUES (@InternalUserID, @EquipmentID, @StartTime, @EndTime, 'Active'); SELECT 1 AS Result; END ELSE BEGIN SELECT 0 AS Result; END; END;
GO
CREATE PROCEDURE sp_GetMyReservations @UniversityID NVARCHAR(20) AS BEGIN DECLARE @InternalUserID INT; SELECT @InternalUserID = UserID FROM Users WHERE UniversityID = @UniversityID; SELECT DISTINCT E.EquipmentID, E.EquipmentName + ' (' + L.RoomName + ')' AS EquipmentName, E.ModelName, ISNULL(ER.ReservationDate, RR.StartTime) AS ReservationDate, ISNULL(ER.DueDate, RR.EndTime) AS DueDate, E.CurrentStatus AS Status FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID LEFT JOIN EquipmentReservations ER ON E.EquipmentID = ER.EquipmentID AND ER.UserID = @InternalUserID AND ER.Status = 'Active' LEFT JOIN RoomReservations RR ON E.LocationID = RR.LocationID AND RR.UserID = @InternalUserID AND RR.EndTime >= GETDATE() WHERE (ER.ReservationID IS NOT NULL) OR (RR.RoomReservationID IS NOT NULL) ORDER BY ReservationDate DESC; END;
GO

-- --- PROFESSOR RESERVATION LOGIC ---
CREATE PROCEDURE sp_GetRoomBusyTimes @LocationID INT, @SelectedDate DATE AS BEGIN SELECT StartTime, EndTime FROM RoomReservations WHERE LocationID = @LocationID AND CAST(StartTime AS DATE) = @SelectedDate; END;
GO
CREATE PROCEDURE sp_ReserveRoom @UniversityID NVARCHAR(20), @LocationID INT, @StartTime DATETIME, @EndTime DATETIME, @Purpose NVARCHAR(100) AS BEGIN DECLARE @InternalUserID INT; SELECT @InternalUserID = UserID FROM Users WHERE UniversityID = @UniversityID; IF @InternalUserID IS NOT NULL BEGIN INSERT INTO RoomReservations (UserID, LocationID, StartTime, EndTime, Purpose) VALUES (@InternalUserID, @LocationID, @StartTime, @EndTime, @Purpose); SELECT 1 AS Result; END ELSE BEGIN SELECT 0 AS Result; END; END;
GO
CREATE PROCEDURE sp_GetTeacherCourses @UniversityID NVARCHAR(20) AS BEGIN SELECT C.CourseID, C.CourseName, U.FullName AS TeacherName FROM Courses C JOIN Users U ON C.TeacherID = U.UserID WHERE U.UniversityID = @UniversityID; END;
GO

-- --- ADMIN FEATURES ---
CREATE PROCEDURE sp_GetAllActiveReservations AS BEGIN SELECT E.EquipmentID, E.EquipmentName, U.FullName AS StudentName, U.UniversityID AS StudentID, ER.ReservationDate, ER.DueDate FROM EquipmentReservations ER JOIN Equipment E ON ER.EquipmentID = E.EquipmentID JOIN Users U ON ER.UserID = U.UserID WHERE ER.Status = 'Active' ORDER BY ER.DueDate ASC; END;
GO
CREATE PROCEDURE sp_ReturnEquipment @EquipmentID INT, @Condition NVARCHAR(20) AS BEGIN DECLARE @ResID INT; SELECT TOP 1 @ResID = ReservationID FROM EquipmentReservations WHERE EquipmentID = @EquipmentID AND Status = 'Active'; IF @ResID IS NULL BEGIN SELECT 0 AS Result; RETURN; END; UPDATE EquipmentReservations SET ReturnDate = GETDATE(), Status = 'Completed' WHERE ReservationID = @ResID; IF @Condition = 'Damaged' UPDATE Equipment SET CurrentStatus = 'Maintenance' WHERE EquipmentID = @EquipmentID; ELSE UPDATE Equipment SET CurrentStatus = 'Available' WHERE EquipmentID = @EquipmentID; SELECT 1 AS Result; END;
GO
CREATE PROCEDURE sp_GetOverdueItems AS BEGIN SELECT U.FullName AS StudentName, U.UniversityID AS StudentID, E.EquipmentName, ER.ReservationDate, ER.DueDate, DATEDIFF(day, ER.DueDate, GETDATE()) AS DaysLate FROM EquipmentReservations ER JOIN Users U ON ER.UserID = U.UserID JOIN Equipment E ON ER.EquipmentID = E.EquipmentID WHERE ER.Status = 'Active' AND ER.DueDate < GETDATE(); END;
GO
CREATE PROCEDURE sp_GetDamagedEquipment AS BEGIN SELECT E.EquipmentID, E.EquipmentName, E.ModelName, E.SerialNumber, L.RoomName FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID WHERE E.CurrentStatus = 'Maintenance'; END;
GO
CREATE PROCEDURE sp_FixEquipment @EquipmentID INT AS BEGIN UPDATE Equipment SET CurrentStatus = 'Available' WHERE EquipmentID = @EquipmentID; END;
GO
CREATE PROCEDURE sp_AddLocation @RoomName NVARCHAR(50), @RoomType NVARCHAR(20), @Capacity INT AS BEGIN IF EXISTS (SELECT 1 FROM Locations WHERE RoomName = @RoomName) BEGIN SELECT 0 AS Result; END ELSE BEGIN INSERT INTO Locations (RoomName, RoomType, Capacity) VALUES (@RoomName, @RoomType, @Capacity); SELECT 1 AS Result; END; END;
GO
CREATE PROCEDURE sp_AddEquipment @Name NVARCHAR(100), @Model NVARCHAR(100), @Serial NVARCHAR(50), @SupplierID INT, @LocationID INT AS BEGIN IF EXISTS (SELECT 1 FROM Equipment WHERE SerialNumber = @Serial) BEGIN SELECT 0 AS Result; END ELSE BEGIN INSERT INTO Equipment (EquipmentName, ModelName, SerialNumber, SupplierID, LocationID, CurrentStatus) VALUES (@Name, @Model, @Serial, @SupplierID, @LocationID, 'Available'); SELECT 1 AS Result; END; END;
GO
CREATE PROCEDURE sp_CreateMaintenanceReport @UniversityID NVARCHAR(20), @EquipmentID INT, @Description NVARCHAR(500) AS BEGIN DECLARE @InternalUserID INT; SELECT @InternalUserID = UserID FROM Users WHERE UniversityID = @UniversityID; IF @InternalUserID IS NOT NULL BEGIN INSERT INTO MaintenanceReports (EquipmentID, ReportedByUserID, Description) VALUES (@EquipmentID, @InternalUserID, @Description); UPDATE Equipment SET CurrentStatus = 'Maintenance' WHERE EquipmentID = @EquipmentID; SELECT 1 AS Result; END ELSE BEGIN SELECT 0 AS Result; END; END;
GO

-- --- TEAMMATE'S PROFESSOR MODULE PROCEDURES ---
CREATE PROCEDURE sp_SubmitEquipmentRequest @TeacherUniversityID NVARCHAR(20), @EquipmentName NVARCHAR(100), @Justification NVARCHAR(500) AS BEGIN DECLARE @TeacherUserID INT; SELECT @TeacherUserID = UserID FROM Users WHERE UniversityID = @TeacherUniversityID; IF @TeacherUserID IS NOT NULL BEGIN INSERT INTO EquipmentRequests (UserID, EquipmentCode, Justification, Status, RequestDate) VALUES (@TeacherUserID, @EquipmentName, @Justification, 'Pending', GETDATE()); SELECT 1; END ELSE BEGIN SELECT 0; END; END;
GO
CREATE PROCEDURE sp_GetTeacherEquipmentRequests @TeacherUniversityID NVARCHAR(20) AS BEGIN SELECT er.RequestID, er.EquipmentCode, er.Justification, er.Status, er.RequestDate FROM EquipmentRequests er JOIN Users u ON er.UserID = u.UserID WHERE u.UniversityID = @TeacherUniversityID; END;
GO
CREATE PROCEDURE sp_GetPendingRequests AS BEGIN SELECT ER.RequestID, U.FullName AS ProfessorName, ER.EquipmentCode AS ItemName, ER.Justification, ER.RequestDate, ER.Status FROM EquipmentRequests ER JOIN Users U ON ER.UserID = U.UserID WHERE ER.Status = 'Pending' ORDER BY ER.RequestDate; END;
GO
CREATE PROCEDURE sp_UpdateRequestStatus @RequestID INT, @Status NVARCHAR(20), @AdminID INT AS BEGIN UPDATE EquipmentRequests SET Status = @Status, ApprovedByUserID = @AdminID WHERE RequestID = @RequestID; END;
GO
CREATE PROCEDURE sp_GetRoomNameByType AS BEGIN SELECT RoomName, LocationID FROM Locations WHERE RoomType = 'Lab' AND LabStatus = 'Available'; END;
GO
CREATE PROCEDURE sp_GetAvailableEquipmentByLab @LocationID INT AS BEGIN SELECT E.EquipmentID, E.EquipmentName, E.ModelName, E.SerialNumber, E.CurrentStatus, L.RoomName FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID WHERE L.LocationID = @LocationID AND E.CurrentStatus = 'Available'; END;
GO
CREATE PROCEDURE sp_GetRoomNameByStatus @LabStatus NVARCHAR(50) AS BEGIN SELECT RoomName, LocationID, LabStatus FROM Locations WHERE RoomType = 'Lab' AND LabStatus = @LabStatus; END;
GO

-- --- GENERAL HELPER PROCEDURES ---
CREATE PROCEDURE sp_SearchEquipment @Keyword NVARCHAR(50) AS BEGIN SELECT E.EquipmentID, E.EquipmentName, E.ModelName, E.CurrentStatus, L.RoomName FROM Equipment E JOIN Locations L ON E.LocationID = L.LocationID WHERE (E.EquipmentName LIKE '%' + @Keyword + '%' OR E.ModelName LIKE '%' + @Keyword + '%'); END;
GO
CREATE PROCEDURE sp_GetAllSuppliers AS SELECT SupplierID, SupplierName FROM Suppliers;
GO
CREATE PROCEDURE sp_GetAllLocations AS SELECT LocationID, RoomName FROM Locations;
GO
CREATE PROCEDURE sp_GetLocationsList AS SELECT LocationID, RoomName, RoomType, Capacity FROM Locations ORDER BY RoomType, RoomName;
GO
CREATE PROCEDURE sp_GetAllEquipmentList AS BEGIN SELECT EquipmentID, EquipmentName + ' - ' + ISNULL(ModelName, '') + ' (ID: ' + CAST(EquipmentID AS NVARCHAR(20)) + ')' AS DisplayName FROM Equipment ORDER BY EquipmentName; END;
GO
USE GetLabDB;
GO

-- BAR CHART PROCEDURES
CREATE OR ALTER PROCEDURE sp_GetMostReservedEquipment AS BEGIN SELECT TOP 10 E.EquipmentName, COUNT(ER.ReservationID) AS ReservationCoun FROM EquipmentReservations ER JOIN Equipment E ON ER.EquipmentID = E.EquipmentID  GROUP BY E.EquipmentName  ORDER BY ReservationCount DESC; END GO

CREATE OR ALTER PROCEDURE sp_GetEquipmentStatusCount AS BEGIN SELECT     CurrentStatus,  COUNT(EquipmentID) AS StatusCount   FROM Equipment GROUP BY CurrentStatus; END GO
PRINT 'All stored procedures created successfully.';

-- =============================================
-- 4. SAMPLE DATA (DML)
-- =============================================
-- Password for ALL users is '1234'
INSERT INTO Users (UniversityID, FullName, Email, PasswordHash, UserRole) VALUES 
('ADM001', 'Mahmoud Attia (Admin)', 'admin@getlab.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Admin'),
('4230175', 'Mahmoud Attia (Student)', 'mahmoud@student.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Student'),
('1230256', 'Mariam Raafat', 'mariam@student.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Student'),
('PROF01', 'Dr. Hisham', 'hisham@cairo.edu', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Teacher');

INSERT INTO Suppliers (SupplierName, ContactInfo) VALUES 
('Tektronix Inc', 'contact@tek.com'), ('RadioShack Egypt', 'sales@radioshack.eg'), ('Future Electronics', 'support@future.com');

INSERT INTO Locations (RoomName, RoomType, Capacity) VALUES 
('Lab 301 - Electronics', 'Lab', 30), ('Lab 302 - Embedded', 'Lab', 25), ('Main Storage', 'Storage', 100);

DECLARE @StorageID INT; SELECT @StorageID = LocationID FROM Locations WHERE RoomType = 'Storage';
INSERT INTO Equipment (EquipmentName, ModelName, SerialNumber, SupplierID, LocationID, CurrentStatus) VALUES 
('Digital Oscilloscope', 'TBS1052B', 'TEK-001', 1, 1, 'Available'), ('Digital Oscilloscope', 'TBS1052B', 'TEK-002', 1, 1, 'Available'),
('Function Generator', 'AFG1022', 'FG-100', 1, 1, 'Available'), ('Digital Multimeter', 'Fluke 179', 'FL-550', 2, 1, 'Available'),
('Soldering Station', 'Weller WE1010', 'WL-990', 2, 2, 'Available'), ('Arduino Uno Kit', 'R3', 'ARD-001', 3, 2, 'Available'),
('Raspberry Pi 4', 'Model B 4GB', 'RPI-777', 3, 2, 'Available'), ('DC Power Supply', 'KA3005D', 'PS-200', 2, 1, 'Available'),
('Arduino Mega Kit', 'R3 Ultimate', 'ARD-MEGA-001', 3, @StorageID, 'Available'), ('Raspberry Pi 5', '8GB RAM', 'RPI-5-001', 3, @StorageID, 'Available'),
('Portable Multimeter', 'Fluke 101', 'FL-PORT-001', 2, @StorageID, 'Available'), ('FPGA Development Board', 'DE10-Nano', 'FPGA-001', 1, @StorageID, 'Available');

DECLARE @TeacherID INT; SELECT @TeacherID = UserID FROM Users WHERE UniversityID = 'PROF01';
IF @TeacherID IS NOT NULL
BEGIN
    INSERT INTO Courses (CourseName, TeacherID) VALUES ('Microprocessors', @TeacherID), ('Electronics 101', @TeacherID);
END

-- Add some dummy reservations to make reports look good
DECLARE @StudentID INT; SELECT @StudentID = UserID FROM Users WHERE UniversityID = '4230175';
IF @StudentID IS NOT NULL
BEGIN
    INSERT INTO EquipmentReservations (UserID, EquipmentID, ReservationDate, DueDate, Status) VALUES (@StudentID, 1, GETDATE(), DATEADD(day, -2, GETDATE()), 'Active'); -- Overdue
    INSERT INTO RoomReservations (UserID, LocationID, StartTime, EndTime, Purpose) VALUES (@TeacherID, 1, DATEADD(hour, 14, GETDATE()), DATEADD(hour, 16, GETDATE()), 'Lab Exam'); -- Room Booking
END

PRINT 'Sample data inserted successfully.';
PRINT '--- SCRIPT COMPLETE ---';
GO