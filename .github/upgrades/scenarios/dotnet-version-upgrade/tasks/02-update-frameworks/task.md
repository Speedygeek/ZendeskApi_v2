# 02-update-frameworks: Update target frameworks across all projects

Update TargetFramework properties across all three projects:
- **ZendeskApi_v2.csproj**: Add `net8.0` to existing multi-target (keep netstandard2.1;net462;net6.0 for compatibility)
- **ZendeskApi_v2.Example.csproj**: Change from `net6.0` to `net8.0`
- **ZendeskApi_v2.Tests.csproj**: Change from `net6.0` to `net8.0`

**Done when**: All project files updated with correct target frameworks, solution restores without errors.
