/* 
 * =============================================================
 * GetLab - Stored Procedures (API Layer)
 * =============================================================
 * This file contains all stored procedures used by the application.
 * DO NOT run this file independently - use 00_Master_Setup.sql
 * 
 * PROCEDURES:
 * - Authentication: Login and user verification
 * - Equipment Browsing: Search and filter available items
 * - Reservations: Checkout and return management
 * - Reports: Overdue items and usage statistics
 * 
 * IMPORTANT: All C# code must call these procedures using SqlParameter.
 * Never write inline SQL in the application code.
 * =============================================================
 */

USE GetLabDB;
GO

-- =============================================
-- AUTHENTICATION PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_CheckUserExists
-- ---------------------------------------------
-- Checks if a University ID exists in the system
-- Used as Step 1 of login (before password validation)
PRINT 'Creating procedure: sp_CheckUserExists';
GO
CREATE PROCEDURE sp_CheckUserExists
    @UniversityID NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Users WHERE UniversityID = @UniversityID)
        SELECT 1 AS UserExists;
    ELSE
        SELECT 0 AS UserExists;
END
GO

-- ---------------------------------------------
-- sp_UserLogin
-- ---------------------------------------------
-- Validates user credentials and returns user details
-- Used as Step 2 of login (password verification)
PRINT 'Creating procedure: sp_UserLogin';
GO
CREATE PROCEDURE sp_UserLogin
    @UniversityID NVARCHAR(20),
    @PasswordHash NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        UserID, 
        UniversityID, 
        FullName, 
        UserRole 
    FROM Users 
    WHERE UniversityID = @UniversityID 
      AND PasswordHash = @PasswordHash;
END
GO

-- =============================================
-- EQUIPMENT BROWSING PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_GetAvailableEquipment
-- ---------------------------------------------
-- Returns all equipment with "Available" status
-- Used to populate the student reservation grid
PRINT 'Creating procedure: sp_GetAvailableEquipment';
GO
CREATE PROCEDURE sp_GetAvailableEquipment
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        E.EquipmentID, 
        E.EquipmentName, 
        E.ModelName, 
        L.RoomName,
        E.CurrentStatus
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE E.CurrentStatus = 'Available'
    ORDER BY E.EquipmentName, E.ModelName;
END
GO

-- ---------------------------------------------
-- sp_SearchEquipment
-- ---------------------------------------------
-- Searches equipment by name or model keyword
-- Used in student search functionality
PRINT 'Creating procedure: sp_SearchEquipment';
GO
CREATE PROCEDURE sp_SearchEquipment
    @Keyword NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        E.EquipmentID, 
        E.EquipmentName, 
        E.ModelName, 
        E.CurrentStatus, 
        L.RoomName
    FROM Equipment E
    JOIN Locations L ON E.LocationID = L.LocationID
    WHERE (E.EquipmentName LIKE '%' + @Keyword + '%' 
           OR E.ModelName LIKE '%' + @Keyword + '%')
      AND E.CurrentStatus = 'Available'
    ORDER BY E.EquipmentName;
END
GO

-- =============================================
-- RESERVATION PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_ReserveEquipment
-- ---------------------------------------------
-- Creates a new reservation and updates equipment status
-- IMPORTANT: Converts UniversityID (string) to UserID (int)
PRINT 'Creating procedure: sp_ReserveEquipment';
GO
CREATE PROCEDURE sp_ReserveEquipment
    @UniversityID NVARCHAR(20),     -- Student's login ID
    @EquipmentID INT,
    @DueDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Convert UniversityID to internal UserID
        DECLARE @InternalUserID INT;
        SELECT @InternalUserID = UserID 
        FROM Users 
        WHERE UniversityID = @UniversityID;

        -- Validate user exists
        IF @InternalUserID IS NULL
        BEGIN
            SELECT 0 AS Result, 'User not found' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Check if equipment is available
        IF EXISTS (
            SELECT 1 
            FROM Equipment 
            WHERE EquipmentID = @EquipmentID 
              AND CurrentStatus = 'Available'
        )
        BEGIN
            -- Create reservation record
            INSERT INTO EquipmentReservations (UserID, EquipmentID, DueDate, Status)
            VALUES (@InternalUserID, @EquipmentID, @DueDate, 'Active');

            -- Update equipment status
            UPDATE Equipment 
            SET CurrentStatus = 'Borrowed' 
            WHERE EquipmentID = @EquipmentID;

            SELECT 1 AS Result, 'Reservation successful' AS Message;
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            SELECT 0 AS Result, 'Equipment not available' AS Message;
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        SELECT 0 AS Result, ERROR_MESSAGE() AS Message;
        ROLLBACK TRANSACTION;
    END CATCH
