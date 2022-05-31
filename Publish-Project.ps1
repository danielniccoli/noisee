#Requires -Version 7.2

[CmdletBinding()]
param (
    [Parameter()]
    [ValidateSet("Debug", "Release")] 
    [String]
    $Configuration = "Debug",

    [Parameter()]
    [ArgumentCompletions("win10-x64", "win-x64")]
    [String]
    $Runtime,

    [Parameter()]
    [Switch]
    $SelfContained,

    [Parameter()]
    [bool]
    $PublishSingleFile = $true,

    [Parameter()]
    [String]
    $Version,

    [Parameter()]
    [String]
    $OutputPath,

    [Parameter()]
    [String]
    $DotnetCliPath
)

if ($DotnetCliPath) {
    Set-Alias dotnet $DotnetCliPath
}

$projectPath = Join-Path $PSScriptRoot src

dotnet publish $projectPath `
    --nologo `
    --configuration $Configuration `
($Runtime ? ("--runtime", $Runtime) : "--use-current-runtime") `
    --self-contained $SelfContained `
    --property:PublishSingleFile=$PublishSingleFile `
($SelfContained ? "--property:PublishTrimmed=True" : $null) `
($Version ? "--property:Version=$Version" : $null) `
($Runtime ? ("output", $OutputPath) : $null)
