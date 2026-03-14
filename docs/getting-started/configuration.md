# Configuration

## Claude Code — User scope (recommended)

Registers the MCP server globally, available in all projects:

```bash
claude mcp add --scope user --transport stdio roslyn-navigator -- jfm-roslyn-navigator
```

## Claude Code — Project scope

Registers for a single project only:

```bash
claude mcp add --transport stdio roslyn-navigator -- jfm-roslyn-navigator
```

## Manual `.mcp.json`

Create `.mcp.json` in your project root:

```json
{
  "mcpServers": {
    "roslyn-navigator": {
      "type": "stdio",
      "command": "jfm-roslyn-navigator",
      "args": []
    }
  }
}
```

## Solution discovery

By default, the server finds the nearest `.sln` or `.slnx` file using breadth-first
search from the current directory (up to 3 levels).

To specify a solution explicitly:

```bash
claude mcp add --scope user --transport stdio roslyn-navigator -- jfm-roslyn-navigator --solution /path/to/My.slnx
```

## Verify connection

```bash
claude mcp list
```

Expected output:

```text
roslyn-navigator: jfm-roslyn-navigator — Connected
```

## Remove

```bash
claude mcp remove roslyn-navigator
```
