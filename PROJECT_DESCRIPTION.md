# GetLab - Project Handover Documentation

## 1. Project Overview
**GetLab** is a Lab Equipment Checkout System built for the CMPS202 Database Course at Cairo University.

*   **Technology Stack:** C# Windows Forms (.NET Framework) + SQL Server
*   **Architecture Pattern:** 3-Tier Architecture (Presentation → Business Logic → Data Access)
*   **Database Design:** Stored Procedures only (no inline SQL)
*   **Security:** SHA-256 password hashing with parameterized queries

### Project Structure
```
GetLab/
├── Forms/              # UI Layer (Windows Forms)
├── Controller/         # Business Logic Layer
├── Data/              # Data Access Layer (DBManager)
├── Models/            # Entity Classes (User, Equipment, Reservation, etc.)
├── Helpers/           # Utility Classes (Validation, Form Management)
└── DatabaseScripts/   # SQL Server Scripts (organized by purpose)
```

## 2. Database Connection
*   **Connection String:** Located in `App.config`
*   **Database Name:** `GetLabDB`
*   **Authentication Method:** Windows Authentication (`Trusted_Connection=True`)
*   **Server:** Local SQL Server instance

### Database Scripts Organization
The database is organized into separate files for maintainability:
*   `00_Master_Setup.sql` - Main script that executes all others in order
*   `01_Database_Schema.sql` - Tables and constraints (DDL)
*   `02_Stored_Procedures.sql` - All stored procedures (API layer)
*   `03_Sample_Data.sql` - Test data for development

## 3. Development Workflow (IMPORTANT)

### ⚠️ SQL-First Approach
**When implementing any new feature, you MUST follow this sequence:**

1.  **SQL Server Management Studio (SSMS) First:**
    *   Open SSMS and connect to your local SQL Server
    *   Create/modify the stored procedure(s) needed for the feature
    *   Test the stored procedure with sample parameters
    *   Verify the result set matches what your UI expects

2.  **Visual Studio Second:**
    *   Add the method to `Controller.cs` that calls the new stored procedure
    *   Update the UI form to call the controller method
    *   Handle the returned data appropriately

**Example:**
```
Feature: "Add Equipment Search by Location"
Step 1 (SSMS): CREATE PROCEDURE sp_SearchEquipmentByLocation @LocationID INT
Step 2 (VS):   Add Controller.SearchByLocation(int locationID) method
Step 3 (VS):   Update UI form to call the controller
```

### Why This Matters
*   The database is the **API layer** for this application
*   All business rules should be in stored procedures when possible
*   C# code should be thin—just passing parameters and displaying results
*   This prevents SQL injection and centralizes data logic

## 4. Architecture & Code Structure

### Layer 1: Data Access (`Data/DBManager.cs`)
*   **Purpose:** Manages SQL Server connections and command execution
*   **Key Methods:**
    *   `ExecuteReader(string spName, SqlParameter[] params)` → Returns `DataTable`
    *   `ExecuteScalar(string spName, SqlParameter[] params)` → Returns single value (int/string)
*   **Security Rule:** ALL queries use stored procedures with `SqlParameter` objects

### Layer 2: Business Logic (`Controller/Controller.cs`)
*   **Purpose:** Bridges UI and database; contains application logic
*   **Pattern:** Each method corresponds to a user action
*   **Examples:**
    *   `bool ReserveEquipment(string uniID, int equipID, DateTime dueDate)`
    *   `DataTable GetAvailableEquipment()`
    *   `DataTable GetMyReservations(string uniID)`

### Layer 3: Presentation (`Forms/`)
*   **Organized by User Role:**
    *   `Authentication/` - Login and user creation forms
    *   `Student/` - Equipment browsing, reservation, history
    *   `Professor/` - Bulk reservations, reports
    *   `Assistant/` - Return equipment, overdue management
*   **Base Class:** `BaseForm.cs` provides common UI utilities
*   **Routing:** Login form redirects based on `UserRole` column

## 5. Key Features Implemented

### A. Authentication System
**Two-Step Login Process:**

1.  **Step 1 - User Existence Check:**
    *   Stored Procedure: `sp_CheckUserExists`
    *   Input: `@UniversityID` (e.g., "4230175")
    *   Purpose: Verify account exists before attempting password validation

2.  **Step 2 - Password Validation:**
    *   Stored Procedure: `sp_UserLogin`
    *   Inputs: `@UniversityID`, `@PasswordHash` (SHA-256)
    *   Returns: User details if credentials match

3.  **Role-Based Routing:**
    *   After successful login, redirects to appropriate dashboard:
        *   `Student` → `Welcome_student.cs`
        *   `Teacher` → `Welcome_Professor.cs`
        *   `Admin` → `Welcome_Assistant.cs`

### B. Student Module

**Feature: Browse & Reserve Equipment**
*   **UI:** `studentsreservation.cs`
*   **Data Source:** `sp_GetAvailableEquipment` (returns grid of available items)
*   **Search:** `sp_SearchEquipment` (filter by name/model)
*   **Reservation Logic:**
    *   Stored Procedure: `sp_ReserveEquipment`
    *   **ID Conversion:** UI passes `UniversityID` (string) → SP converts to `UserID` (int)
    *   Updates Equipment status to "Borrowed"
    *   Creates reservation record with due date

**Feature: My Reservations**
*   **UI:** `MyReservations.cs`
*   **Data Source:** `sp_GetMyReservations(@UniversityID)`
*   **Shows:** Active loans, return history, overdue status

