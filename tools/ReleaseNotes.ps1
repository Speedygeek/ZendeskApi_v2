Param(
  [string]$Version = "3.5.1",
  [string]$gitHubToken = "$env:GitHubToken"
)

$gitReleaseNotesPath = "$PSScriptRoot\GitReleaseNotes.exe"
Push-Location  "$PSScriptRoot\.."
& $gitReleaseNotesPath . /O ReleaseNotes.md /Vers $Version /RepoUrl https://github.com/mozts2005/ZendeskApi_v2 /RepoUsername mozts2005 /RepoT $gitHubToken /AllT /AllL /IssueTrackerUsername mozts /IssueTrackerToken $gitHubToken
Pop-Location