[CmdletBinding()]
param (
    [Parameter()]
    $VersionNumber = "3.10.4"

)

git checkout master
git pull
git tag -s "v$($VersionNumber)" -m "v$($VersionNumber)"
git push --tags