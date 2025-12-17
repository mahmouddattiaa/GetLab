/*
 * =============================================================
 * HOTFIX: Add Missing RoomReservations Table
 * =============================================================
 * This script adds the missing RoomReservations table that is
 * referenced by stored procedures sp_GetRoomBusyTimes and sp_ReserveRoom
 * 
 * RUN THIS SCRIPT IN SSMS to fix the form loading issue!
 * =============================================================
 */

USE GetLabDB;
GO

-- Check if table already exists
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RoomReservations')
BEGIN
    PRINT 'Creating missing RoomReservations table...';
    
    CREATE TABLE RoomReservations (
        RoomReservationID INT PRIMARY KEY IDENTITY(1,1),
 UserID INT NOT NULL,
        LocationID INT NOT NULL,
        StartTime DATETIME NOT NULL, -- When the room booking starts
        EndTime DATETIME NOT NULL,        -- When the room booking ends
        Purpose NVARCHAR(100) NULL,         -- Purpose of the reservation
        Status NVARCHAR(20) DEFAULT 'Active' 
            CHECK (Status IN ('Active', 'Completed', 'Cancelled')),
CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_RoomRes_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
        CONSTRAINT FK_RoomRes_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
    );
    
    PRINT 'RoomReservations table created successfully!';
    PRINT 'Your application should now work correctly.';
END
ELSE
BEGIN
    PRINT 'RoomReservations table already exists. No action needed.';
END
GO
