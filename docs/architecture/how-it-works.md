# How It Works

## Overview

RoslynLens is an MCP (Model Context Protocol) server that uses Roslyn to
provide semantic code navigation. Instead of Claude Code reading entire `.cs` files
(500-2000+ tokens each), it queries this server and receives focused results
(30-150 tokens).

## Startup sequence

```text
1. MSBuildLocator.RegisterDefaults()     — locate MSBuild SDK
2. SolutionDiscovery.FindSolutionPath()  — BFS for .sln/.slnx
3. SolutionDiscovery.BfsDiscoverAll()    — collect all solutions
4. Host starts MCP stdio transport       — stdin/stdout
5. WorkspaceInitializer (background)     — load solution
6. Tools become available                — "loading" until Ready
```

When multiple solutions are discovered at step 3, the server logs a
warning and stores the full list. The `list_solutions` and
`switch_solution` tools allow runtime switching without restart.

## Solution loading

`WorkspaceManager` wraps `MSBuildWorkspace` with:

- **Lazy compilation**: For solutions with 50+ projects, compilations are created
  on-demand instead of upfront
- **LRU cache**: Keeps the 50 most recently used compilations in memory (~250-750 MB)
- **File watcher**: `.cs` changes trigger incremental text updates;
  `.csproj` changes trigger full project reload (with 200ms debounce)

### Solution switching

`ReloadSolutionAsync` disposes the current workspace (watchers,
compilation cache, MSBuild workspace) and loads a new solution.
If the new solution fails to load, it attempts to rollback to the
previous solution. State transitions:

```text
Ready → Loading → Ready (success) or Error (both loads failed)
```

## Tool execution

Each tool class is auto-discovered via `[McpServerToolType]` and registered by
`WithToolsFromAssembly()`. When Claude Code calls a tool:

```text
Claude Code → JSON-RPC (stdin) → MCP SDK → Tool class → Roslyn API → JSON-RPC (stdout)
```

Tools access the workspace via dependency-injected `WorkspaceManager`.

## Anti-pattern detection

Detectors implement `IAntiPatternDetector`:

```csharp
public interface IAntiPatternDetector
{
    IEnumerable<AntiPatternViolation> Detect(
        SyntaxTree tree,
        SemanticModel? model,
        CancellationToken cancellationToken);
}
```

Two modes:

- **Syntax-only** (`model = null`): Fast, works without compilation. Most detectors
  use this mode (pattern matching on the syntax tree).
- **Semantic** (`model` provided): Required for type-aware analysis (e.g., checking
  if a method returns `Task`, resolving interface implementations).

## Logging

All logs go to **stderr** via `LogToStandardErrorThreshold = LogLevel.Trace`.
Stdout is reserved exclusively for JSON-RPC messages (MCP protocol requirement).
