Param(
[Parameter(Mandatory=$true)]
[ValidateNotNullOrEmpty()]
[ValidateSet('Debug','Release')]
[String]$configMode,

[Parameter(Mandatory=$true)]
[ValidateNotNullOrEmpty()]
[String]$cloneDir,

[Parameter(Mandatory=$true)]
[ValidateNotNullOrEmpty()]
[String]$nugetApiKey
)

# Abort when an error occurs
$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Path $MyInvocation.MyCommand.Path

if ($configMode -NotLike "Release")
{
    "Configuration not Release but '$configMode', skipping creating Nuget package."
    Exit
}
if (-Not (Test-Path $cloneDir))
    { Throw "Could not find clone directory '$cloneDir'" }

"Changing to directory $cloneDir"
cd $cloneDir

"Creating Nuget package(s)."
nuget pack $scriptRoot\..\Src\KP.GmailClient\KP.GmailClient.csproj -Properties "Configuration=Release;Platform=AnyCPU" -NonInteractive -OutputDirectory $scriptRoot # -Symbols

"Setting Nuget API key."
nuget setApiKey -Verbosity quiet -NonInteractive $nugetApiKey -Source "https://www.nuget.org/api/v2/package"

#"Publishing to Nuget / SymbolSource."
#nuget push $scriptRoot\KP.GmailClient.*.nupkg -Source "https://www.nuget.org/api/v2/package"

"done."
