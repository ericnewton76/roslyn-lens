# General .NET Detectors

These detectors flag common .NET anti-patterns. They work on any .NET codebase.

## AP001 — AsyncVoidDetector

**Severity:** Error

Detects `async void` methods. These cannot be awaited, swallow exceptions, and cause
unpredictable behavior. Use `async Task` instead.

```csharp
// Bad
public async void DoWork() { await Task.Delay(1); }

// Good
public async Task DoWork() { await Task.Delay(1); }
```

**Exception:** Event handlers (`void OnClick(object sender, EventArgs e)`) are excluded.

---

## AP002 — SyncOverAsyncDetector

**Severity:** Warning

Detects synchronous blocking on async code: `.Result`, `.Wait()`, `.GetAwaiter().GetResult()`.
These cause deadlocks in ASP.NET Core.

```csharp
// Bad
var result = GetDataAsync().Result;
GetDataAsync().Wait();
var result = GetDataAsync().GetAwaiter().GetResult();

// Good
var result = await GetDataAsync();
```

---

## AP003 — HttpClientInstantiationDetector

**Severity:** Warning

Detects `new HttpClient()`. This causes socket exhaustion under load.
Use `IHttpClientFactory` instead.

```csharp
// Bad
var client = new HttpClient();

// Good — inject via DI
public MyService(IHttpClientFactory factory)
{
    var client = factory.CreateClient();
}
```

---

## AP004 — DateTimeDirectUseDetector

**Severity:** Warning

Detects `DateTime.Now` and `DateTime.UtcNow`. These are untestable.
Inject `TimeProvider` (or `IClock`) instead.

```csharp
// Bad
var now = DateTime.UtcNow;

// Good
public MyService(TimeProvider timeProvider)
{
    var now = timeProvider.GetUtcNow();
}
```

---

## AP005 — BroadCatchDetector

**Severity:** Warning

Detects `catch (Exception)` without a re-throw. Broad catches hide bugs and
swallow critical exceptions like `OutOfMemoryException`.

```csharp
// Bad
catch (Exception ex) { logger.LogError(ex, "Failed"); }

// Good
catch (Exception ex) { logger.LogError(ex, "Failed"); throw; }

// Better — catch specific exceptions
catch (HttpRequestException ex) { ... }
```

---

## AP006 — LoggingInterpolationDetector

**Severity:** Warning

Detects string interpolation in log calls. Interpolated strings are always
allocated, even when the log level is disabled. Use structured logging templates.

```csharp
// Bad
logger.LogInformation($"User {userId} logged in");

// Good
logger.LogInformation("User {UserId} logged in", userId);
```

---

## AP007 — PragmaWithoutRestoreDetector

**Severity:** Info

Detects `#pragma warning disable` without a matching `#pragma warning restore`.
Forgotten restores suppress warnings for the rest of the file.

---

## AP008 — MissingCancellationTokenDetector

**Severity:** Info

Detects async methods that don't accept a `CancellationToken` parameter.
Without cancellation support, long-running operations cannot be stopped gracefully.

**Note:** Requires semantic model (full compilation). Skipped in syntax-only mode.

---

## AP009 — EfCoreNoTrackingDetector

**Severity:** Info

Detects EF Core queries that don't use `.AsNoTracking()`. Read-only queries
should use `AsNoTracking()` to avoid change tracker overhead.

**Note:** Requires semantic model (full compilation). Skipped in syntax-only mode.
