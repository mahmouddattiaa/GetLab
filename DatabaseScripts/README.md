# GetLab Database Scripts

This folder contains all SQL scripts needed to set up the GetLab database.

## File Organization

| File | Purpose | Run Independently? |
|------|---------|-------------------|
| `00_Master_Setup.sql` | **Main script** - Executes all others in order | ✅ YES - Run this |
| `01_Database_Schema.sql` | Creates tables and constraints (DDL) | ❌ NO |
| `02_Stored_Procedures.sql` | Creates all stored procedures (API layer) | ❌ NO |
| `03_Sample_Data.sql` | Inserts test data for development | ❌ NO |
| `project DB.sql` | **Legacy file** - Contains everything in one file | ⚠️ Use for reference only |

## Quick Start

### Option 1: Using Master Setup Script (Recommended)

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your local SQL Server instance
3. Open `00_Master_Setup.sql`
4. Click **Execute** (F5) or press the green "Execute" button
5. Wait for all scripts to complete (~30 seconds)

The master script will automatically:
- Drop the existing database (if it exists)
- Create a fresh `GetLabDB` database
- Execute all scripts in the correct order
- Display progress messages

### Option 2: Manual Execution (For Development)

If you need to modify individual components:

```sql
-- Step 1: Create database and schema
USE master;
DROP DATABASE IF EXISTS GetLabDB;
CREATE DATABASE GetLabDB;
GO

-- Step 2: Run schema script
USE GetLabDB;
-- Paste and execute 01_Database_Schema.sql

-- Step 3: Run stored procedures
-- Paste and execute 02_Stored_Procedures.sql

-- Step 4: Insert sample data
-- Paste and execute 03_Sample_Data.sql
```

## What Gets Created

### Tables (6)
- **Users** - Students, professors, admins with hashed passwords
- **Equipment** - All lab equipment items with serial numbers
- **EquipmentReservations** - Checkout/return transaction records
- **Locations** - Physical rooms (labs and storage)
- **Suppliers** - Equipment vendors
- **MaintenanceReports** - Damage tracking

### Stored Procedures (10)
- `sp_CheckUserExists` - Validates University ID exists
- `sp_UserLogin` - Authenticates user credentials
- `sp_GetAvailableEquipment` - Lists available items
- `sp_SearchEquipment` - Searches by keyword
- `sp_ReserveEquipment` - Creates new reservation
- `sp_GetMyReservations` - User's reservation history
- `sp_GetAllActiveReservations` - Admin dashboard of active loans
- `sp_ReturnEquipment` - Processes returns
- `sp_GetOverdueItems` - Lists overdue equipment

### Sample Data
- **4 Test Users:**
  - Admin: `ADM001` / password: `1234`
  - Student: `4230175` / password: `1234`
  - Student: `1230256` / password: `1234`
  - Professor: `PROF01` / password: `1234`
- **20 Equipment Items** (oscilloscopes, multimeters, Arduino kits, etc.)
- **3 Lab Locations**
- **3 Suppliers**

## Development Workflow

### Adding a New Feature

**⚠️ CRITICAL: Always create stored procedures BEFORE writing C# code!**

#### Step 1: Create Stored Procedure (SSMS)

```sql
-- Example: Adding equipment filtering by location
USE GetLabDB;
GO

CREATE PROCEDURE sp_GetEquipmentByLocation
    @LocationID INT
AS
BEGIN
    SELECT EquipmentID, EquipmentName, ModelName, CurrentStatus
    FROM Equipment
    WHERE LocationID = @LocationID 
      AND CurrentStatus = 'Available';
END
GO

-- Test it immediately
EXEC sp_GetEquipmentByLocation @LocationID = 1;
```

#### Step 2: Add to Controller (Visual Studio)

```csharp
// Controller.cs
public DataTable GetEquipmentByLocation(int locationID)
{
    SqlParameter[] parameters = {
        new SqlParameter("@LocationID", locationID)
    };
    return dbManager.ExecuteReader("sp_GetEquipmentByLocation", parameters);
}
```

#### Step 3: Update UI Form

```csharp
// UI Form
private void LoadEquipmentByLocation(int locationID)
{
    DataTable data = controller.GetEquipmentByLocation(locationID);
    dgvEquipment.DataSource = data;
}
```

### Modifying Existing Procedures

If you need to update a stored procedure:

```sql
-- Drop the old version
DROP PROCEDURE IF EXISTS sp_ReserveEquipment;
GO

-- Recreate with new logic
CREATE PROCEDURE sp_ReserveEquipment
    -- Updated code here
GO
```

**Note:** The Visual Studio C# code doesn't need to change unless the parameters or return columns change.

## Troubleshooting

### Error: "Database 'GetLabDB' already exists"
The master script handles this automatically. If running manually:
```sql
ALTER DATABASE GetLabDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE GetLabDB;
```

### Error: "Procedure 'sp_X' already exists"
Use `DROP PROCEDURE IF EXISTS` before `CREATE PROCEDURE`, or use `ALTER PROCEDURE` instead.

### Error: "Cannot insert duplicate key"
Sample data uses specific IDs. If you've modified the database, you may need to clear it first by re-running the master setup.

### Connection Issues
Verify your connection string in `App.config`:
```xml
<connectionStrings>
  <add name="GetLabDB" 
       connectionString="Data Source=.;Initial Catalog=GetLabDB;Integrated Security=True" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

## Best Practices

### ✅ DO:
- Always run `00_Master_Setup.sql` for a clean slate
- Test stored procedures in SSMS before using them in C#
- Use `SqlParameter` for all inputs (prevents SQL injection)
- Add comments to complex stored procedures
- Keep the master script updated when adding new procedures

### ❌ DON'T:
- Write inline SQL queries in C# code
- Modify the legacy `project DB.sql` file (use individual files)
- Skip testing stored procedures directly in SSMS
- Use string concatenation for SQL parameters
- Delete the sample data if you're still developing

## Version Control

When committing database changes:
```bash
# Add only the modified script files
git add DatabaseScripts/02_Stored_Procedures.sql
git commit -m "Add sp_GetEquipmentByLocation procedure"
```

The application expects these procedures to exist, so always keep the scripts in sync with the C# code.

## Need Help?

- **Syntax Errors:** Check the Messages tab in SSMS for line numbers
- **Procedure Testing:** Use `EXEC sp_ProcedureName @Param = value` in SSMS
- **Data Verification:** Query tables directly: `SELECT * FROM Equipment`
- **Reset Everything:** Re-run `00_Master_Setup.sql`

---

**Last Updated:** December 10, 2025  
**Database Version:** 1.0  
**Compatible with:** SQL Server 2016 and later
