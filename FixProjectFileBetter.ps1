# =============================================
# Fix Project File - Better Version
# =============================================

$projectFile = "D:\CCEE\fall 26\DataBase\Final Project\GetLab\GetLab.csproj"

Write-Host "Reading project file..." -ForegroundColor Cyan
[xml]$xml = Get-Content $projectFile

Write-Host "Updating file references..." -ForegroundColor Yellow

# Find all ItemGroup elements
$itemGroups = $xml.Project.ItemGroup

foreach ($itemGroup in $itemGroups) {
    # Update Compile elements
    if ($itemGroup.Compile) {
    foreach ($compile in $itemGroup.Compile) {
            $include = $compile.Include

         # Authentication forms
         if ($include -eq "login.cs") { $compile.Include = "Forms\Authentication\login.cs"; Write-Host "  Updated: login.cs" }
            if ($include -eq "login.Designer.cs") { $compile.Include = "Forms\Authentication\login.Designer.cs" }
   if ($include -eq "Create.cs") { $compile.Include = "Forms\Authentication\Create.cs"; Write-Host "  Updated: Create.cs" }
     if ($include -eq "Create.Designer.cs") { $compile.Include = "Forms\Authentication\Create.Designer.cs" }
      
  # Student forms
            if ($include -eq "Welcome_student.cs") { $compile.Include = "Forms\Student\Welcome_student.cs"; Write-Host "  Updated: Welcome_student.cs" }
            if ($include -eq "Welcome_student.Designer.cs") { $compile.Include = "Forms\Student\Welcome_student.Designer.cs" }
            if ($include -eq "studentsreservation.cs") { $compile.Include = "Forms\Student\studentsreservation.cs"; Write-Host "Updated: studentsreservation.cs" }
            if ($include -eq "studentsreservation.Designer.cs") { $compile.Include = "Forms\Student\studentsreservation.Designer.cs" }
  if ($include -eq "MyReservations.cs") { $compile.Include = "Forms\Student\MyReservations.cs"; Write-Host "  Updated: MyReservations.cs" }
if ($include -eq "MyReservations.Designer.cs") { $compile.Include = "Forms\Student\MyReservations.Designer.cs" }
            
 # Professor forms
  if ($include -eq "Welcome_Professor.cs") { $compile.Include = "Forms\Professor\Welcome_Professor.cs"; Write-Host "  Updated: Welcome_Professor.cs" }
            if ($include -eq "Welcome_Professor.Designer.cs") { $compile.Include = "Forms\Professor\Welcome_Professor.Designer.cs" }
     if ($include -eq "submitreport.cs") { $compile.Include = "Forms\Professor\submitreport.cs"; Write-Host "  Updated: submitreport.cs" }
       if ($include -eq "submitreport.Designer.cs") { $compile.Include = "Forms\Professor\submitreport.Designer.cs" }
            
 # Assistant forms
            if ($include -eq "Welcome_Assistant.cs") { $compile.Include = "Forms\Assistant\Welcome_Assistant.cs"; Write-Host "  Updated: Welcome_Assistant.cs" }
          if ($include -eq "Welcome_Assistant.Designer.cs") { $compile.Include = "Forms\Assistant\Welcome_Assistant.Designer.cs" }
        }
    }
    
    # Update EmbeddedResource elements
    if ($itemGroup.EmbeddedResource) {
        foreach ($resource in $itemGroup.EmbeddedResource) {
            $include = $resource.Include
            
            # Authentication forms
            if ($include -eq "login.resx") { $resource.Include = "Forms\Authentication\login.resx" }
            if ($include -eq "Create.resx") { $resource.Include = "Forms\Authentication\Create.resx" }
            
          # Student forms
        if ($include -eq "Welcome_student.resx") { $resource.Include = "Forms\Student\Welcome_student.resx" }
    if ($include -eq "studentsreservation.resx") { $resource.Include = "Forms\Student\studentsreservation.resx" }
            if ($include -eq "MyReservations.resx") { $resource.Include = "Forms\Student\MyReservations.resx" }
   
            # Professor forms
      if ($include -eq "Welcome_Professor.resx") { $resource.Include = "Forms\Professor\Welcome_Professor.resx" }
          if ($include -eq "submitreport.resx") { $resource.Include = "Forms\Professor\submitreport.resx" }
 
         # Assistant forms
        if ($include -eq "Welcome_Assistant.resx") { $resource.Include = "Forms\Assistant\Welcome_Assistant.resx" }
        }
    }
}

Write-Host "Saving updated project file..." -ForegroundColor Yellow
$xml.Save($projectFile)
Write-Host "? Project file updated successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "CLOSE VISUAL STUDIO AND REOPEN IT NOW!" -ForegroundColor Yellow
Write-Host "================================================" -ForegroundColor Cyan
