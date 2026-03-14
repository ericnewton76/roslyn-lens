# Analysis Tools

## detect_antipatterns

Run anti-pattern detectors across the solution. See [General Detectors](../detectors/general.md)
and [Domain-Specific Detectors](../detectors/domain.md) for the full list.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `file` | string | no | Scan a specific file only |
| `projectFilter` | string | no | Filter by project name |
| `severity` | string | no | `error`, `warning`, `info`, or `all` (default: `all`) |
| `maxResults` | int | no | Max results to return (default: 100) |

**Example prompt:** "Detect anti-patterns in MyApp.Storage"

**Returns:** Violations with detector ID, severity, message, file path, and line number.

---

## detect_circular_dependencies

Detect circular dependencies at the project or type level using DFS cycle detection.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `scope` | string | no | `projects` (default) or `types` |
| `projectFilter` | string | no | Filter by project name |

**Example prompt:** "Check for circular project dependencies"

**Returns:** List of cycles found (e.g., `A → B → C → A`).

---

## find_dead_code

Detect unused types, methods, or properties across the solution.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `scope` | string | no | `solution` (default) or `project` |
| `path` | string | no | Project name (required when scope is `project`) |
| `kind` | string | no | `type`, `method`, `property`, or `all` (default: `all`) |
| `maxResults` | int | no | Max results to return (default: 50) |

**Example prompt:** "Find dead code in MyApp.Caching"

**Returns:** Unused symbols with kind, name, file path, and line number.
