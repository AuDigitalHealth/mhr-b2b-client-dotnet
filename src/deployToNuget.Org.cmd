del *.nupkg

msbuild PCEHR.sln /p:Configuration=Release

REM Use dotnet for packaging now
REM NuGet.exe pack PCEHR/PCEHR.csproj -Properties Configuration=Release
dotnet pack .\PCEHR\PCEHR.csproj -c Release -o .

forfiles /m *.nupkg /c "cmd /c NuGet.exe push @FILE -Source https://www.nuget.org/api/v2/package"
