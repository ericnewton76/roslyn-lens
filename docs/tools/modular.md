# Modular Architecture Tools

These tools are designed for modular .NET architectures that use `[DependsOn]`
attributes for module dependency management.

Built with [Granit](https://granit-fx.dev) in mind but works with any
`[DependsOn]`-based module system.

## get_module_depends_on

Traverse the `[DependsOn(typeof(...))]` attribute graph to show module dependencies.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `moduleName` | string | yes | Module class name (e.g., `SecurityModule`) |
| `depth` | int | no | Max traversal depth (default: 3) |
| `direction` | string | no | `dependsOn` (what it needs) or `dependedBy` (what needs it) |

**Example prompt:** "Show what PersistenceModule depends on"

**Returns:** Tree of module dependencies with depth indicators.

---

## validate_conventions

Check .NET conventions across the solution: naming, security patterns,
EF Core conventions, and module dependency rules.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `projectFilter` | string | no | Filter by project name |
| `file` | string | no | Check a specific file only |
| `checkCategory` | string | no | `all`, `naming`, `security`, `efcore`, `dependencies` (default: `all`) |

**Example prompt:** "Validate conventions in MyApp.Security"

**Returns:** Violations grouped by category with file path, line number, and suggested fix.

### Convention checks

| Category | What it checks |
| -------- | -------------- |
| `naming` | `*Dto` suffix (forbidden), endpoint DTO prefixes, response record naming |
| `security` | Hardcoded secrets, `Guid.NewGuid()` usage, `new Regex()` without `[GeneratedRegex]` |
| `efcore` | Synchronous `SaveChanges()`, missing `AsNoTracking()`, manual `HasQueryFilter` |
| `dependencies` | `[DependsOn]` matches `<ProjectReference>`, no circular module refs |
