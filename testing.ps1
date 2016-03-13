$gitVersionExe = "C:\git_pub\ZendeskApi_v2\build\GitVersion\GitVersion.exe"
cd C:\git_pub\ZendeskApi_v2\src

#& $path /l console /output buildserver /updateAssemblyInfo


$output = gitversion
$joined = $output -join "`n"
$versionInfo = $joined | ConvertFrom-Json

$versionInfo | % { foreach ($property in $_.PSObject.Properties) { 
Set-AppveyorBuildVariable -Name "GitVersion_$($property.Name)" -Value "$($_.PSObject.properties[$property.Name].Value)"
}
}

Update-AppveyorBuild -Version "$versionInfo.NuGetVersionV2" + ".build.$env:APPVEYOR_BUILD_ID"
#Write-Host $versionInfo;