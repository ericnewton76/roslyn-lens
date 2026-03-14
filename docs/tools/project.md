# Project Tools

## get_project_graph

Show the solution's project dependency tree.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| (none) | | | |

**Example prompt:** "Show the project dependency graph"

**Returns:** Each project with its direct project references.

---

## get_diagnostics

Get compiler warnings and errors for a project or the entire solution.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `scope` | string | no | `solution` (default) or `project` |
| `path` | string | no | Project name or path (required when scope is `project`) |
| `severityFilter` | string | no | `error`, `warning`, or `all` (default: `all`) |

**Example prompt:** "Show all compiler errors in MyApp.Security"

**Returns:** Diagnostic ID, severity, message, file path, and line number.

---

## get_test_coverage_map

Heuristic mapping of which tests cover which production types, based on naming
conventions and project references.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `projectFilter` | string | no | Filter by project name |
| `maxResults` | int | no | Max results to return (default: 50) |

**Example prompt:** "Show test coverage map for MyApp.Validation"

**Returns:** Production types mapped to their corresponding test classes (if found).