### C. Assistant/Admin Module

**Feature: Return Equipment**
*   **UI:** `ReturnItemForm.cs`
*   **Dashboard:** `sp_GetAllActiveReservations` (shows all borrowed items)
*   **Return Process:**
    *   Stored Procedure: `sp_ReturnEquipment(@EquipmentID, @Condition)`
    *   Sets `ReturnDate` and marks reservation as "Completed"
    *   Updates equipment status:
        *   `"Good"` → Status: Available
        *   `"Damaged"` → Status: Maintenance

**Feature: Overdue Report**
*   **UI:** `submitreport.cs` (Professor/Admin view)
*   **Data Source:** `sp_GetOverdueItems`
*   **Displays:** Student name, equipment, days late

### D. Database ID Architecture (CRITICAL)
**The system uses TWO types of IDs:**

*   **UniversityID (String):** User-facing login ID
    *   Examples: `"4230175"` (student), `"ADM001"` (admin), `"PROF01"` (professor)
    *   Used in: Login forms, UI displays, stored procedure inputs

*   **UserID (Int):** Internal database primary key
    *   Auto-generated by SQL Server (IDENTITY column)
    *   Used in: Foreign keys, internal table relationships

**Why This Design?**
*   University IDs have different formats (numeric for students, alphanumeric for staff)
*   Stored procedures handle the conversion: `SELECT @UserID = UserID FROM Users WHERE UniversityID = @InputID`
*   C# code never needs to know the internal UserID

## 6. Stored Procedures Reference

### Authentication
*   `sp_CheckUserExists` - Validates if University ID exists
*   `sp_UserLogin` - Authenticates user credentials

### Equipment Management
*   `sp_GetAvailableEquipment` - Lists all available equipment
*   `sp_SearchEquipment` - Searches equipment by keyword
*   `sp_ReserveEquipment` - Creates new reservation
*   `sp_ReturnEquipment` - Processes equipment return

### Reporting
*   `sp_GetMyReservations` - Gets user's reservation history
*   `sp_GetAllActiveReservations` - Admin dashboard of active loans
*   `sp_GetOverdueItems` - Lists overdue equipment

## 7. How to Add a New Feature

### Example: Adding "Equipment by Category" Filter

**Step 1 - Create Stored Procedure (SSMS):**
```sql
CREATE PROCEDURE sp_GetEquipmentByCategory
    @Category NVARCHAR(50)
AS
BEGIN
    SELECT EquipmentID, EquipmentName, ModelName, CurrentStatus
    FROM Equipment
    WHERE Category = @Category AND CurrentStatus = 'Available';
END
GO
```

**Step 2 - Add Controller Method (Controller.cs):**
```csharp
public DataTable GetEquipmentByCategory(string category)
{
    SqlParameter[] parameters = {
        new SqlParameter("@Category", category)
    };
    return dbManager.ExecuteReader("sp_GetEquipmentByCategory", parameters);
}
```

**Step 3 - Update UI Form:**
```csharp
private void btnFilterCategory_Click(object sender, EventArgs e)
{
    string selectedCategory = cmbCategory.SelectedItem.ToString();
    DataTable results = controller.GetEquipmentByCategory(selectedCategory);
    dgvEquipment.DataSource = results;
}
```

### Development Checklist
- [ ] Create/test stored procedure in SSMS
- [ ] Verify result set matches UI expectations
- [ ] Add method to `Controller.cs`
- [ ] Update UI form to call controller
- [ ] Test with real data
- [ ] Handle edge cases (empty results, SQL errors)

## 8. Pending Features

### High Priority
*   **Professor Bulk Reservation:** Allow professors to reserve multiple items for lab sessions
*   **Maintenance Workflow:** Form to view items in maintenance, mark as repaired
*   **Email Notifications:** Send reminders for due dates and overdue items

### Future Enhancements
*   **Equipment Categories:** Add filtering by type (Oscilloscope, Multimeter, etc.)
*   **Usage Statistics:** Track most borrowed items, peak usage times
*   **QR Code Integration:** Scan equipment serial numbers for quick checkout

## 9. Testing Credentials

| Role      | University ID | Password | Access Level                          |
|-----------|---------------|----------|---------------------------------------|
| Admin     | `ADM001`      | `1234`   | Return items, view reports, manage users |
| Student   | `4230175`     | `1234`   | Browse equipment, make reservations   |
| Student   | `1230256`     | `1234`   | Browse equipment, make reservations   |
| Professor | `PROF01`      | `1234`   | View reports, bulk reservations       |

**Note:** All passwords are hashed as SHA-256 in the database.

## 10. Common Troubleshooting

### Issue: "Stored procedure not found"
*   **Solution:** Make sure you ran the database setup script in SSMS
*   **Check:** `SELECT * FROM sys.procedures` to list all procedures

### Issue: "Login failed for user"
*   **Solution:** Verify Windows Authentication is enabled in SQL Server
*   **Check:** Your connection string in `App.config`

### Issue: Equipment status not updating
*   **Solution:** Check that stored procedures are using transactions correctly
*   **Debug:** Run the SP manually in SSMS with test parameters

### Issue: UniversityID not recognized
*   **Solution:** Verify the ID exists: `SELECT * FROM Users WHERE UniversityID = 'YOUR_ID'`
*   **Check:** Make sure you ran `03_Sample_Data.sql`