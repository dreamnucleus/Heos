msbuild /t:pack /p:Configuration=Release .\DreamNucleus.Heos\DreamNucleus.Heos.csproj

$latestHeos = Get-ChildItem -Path .\DreamNucleus.Heos\bin\Release\ | Sort-Object LastAccessTime -Descending | Select-Object -First 1

nuget add $latestHeos.FullName -source D:\NuGet


# msbuild /t:pack /p:Configuration=Release .\DreamNucleus.Heos.Locator\DreamNucleus.Heos.Locator.csproj

# $latestHeosLocator = Get-ChildItem -Path .\DreamNucleus.Heos.Locator\bin\Release\ | Sort-Object LastAccessTime -Descending | Select-Object -First 1

# nuget add $latestHeosLocator.FullName -source D:\NuGet