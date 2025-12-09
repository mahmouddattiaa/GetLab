# =============================================
# GetLab Project Reorganization Script
# =============================================

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  GetLab Project Reorganization Script" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Get the script directory (project root)
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path

Write-Host "Project Root: $projectRoot" -ForegroundColor Yellow
Write-Host ""

# Create new folder structure
Write-Host "Creating folder structure..." -ForegroundColor Green

$folders = @(
    "Forms",
    "Forms\Authentication",
    "Forms\Student",
    "Forms\Professor",
    "Forms\Assistant",
    "Models",
 "Helpers"
)

foreach ($folder in $folders) {
    $path = Join-Path $projectRoot $folder
    if (-not (Test-Path $path)) {
    New-Item -ItemType Directory -Path $path -Force | Out-Null
        Write-Host "  ? Created: $folder" -ForegroundColor Green
    } else {
        Write-Host "  ? Already exists: $folder" -ForegroundColor Gray
    }
}

Write-Host ""
Write-Host "Moving files..." -ForegroundColor Green

# Define file moves (source -> destination)
$fileMoves = @{
    # Authentication forms
    "login.cs" = "Forms\Authentication\login.cs"
    "login.Designer.cs" = "Forms\Authentication\login.Designer.cs"
    "login.resx" = "Forms\Authentication\login.resx"
    "Create.cs" = "Forms\Authentication\Create.cs"
    "Create.Designer.cs" = "Forms\Authentication\Create.Designer.cs"
    "Create.resx" = "Forms\Authentication\Create.resx"
    
    # Student forms
    "Welcome_student.cs" = "Forms\Student\Welcome_student.cs"
    "Welcome_student.Designer.cs" = "Forms\Student\Welcome_student.Designer.cs"
    "Welcome_student.resx" = "Forms\Student\Welcome_student.resx"
    "studentsreservation.cs" = "Forms\Student\studentsreservation.cs"
    "studentsreservation.Designer.cs" = "Forms\Student\studentsreservation.Designer.cs"
    "studentsreservation.resx" = "Forms\Student\studentsreservation.resx"
    "MyReservations.cs" = "Forms\Student\MyReservations.cs"
    "MyReservations.Designer.cs" = "Forms\Student\MyReservations.Designer.cs"
    "MyReservations.resx" = "Forms\Student\MyReservations.resx"
    
    # Professor forms
    "Welcome_Professor.cs" = "Forms\Professor\Welcome_Professor.cs"
  "Welcome_Professor.Designer.cs" = "Forms\Professor\Welcome_Professor.Designer.cs"
    "Welcome_Professor.resx" = "Forms\Professor\Welcome_Professor.resx"
    "submitreport.cs" = "Forms\Professor\submitreport.cs"
    "submitreport.Designer.cs" = "Forms\Professor\submitreport.Designer.cs"
    "submitreport.resx" = "Forms\Professor\submitreport.resx"
    
    # Assistant forms
    "Welcome_Assistant.cs" = "Forms\Assistant\Welcome_Assistant.cs"
  "Welcome_Assistant.Designer.cs" = "Forms\Assistant\Welcome_Assistant.Designer.cs"
    "Welcome_Assistant.resx" = "Forms\Assistant\Welcome_Assistant.resx"
}

foreach ($move in $fileMoves.GetEnumerator()) {
    $source = Join-Path $projectRoot $move.Key
    $destination = Join-Path $projectRoot $move.Value
    
    if (Test-Path $source) {
        # Create destination directory if it doesn't exist
  $destDir = Split-Path -Parent $destination
 if (-not (Test-Path $destDir)) {
   New-Item -ItemType Directory -Path $destDir -Force | Out-Null
        }
     
        # Move the file
        Move-Item -Path $source -Destination $destination -Force
        Write-Host "  ? Moved: $($move.Key) -> $($move.Value)" -ForegroundColor Green
    } else {
        Write-Host "  ? Not found: $($move.Key)" -ForegroundColor Yellow
 }
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  ? File reorganization complete!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "IMPORTANT: Next steps in Visual Studio:" -ForegroundColor Yellow
Write-Host "  1. Reload the project (Right-click project -> Reload)" -ForegroundColor White
Write-Host "  2. The namespaces will be updated automatically by Copilot" -ForegroundColor White
Write-Host "  3. Build the project to verify everything works" -ForegroundColor White
Write-Host ""
