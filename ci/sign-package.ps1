$currentDirectory = split-path $MyInvocation.MyCommand.Definition

 
$path = $env:BUILD_ArtifactStagingDirectory
if([string]::IsNullOrWhiteSpace($path)){
    $path = $currentDirectory;
}

# See if we have the ClientSecret available
if([string]::IsNullOrEmpty($env:SignClientSecret)){
    Write-Host "Client Secret not found, not signing packages"
    return;
}

# Setup Variables we need to pass into the sign client tool
$appSettings = "$currentDirectory\appsettings.json"
$nupgks = Get-ChildItem $path\..\*.nupkg -Recurse | Select-Object -ExpandProperty FullName

dotnet tool install --tool-path "$currentDirectory" SignClient

foreach ($nupkg in $nupgks){
    Write-Host "Submitting $nupkg for signing"

    & "$currentDirectory\SignClient" 'sign' -c $appSettings -i $nupkg -r $env:SignClientUser -s $env:SignClientSecret -n 'ZendeskApi_V2' -d 'ZendeskApi_V2' -u 'https://github.com/Speedygeek/ZendeskApi_v2'

    Write-Host "Finished signing $nupkg"
}

Write-Host "Sign-package complete"
