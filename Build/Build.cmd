@ECHO off

IF NOT DEFINED NugetApiKey (
	ECHO Nuget API key Environment variable 'NugetApiKey' not found!
	Exit /B 1
)

ECHO Creating Nuget package(s).
nuget pack %~dp0..\Src\KP.GmailApi\KP.GmailApi.csproj -Prop Configuration=Release -Symbols -NonInteractive -OutputDirectory %~dp0 -Verbosity detailed

ECHO Setting Nuget API key.
nuget setApiKey -Verbosity quiet -NonInteractive %NugetApiKey%

ECHO Publishing to Nuget / SymbolSource.
nuget push %~dp0KP.GmailApi.*.nupkg

ECHO done.
@ECHO on