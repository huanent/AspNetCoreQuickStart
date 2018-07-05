$OutputEncoding = New-Object -typename System.Text.UTF8Encoding

$fileExtension = @(".cs" , ".cshtml", ".js", ".csproj", ".sln", ".xml", ".config")

[System.Console]::WriteLine("input old company name,default value is 'MyCompany'")
$oldCompany = [System.Console]::ReadLine()
if ([System.String]::IsNullOrWhiteSpace($oldCompany)) {
    $oldCompany = "MyCompany"
}

[System.Console]::WriteLine("input old project name,default value is 'MyProject'")
$oldProject = [System.Console]::ReadLine()
if ([System.String]::IsNullOrWhiteSpace($oldProject)) {
    $oldProject = "MyProject"
}

[System.Console]::WriteLine("input new company name,default value is 'NewCompany'")
$newCompany = [System.Console]::ReadLine()
if ([System.String]::IsNullOrWhiteSpace($newCompany)) {
    $newCompany = "NewCompany"
}

[System.Console]::WriteLine("input new project name,default value is 'NewProject'")
$newProject = [System.Console]::ReadLine()
if ([System.String]::IsNullOrWhiteSpace($newProject)) {
    $newProject = "NewProject"
}

function ReNameFile ($item) {
    $name = $item.Name.Replace($oldCompany, $newCompany)
    $name = $name.Replace($oldProject, $newProject)
    if ($item.Name -eq $name) {
        continue
    }  
    Rename-Item $item.FullName -NewName $name
}

function ChangeName ($path) {
    $items = Get-ChildItem $path
    foreach ($item in $items) {
        if ($item.PSIsContainer) {
            ChangeName($item.FullName)
            ReNameFile($item)
        }
        else {
            if ($fileExtension -notcontains $item.Extension ) {
                continue
            }
            [System.Console]::WriteLine( $item.FullName)
            $content = Get-Content $item.FullName
            if ([System.String]::IsNullOrWhiteSpace($content)) {
                continue
            }

            $content = $content.Replace($oldCompany, $newCompany)
            $content = $content.Replace($oldProject, $newProject)
            $content | Out-File -Encoding utf8 $item.FullName

            ReNameFile($item)
        }
    }
}

ChangeName('./')

[System.Console]::WriteLine("input enter exit")
[System.Console]::ReadKey()