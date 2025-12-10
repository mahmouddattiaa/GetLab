/* 
 * =============================================================
 * GetLab - Sample Data
 * =============================================================
 * This file contains test data for development and testing.
 * DO NOT run this file independently - use 00_Master_Setup.sql
 * 
 * DATA INCLUDED:
 * - 4 Test Users (Admin, 2 Students, 1 Professor)
 * - 3 Suppliers
 * - 3 Locations (Labs and Storage)
 * - 20 Equipment Items (various statuses)
 * 
 * NOTE: All passwords are '1234' hashed with SHA-256
 * =============================================================
 */

USE GetLabDB;
GO

-- =============================================
-- USERS - Test Accounts
-- =============================================
PRINT 'Inserting test users...';
-- Password Hash: '1234' -> SHA-256 -> '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4'

INSERT INTO Users (UniversityID, FullName, Email, PasswordHash, UserRole, Major, Department) 
VALUES 
    ('ADM001', 'Mahmoud Attia (Admin)', 'admin@getlab.com', 
     '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 
     'Admin', NULL, 'IT Department'),
    
    ('4230175', 'Mahmoud Attia (Student)', 'mahmoud@student.cairo.edu', 
     '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 
     'Student', 'Computer Engineering', NULL),
    
    ('1230256', 'Mariam Raafat', 'mariam@student.cairo.edu', 
     '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 
     'Student', 'Computer Engineering', NULL),
    
    ('PROF01', 'Dr. Hisham', 'hisham@cairo.edu', 
     '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 
     'Teacher', NULL, 'Computer Engineering');
GO

PRINT 'Users inserted: 4 (1 Admin, 2 Students, 1 Professor)';

-- =============================================
-- SUPPLIERS
-- =============================================
PRINT 'Inserting suppliers...';

INSERT INTO Suppliers (SupplierName, ContactInfo, Address) 
VALUES 
    ('Tektronix Inc', 'contact@tek.com', 'Beaverton, Oregon, USA'),
    ('RadioShack Egypt', 'sales@radioshack.eg', 'Cairo, Egypt'),
    ('Future Electronics', 'support@future.com', 'Alexandria, Egypt');
GO

PRINT 'Suppliers inserted: 3';

-- =============================================
-- LOCATIONS
-- =============================================
PRINT 'Inserting locations...';

INSERT INTO Locations (RoomName, RoomType, Capacity) 
VALUES 
    ('Lab 301 - Electronics', 'Lab', 30),
    ('Lab 302 - Embedded Systems', 'Lab', 25),
    ('Storage Room A', 'Storage', NULL);
GO

PRINT 'Locations inserted: 3';

-- =============================================
-- EQUIPMENT
-- =============================================
PRINT 'Inserting equipment items...';

INSERT INTO Equipment (EquipmentName, ModelName, SerialNumber, SupplierID, LocationID, CurrentStatus) 
VALUES 
    -- Digital Oscilloscopes (4 units)
    ('Digital Oscilloscope', 'TBS1052B', 'TEK-001', 1, 1, 'Available'),
    ('Digital Oscilloscope', 'TBS1052B', 'TEK-002', 1, 1, 'Borrowed'),
    ('Digital Oscilloscope', 'TBS1052B', 'TEK-003', 1, 1, 'Available'),
    ('Digital Oscilloscope', 'TBS1052B', 'TEK-004', 1, 1, 'Maintenance'),
    
    -- Function Generators (2 units)
    ('Function Generator', 'AFG1022', 'FG-100', 1, 1, 'Available'),
    ('Function Generator', 'AFG1022', 'FG-101', 1, 1, 'Available'),
    
    -- Digital Multimeters (3 units)
    ('Digital Multimeter', 'Fluke 179', 'FL-550', 2, 1, 'Available'),
    ('Digital Multimeter', 'Fluke 179', 'FL-551', 2, 1, 'Borrowed'),
    ('Digital Multimeter', 'Fluke 179', 'FL-552', 2, 1, 'Available'),
    
    -- Soldering Stations (2 units)
    ('Soldering Station', 'Weller WE1010', 'WL-990', 2, 2, 'Available'),
    ('Soldering Station', 'Weller WE1010', 'WL-991', 2, 2, 'Available'),
    
    -- Arduino Kits (3 units)
    ('Arduino Uno Kit', 'R3', 'ARD-001', 3, 2, 'Available'),
    ('Arduino Uno Kit', 'R3', 'ARD-002', 3, 2, 'Lost'),
    ('Arduino Uno Kit', 'R3', 'ARD-003', 3, 2, 'Available'),
    
    -- Raspberry Pi (2 units)
    ('Raspberry Pi 4', 'Model B 4GB', 'RPI-777', 3, 2, 'Reserved'),
    ('Raspberry Pi 4', 'Model B 4GB', 'RPI-778', 3, 2, 'Available'),
    
    -- Logic Analyzer (1 unit)
    ('Logic Analyzer', 'Saleae 8', 'LOG-001', 1, 2, 'Available'),
    
    -- DC Power Supplies (3 units)
    ('DC Power Supply', 'KA3005D', 'PS-200', 2, 1, 'Available'),
    ('DC Power Supply', 'KA3005D', 'PS-201', 2, 1, 'Available'),
    ('DC Power Supply', 'KA3005D', 'PS-202', 2, 1, 'Maintenance');
GO

PRINT 'Equipment inserted: 20 items';
PRINT '';
PRINT '========================================';
PRINT 'Sample Data Summary:';
PRINT '========================================';
PRINT 'Users:      4 (Admin: ADM001, Students: 4230175, 1230256, Prof: PROF01)';
PRINT 'Suppliers:  3';
PRINT 'Locations:  3';
PRINT 'Equipment:  20 items';
PRINT '  - Available:   13';
PRINT '  - Borrowed:    2';
PRINT '  - Maintenance: 2';
PRINT '  - Reserved:    1';
PRINT '  - Lost:        1';
PRINT '';
PRINT 'All test data inserted successfully!';
GO
