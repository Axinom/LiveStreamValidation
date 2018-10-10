[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$moduleManifestPath
)

$ErrorActionPreference = "Stop"

# The purpose of this script is to avoid having to manually increment the prerelease suffix on each build.
# We want each build to just have an auto-incrementing number (or similar) without any manual labor needed.

# We do a text search & replace here. While we could properly parse the input, we would need to text edit
# to do the modification, so let's not bother with parsing if we need to string manipulate in the end anyway.
$manifestText = Get-Content $moduleManifestPath

$searchPattern = "^\s*Prerelease = '(.+)'\s*`$"

$prereleaseTagLine = $manifestText | ? { $_ -match $searchPattern }

if (!$prereleaseTagLine) {
    # If there is no match then either the line was commented out, missing or had an empty string value.
    # This means it is not a prerelease, so nothing more we need to do.
    return
}

if ($prereleaseTagLine.Count -ne 1) {
    Write-Error "More than one line matched the prerelease version declaration."
}

# If we do make a prerelease build, we put "-pre-123456" as the suffix, with the number being the build ID.
# The build ID is auto-incrementing as long as we use the same Azure Devops account, so it's all taken care of.
$buildId = ([int]$env:BUILD_BUILDID).ToString("000000")
$prereleaseSuffix = "-pre$buildId"

Write-Host "Setting prerelease version suffix: $prereleaseSuffix"

$updatedManifestText = $manifestText -replace $searchPattern, "Prerelease = '$prereleaseSuffix'"

Set-Content $moduleManifestPath $updatedManifestText