END
GO

-- ---------------------------------------------
-- sp_GetMyReservations
-- ---------------------------------------------
-- Returns reservation history for a specific user
-- Shows both active and past reservations
PRINT 'Creating procedure: sp_GetMyReservations';
GO
CREATE PROCEDURE sp_GetMyReservations
    @UniversityID NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        E.EquipmentName,
        E.ModelName,
        R.ReservationDate,
        R.DueDate,
        R.ReturnDate,
        R.Status,
        CASE 
            WHEN R.Status = 'Active' AND R.DueDate < GETDATE() THEN 'OVERDUE'
            WHEN R.Status = 'Active' THEN 'BORROWED'
            ELSE R.Status
        END AS DisplayStatus
    FROM EquipmentReservations R
    JOIN Users U ON R.UserID = U.UserID
    JOIN Equipment E ON R.EquipmentID = E.EquipmentID
    WHERE U.UniversityID = @UniversityID
    ORDER BY R.ReservationDate DESC;
END
GO

-- =============================================
-- ADMIN PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_GetAllActiveReservations
-- ---------------------------------------------
-- Returns all currently borrowed equipment
-- Used in admin return equipment dashboard
PRINT 'Creating procedure: sp_GetAllActiveReservations';
GO
CREATE PROCEDURE sp_GetAllActiveReservations
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        E.EquipmentID,
        E.EquipmentName,
        E.ModelName,
        U.FullName AS StudentName,
        U.UniversityID AS StudentID,
        ER.ReservationDate,
        ER.DueDate,
        DATEDIFF(day, ER.DueDate, GETDATE()) AS DaysOverdue
    FROM EquipmentReservations ER
    JOIN Equipment E ON ER.EquipmentID = E.EquipmentID
    JOIN Users U ON ER.UserID = U.UserID
    WHERE ER.Status = 'Active'
    ORDER BY ER.DueDate ASC;
END
GO

-- ---------------------------------------------
-- sp_ReturnEquipment
-- ---------------------------------------------
-- Processes equipment return and updates status
-- Handles both good returns and damaged equipment
PRINT 'Creating procedure: sp_ReturnEquipment';
GO
CREATE PROCEDURE sp_ReturnEquipment
    @EquipmentID INT,
    @Condition NVARCHAR(20)     -- 'Good' or 'Damaged'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Find the active reservation for this equipment
        DECLARE @ResID INT;
        SELECT TOP 1 @ResID = ReservationID 
        FROM EquipmentReservations 
        WHERE EquipmentID = @EquipmentID 
          AND Status = 'Active'
        ORDER BY ReservationDate DESC;

        -- Validate reservation exists
        IF @ResID IS NULL
        BEGIN
            SELECT 0 AS Result, 'No active reservation found' AS Message;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Mark reservation as completed
        UPDATE EquipmentReservations 
        SET ReturnDate = GETDATE(), 
            Status = 'Completed' 
        WHERE ReservationID = @ResID;

        -- Update equipment status based on condition
        IF @Condition = 'Damaged'
            UPDATE Equipment 
            SET CurrentStatus = 'Maintenance' 
            WHERE EquipmentID = @EquipmentID;
        ELSE
            UPDATE Equipment 
            SET CurrentStatus = 'Available' 
            WHERE EquipmentID = @EquipmentID;

        SELECT 1 AS Result, 'Equipment returned successfully' AS Message;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Result, ERROR_MESSAGE() AS Message;
        ROLLBACK TRANSACTION;
    END CATCH
END
GO

-- =============================================
-- REPORTING PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_GetOverdueItems
-- ---------------------------------------------
-- Returns all equipment that is past its due date
-- Used in admin/professor overdue reports
PRINT 'Creating procedure: sp_GetOverdueItems';
GO
CREATE PROCEDURE sp_GetOverdueItems
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        U.FullName AS StudentName,
        U.UniversityID AS StudentID,
        U.Email,
        E.EquipmentName,
        E.ModelName,
        ER.ReservationDate,
        ER.DueDate,
        DATEDIFF(day, ER.DueDate, GETDATE()) AS DaysLate
    FROM EquipmentReservations ER
    JOIN Users U ON ER.UserID = U.UserID
    JOIN Equipment E ON ER.EquipmentID = E.EquipmentID
    WHERE ER.Status = 'Active' 
      AND ER.DueDate < GETDATE()
    ORDER BY DaysLate DESC;
END
GO

PRINT 'All stored procedures created successfully!';
PRINT '';
