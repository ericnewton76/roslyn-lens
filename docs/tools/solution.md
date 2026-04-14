# Solution Management Tools

Tools for working with repositories that contain multiple
`.sln`/`.slnx` files. At startup, RoslynLens discovers all
solutions via BFS and auto-selects the shallowest one. These
tools let you inspect and switch solutions at runtime.

## list_solutions

Lists all discovered solution files with their paths and which
one is currently loaded.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| *(none)* | | | |

**Example prompt:** "What solutions are available?"

**Returns:**

- `Solutions` — array of `{ Path, Name, IsActive }`
- `Hint` — contextual message when multiple solutions exist
  (null when only one solution is found)

Does not require the workspace to be ready (reads static
discovery state).

---

## switch_solution

Switch the active workspace to a different discovered solution.
The path must be one returned by `list_solutions`.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `solutionPath` | string | yes | Full path to the target solution |

**Example prompt:** "Switch to Frontend.slnx"

**Behavior:**

1. Validates the path is in the discovered list
2. Disposes the current workspace (watchers, cache, MSBuild)
3. Loads the new solution
4. On failure, attempts to rollback to the previous solution

**Returns on success:**

```json
{ "status": "switched", "solution": "Frontend.slnx", "projectCount": 12 }
```

**Returns on error:**

```json
{ "error": "Failed to switch solution: ...", "state": "Error" }
```
