# 03-update-packages: Update NuGet packages

Update 4 packages to versions compatible with .NET 8:
- **Newtonsoft.Json**: 11.0.2 → 13.0.4 (addresses security vulnerability)
- **Microsoft.Extensions.Configuration.Binder**: 7.0.4 → 8.0.2
- **Microsoft.Extensions.Configuration.EnvironmentVariables**: 7.0.0 → 8.0.0
- **Microsoft.Extensions.Configuration.UserSecrets**: 7.0.0 → 8.0.1

All packages are in ZendeskApi_v2.Tests.csproj except Newtonsoft.Json (in ZendeskApi_v2.csproj).

**Done when**: All packages updated to recommended versions, solution restores successfully.
