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
[String]$apiKey
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
nuget pack $scriptRoot\..\Src\KP.GmailApi\KP.GmailApi.csproj -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -NonInteractive -OutputDirectory $scriptRoot -Verbosity detailed

"Setting Nuget API key."
nuget setApiKey -Verbosity quiet -NonInteractive %NugetApiKey%

"Publishing to Nuget / SymbolSource."
nuget push $scriptRoot\KP.GmailApi.*.nupkg

"done."