# Domain-Specific Detectors

These detectors enforce conventions for modular .NET architectures.

## GR-GUID — GuidNewGuidDetector

**Severity:** Warning

Detects `Guid.NewGuid()`. In frameworks with sequential GUID generators (for clustered
index performance), direct `Guid.NewGuid()` produces random GUIDs that cause index
fragmentation. Use `IGuidGenerator` instead.

```csharp
// Bad
var id = Guid.NewGuid();

// Good
public MyService(IGuidGenerator guidGenerator)
{
    var id = guidGenerator.Create();
}
```

---

## GR-SECRET — HardcodedSecretDetector

**Severity:** Error

Detects hardcoded passwords, connection strings, API keys, and tokens in string
literals. Secrets must come from configuration, environment variables, or a vault.

Patterns detected: `password=`, `pwd=`, `apikey`, `secret`, `connectionstring`,
bearer tokens, and other common secret patterns in string assignments.

---

## GR-SYNC-EF — SynchronousSaveChangesDetector

**Severity:** Warning

Detects `SaveChanges()` (synchronous). Always use `SaveChangesAsync()` in async
contexts to avoid blocking threads.

```csharp
// Bad
context.SaveChanges();

// Good
await context.SaveChangesAsync(cancellationToken);
```

---

## GR-BADREQ — TypedResultsBadRequestDetector

**Severity:** Warning

Detects `TypedResults.BadRequest<string>()`. Use `TypedResults.Problem()` (RFC 7807
Problem Details) for consistent error responses.

```csharp
// Bad
return TypedResults.BadRequest("Invalid input");

// Good
return TypedResults.Problem("Invalid input", statusCode: 400);
```

---

## GR-REGEX — NewRegexDetector

**Severity:** Warning

Detects `new Regex(...)`. Use `[GeneratedRegex]` source generator for better
performance and AOT compatibility.

```csharp
// Bad
var regex = new Regex(@"\d+", RegexOptions.Compiled);

// Good
[GeneratedRegex(@"\d+")]
private static partial Regex DigitsRegex();
```

---

## GR-SLEEP — ThreadSleepDetector

**Severity:** Warning

Detects `Thread.Sleep()` in production code. This blocks the thread pool.
Use `await Task.Delay()` or timer-based approaches.

```csharp
// Bad
Thread.Sleep(1000);

// Good
await Task.Delay(1000, cancellationToken);
```

---

## GR-CONSOLE — ConsoleWriteDetector

**Severity:** Warning

Detects `Console.Write` and `Console.WriteLine`. Use structured logging
via `ILogger` instead.

```csharp
// Bad
Console.WriteLine($"Processing order {orderId}");

// Good
logger.LogInformation("Processing order {OrderId}", orderId);
```

---

## GR-CFGAWAIT — MissingConfigureAwaitDetector

**Severity:** Info

Detects `await` expressions without `ConfigureAwait(false)` in library code.
Library code should not capture the synchronization context.

```csharp
// Bad (in library code)
var data = await GetDataAsync();

// Good
var data = await GetDataAsync().ConfigureAwait(false);
```

---

## GR-DTO — DtoSuffixDetector

**Severity:** Warning

Detects classes and records with a `*Dto` suffix. Use `*Request` for input
bodies and `*Response` for return types.

```csharp
// Bad
public record UserDto(string Name);

// Good
public record UserResponse(string Name);
public record CreateUserRequest(string Name);
```
