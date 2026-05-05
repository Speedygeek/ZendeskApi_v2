# .NET 8.0 Upgrade Plan

## Overview

**Target**: Upgrade ZendeskApi_v2 solution from .NET 6/.NET Standard 2.1/.NET Framework 4.6.2 to .NET 8.0  
**Scope**: 3 projects (~22k LOC), all SDK-style

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.  
**Rationale**: 3 projects with low complexity, straightforward TFM updates and package upgrades, minimal breaking changes (2 API calls).

## Tasks

### 01-prerequisites: Validate environment and dependencies

Verify the development environment is ready for .NET 8 upgrade:
- Confirm .NET 8 SDK is installed
- Validate global.json files (if present) are compatible with .NET 8
- Identify test projects for validation later

**Done when**: .NET 8 SDK verified, no blocking global.json constraints, test projects identified.

---

### 02-update-frameworks: Update target frameworks across all projects

Update TargetFramework properties across all three projects:
- **ZendeskApi_v2.csproj**: Add `net8.0` to existing multi-target (keep netstandard2.1;net462;net6.0 for compatibility)
- **ZendeskApi_v2.Example.csproj**: Change from `net6.0` to `net8.0`
- **ZendeskApi_v2.Tests.csproj**: Change from `net6.0` to `net8.0`

**Done when**: All project files updated with correct target frameworks, solution restores without errors.

---

### 03-update-packages: Update NuGet packages

Update 4 packages to versions compatible with .NET 8:
- **Newtonsoft.Json**: 11.0.2 → 13.0.4 (addresses security vulnerability)
- **Microsoft.Extensions.Configuration.Binder**: 7.0.4 → 8.0.2
- **Microsoft.Extensions.Configuration.EnvironmentVariables**: 7.0.0 → 8.0.0
- **Microsoft.Extensions.Configuration.UserSecrets**: 7.0.0 → 8.0.1

All packages are in ZendeskApi_v2.Tests.csproj except Newtonsoft.Json (in ZendeskApi_v2.csproj).

**Done when**: All packages updated to recommended versions, solution restores successfully.

---

### 04-fix-api-issues: Modernize WebRequest API calls

Replace deprecated `System.Net.WebRequest.Create()` calls with modern `HttpClient` pattern in `Core.cs`:
- Line 131: `RunRequest` method
- Line 387: `RunRequestAsync` method

Both methods use `WebRequest.Create(requestUrl) as HttpWebRequest` which is source-incompatible with .NET 8. Replace with `HttpClient`-based implementation while maintaining existing behavior (headers, proxy support, authentication).

**Done when**: No deprecated WebRequest usage remains, solution builds without warnings, behavior preserved.

---

### 05-build-validation: Build and verify solution

Build the entire solution targeting .NET 8 and verify compilation:
- Build all configurations (Debug/Release)
- Verify multi-targeting works correctly for ZendeskApi_v2 library
- Confirm zero build errors and no new warnings

**Done when**: Solution builds successfully with 0 errors, all target frameworks compile.

---

### 06-test-validation: Run test suite

Execute the test suite to validate functional correctness:
- Run all tests in ZendeskApi_v2.Tests project
- Verify no regressions from framework or API changes
- Confirm WebRequest → HttpClient migration maintains compatibility

**Done when**: All tests pass, no regressions detected.
