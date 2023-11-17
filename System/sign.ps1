param(
	[parameter]
	[switch]$dryrun = $false
)

remove-item -path .\signed\*.*    -Force
remove-item -path .\artifacts\*.* -Force

new-item -Path artifacts -ItemType Directory -Force | out-null
new-item -Path signed    -ItemType Directory -Force | out-null

dotnet --version
dotnet clean   .\src\ -c Release
dotnet restore .\src\
dotnet build   .\src\ -c Release
Get-ChildItem .\src\ -Recurse Wangkanai.*.dll | where { $_.Name -like "*release*" } | foreach {
    signtool sign /fd SHA256 /tr http://ts.ssl.com /td sha256 /n "Sarin Na Wangkanai" $_.FullName
}

dotnet pack .\src\ -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg

dotnet nuget sign .\artifacts\*.nupkg -v diag --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed
dotnet nuget sign .\artifacts\*.snupkg -v diag --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed

if ($dryrun -eq $false)
{
	dotnet nuget push .\signed\*.nupkg -k $env:NUGET_API_KEY  -s https://api.nuget.org/v3/index.json --skip-duplicate
	dotnet nuget push .\signed\*.nupkg -k $env:GITHUB_API_PAT -s https://nuget.pkg.github.com/wangkanai/index.json --skip-duplicate
}
