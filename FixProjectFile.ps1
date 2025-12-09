# =============================================
# Fix Visual Studio Project File
# Updates .csproj to reference new file locations
# =============================================

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Fixing Visual Studio Project File" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

$projectRoot = "D:\CCEE\fall 26\DataBase\Final Project\GetLab"
$csprojFile = Join-Path $projectRoot "GetLab.csproj"

Write-Host "Backing up project file..." -ForegroundColor Yellow
Copy-Item $csprojFile "$csprojFile.backup" -Force
Write-Host "  ? Backup created: GetLab.csproj.backup" -ForegroundColor Green
Write-Host ""

Write-Host "Reading project file..." -ForegroundColor Yellow
$content = Get-Content $csprojFile -Raw

# Update file references to new locations
$updates = @{
    # Authentication forms
    'Include="login.cs"' = 'Include="Forms\Authentication\login.cs"'
    'Include="login.Designer.cs"' = 'Include="Forms\Authentication\login.Designer.cs"'
    'Include="login.resx"' = 'Include="Forms\Authentication\login.resx"'
    'Include="Create.cs"' = 'Include="Forms\Authentication\Create.cs"'
    'Include="Create.Designer.cs"' = 'Include="Forms\Authentication\Create.Designer.cs"'
    'Include="Create.resx"' = 'Include="Forms\Authentication\Create.resx"'
    
    # Student forms
    'Include="Welcome_student.cs"' = 'Include="Forms\Student\Welcome_student.cs"'
    'Include="Welcome_student.Designer.cs"' = 'Include="Forms\Student\Welcome_student.Designer.cs"'
    'Include="Welcome_student.resx"' = 'Include="Forms\Student\Welcome_student.resx"'
    'Include="studentsreservation.cs"' = 'Include="Forms\Student\studentsreservation.cs"'
    'Include="studentsreservation.Designer.cs"' = 'Include="Forms\Student\studentsreservation.Designer.cs"'
    'Include="studentsreservation.resx"' = 'Include="Forms\Student\studentsreservation.resx"'
    'Include="MyReservations.cs"' = 'Include="Forms\Student\MyReservations.cs"'
    'Include="MyReservations.Designer.cs"' = 'Include="Forms\Student\MyReservations.Designer.cs"'
    'Include="MyReservations.resx"' = 'Include="Forms\Student\MyReservations.resx"'
    
    # Professor forms
    'Include="Welcome_Professor.cs"' = 'Include="Forms\Professor\Welcome_Professor.cs"'
    'Include="Welcome_Professor.Designer.cs"' = 'Include="Forms\Professor\Welcome_Professor.Designer.cs"'
    'Include="Welcome_Professor.resx"' = 'Include="Forms\Professor\Welcome_Professor.resx"'
    'Include="submitreport.cs"' = 'Include="Forms\Professor\submitreport.cs"'
    'Include="submitreport.Designer.cs"' = 'Include="Forms\Professor\submitreport.Designer.cs"'
    'Include="submitreport.resx"' = 'Include="Forms\Professor\submitreport.resx"'
    
    # Assistant forms
    'Include="Welcome_Assistant.cs"' = 'Include="Forms\Assistant\Welcome_Assistant.cs"'
    'Include="Welcome_Assistant.Designer.cs"' = 'Include="Forms\Assistant\Welcome_Assistant.Designer.cs"'
    'Include="Welcome_Assistant.resx"' = 'Include="Forms\Assistant\Welcome_Assistant.resx"'
}

Write-Host "Updating file references..." -ForegroundColor Yellow
foreach ($old in $updates.Keys) {
    if ($content -match [regex]::Escape($old)) {
        $content = $content -replace [regex]::Escape($old), $updates[$old]
        Write-Host "  ? Updated: $old" -ForegroundColor Green
    }
}

# Add new files that were created
$newFilesSection = @"
    <Compile Include="Forms\BaseForm.cs">
    <SubType>Form</SubType>
    </Compile>
    <Compile Include="Helpers\FormHelper.cs" />
    <Compile Include="Helpers\ValidationHelper.cs" />
    <Compile Include="Models\User.cs" />
    <Compile Include="Models\Reservation.cs" />
    <Compile Include="Models\Lab.cs" />
    <Compile Include="Models\Report.cs" />
"@

# Find the location to insert (after Controller\Controller.cs)
$insertPattern = '(<Compile Include="Controller\\Controller.cs" />)'
if ($content -match $insertPattern) {
    $content = $content -replace $insertPattern, "`$1`r`n$newFilesSection"
    Write-Host "  ? Added new files to project" -ForegroundColor Green
}

Write-Host ""
Write-Host "Saving updated project file..." -ForegroundColor Yellow
Set-Content -Path $csprojFile -Value $content -Encoding UTF8
Write-Host "  ? Project file updated successfully!" -ForegroundColor Green

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  ? Project File Fixed!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "IMPORTANT: Next steps:" -ForegroundColor Yellow
Write-Host "  1. Close ALL open files in Visual Studio" -ForegroundColor White
Write-Host "  2. Close Visual Studio completely" -ForegroundColor White
Write-Host "  3. Reopen Visual Studio" -ForegroundColor White
Write-Host "  4. Open your solution" -ForegroundColor White
Write-Host "  5. Build the project (Ctrl+Shift+B)" -ForegroundColor White
Write-Host ""
Write-Host "If you need to restore the old project file:" -ForegroundColor Cyan
Write-Host "  Copy GetLab.csproj.backup back to GetLab.csproj" -ForegroundColor White
Write-Host ""
