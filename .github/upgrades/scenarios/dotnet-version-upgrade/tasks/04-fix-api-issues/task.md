# 04-fix-api-issues: Modernize WebRequest API calls

Replace deprecated `System.Net.WebRequest.Create()` calls with modern `HttpClient` pattern in `Core.cs`:
- Line 131: `RunRequest` method
- Line 387: `RunRequestAsync` method

Both methods use `WebRequest.Create(requestUrl) as HttpWebRequest` which is source-incompatible with .NET 8. Replace with `HttpClient`-based implementation while maintaining existing behavior (headers, proxy support, authentication).

**Done when**: No deprecated WebRequest usage remains, solution builds without warnings, behavior preserved.
