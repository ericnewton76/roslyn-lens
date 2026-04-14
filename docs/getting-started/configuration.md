# Configuration

## Claude Code — User scope (recommended)

Registers the MCP server globally, available in all projects:

```bash
claude mcp add --scope user --transport stdio roslyn-lens -- roslyn-lens
```

## Claude Code — Project scope

Registers for a single project only:

```bash
claude mcp add --transport stdio roslyn-lens -- roslyn-lens
```

## Manual `.mcp.json`

Create `.mcp.json` in your project root:

```json
{
  "mcpServers": {
    "roslyn-lens": {
      "type": "stdio",
      "command": "roslyn-lens",
      "args": []
    }
  }
}
```

## Solution discovery

By default, the server finds the nearest `.sln` or `.slnx` file
using breadth-first search from the current directory (up to 3
levels).

To specify a solution explicitly:

```bash
claude mcp add --scope user --transport stdio roslyn-lens \
  -- roslyn-lens --solution /path/to/My.slnx
```

### Multiple solutions

When multiple `.sln`/`.slnx` files are discovered, the server
auto-selects the shallowest (then alphabetically first) and logs a
warning listing all found solutions.

Two MCP tools enable runtime switching without restarting:

- `list_solutions` — lists all discovered solutions with an
  `IsActive` flag
- `switch_solution` — reloads the workspace with a different
  solution (with rollback on failure)

See [Solution Management Tools](../tools/solution.md) for details.

## Verify connection

```bash
claude mcp list
```

Expected output:

```text
roslyn-lens: roslyn-lens — Connected
```

## Remove

```bash
claude mcp remove roslyn-lens
```
