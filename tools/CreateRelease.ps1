[CmdletBinding()]
param (
    [Parameter()]
    $VersionNumber = "3.10.4"

)

git checkout main
git pull
git tag -s "v$($VersionNumber)" -m "v$($VersionNumber)"
git push --tags