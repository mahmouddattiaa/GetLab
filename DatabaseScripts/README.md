# GetLab Database Setup

This folder contains the complete database setup for the GetLab Lab Equipment Checkout System.

## File

**00_Master_Setup.sql** - Complete database setup script
- Drops and recreates GetLabDB database
- Creates all 9 tables with constraints
- Creates 30+ stored procedures
- Inserts sample test data
- Single file, ready to execute

## Setup Instructions

1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Open `00_Master_Setup.sql`
4. Execute the script (F5)

The script will automatically:
- Drop any existing GetLabDB database
- Create a fresh database with all tables
- Set up all stored procedures
- Insert sample data for testing

**Execution time:** ~30 seconds

## Test Credentials

All test accounts use password: `1234` (SHA-256 hashed)

| Role | University ID |
|------|--------------|
| Lab Assistant | ADM001 |
| Student | 4230175 |
| Student | 1230256 |
| Professor | PROF01 |

## Database Contents

### Tables (9)
- Users
- Equipment
- EquipmentReservations
- RoomReservations
- Locations
- Suppliers
- Courses
- MaintenanceReports
- EquipmentRequests

### Stored Procedures (30+)
Authentication, reservations, equipment management, maintenance tracking, and reporting procedures.

### Sample Data
- 4 test users (Admin, Students, Professor)
- 12 equipment items (oscilloscopes, multimeters, Arduino kits, etc.)
- 3 locations (2 labs, 1 storage room)
- 3 suppliers
- Sample courses and reservations

## Architecture

The application uses a stored procedure-only architecture:
- No inline SQL in the C# application
- All database operations through stored procedures
- SHA-256 password hashing
- Foreign key constraints for data integrity

## Troubleshooting

**Error: Database already exists**
The script handles this automatically with `DROP DATABASE IF EXISTS`.

**Connection issues**
Check your connection string in `App.config`:
```xml
<add name="GetLabConnection" 
     connectionString="Data Source=.;Initial Catalog=GetLabDB;Integrated Security=True" />
```

**Reset database**
Simply re-run `00_Master_Setup.sql` for a clean slate.

---

**Last Updated:** December 2025  
**SQL Server Version:** 2016 and later
