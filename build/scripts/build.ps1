Write-Host ""
Write-Host "Waves Build Script"
Write-Host ""

$SolutionNames = "Waves.UI.Avalonia.sln"
Set-Variable -Name "SolutionsDirectory" -Value "../../solutions/"

Foreach ($name in $SolutionNames)
{
	$SolutionPath = Join-Path -Path $SolutionsDirectory -ChildPath $name

	Write-Host "------------------------------------------"
	Write-Host "Cleaning..." -fore cyan
	dotnet clean $SolutionPath --configuration Release

	Write-Host "------------------------------------------"
	Write-Host "Restoring..." -fore cyan
	dotnet restore $SolutionPath

	Write-Host "------------------------------------------"
	Write-Host "Building..." -fore cyan
	dotnet build $SolutionPath --configuration Release
}

