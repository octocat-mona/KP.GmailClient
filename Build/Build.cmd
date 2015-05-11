@ECHO off

IF NOT DEFINED NugetApiKey (
	ECHO Nuget API key Environment variable 'NugetApiKey' not found!
	Exit /B 1
)

ECHO Creating Nuget package(s).
nuget pack ..\Src\KP.GmailApi\KP.GmailApi.csproj -Prop Configuration=Release -Symbols -NonInteractive

ECHO Setting Nuget API key.
nuget setApiKey -Verbosity quiet -NonInteractive %NugetApiKey%

ECHO Publishing to Nuget / SymbolSource.
nuget push KP.GmailApi.*.nupkg

ECHO done.
@ECHO on