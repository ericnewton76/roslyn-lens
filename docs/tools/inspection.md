# Inspection Tools

## get_public_api

Get the public surface of a type without reading the full file. Returns public methods,
properties, and events with their signatures.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `typeName` | string | yes | Type name to inspect |

**Example prompt:** "Show the public API of WorkflowEngine"

**Returns:** List of public members with signatures and return types.

---

## get_symbol_detail

Full signature, parameters, return type, and XML documentation for a symbol.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `symbolName` | string | yes | Symbol name |
| `containingType` | string | no | Containing type to disambiguate |

**Example prompt:** "Get details of the Encrypt method in ITransitEncryptionService"

**Returns:** Full signature, parameter descriptions, return type, and XML doc summary.

---

## get_type_hierarchy

Show the inheritance chain, implemented interfaces, and derived types.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `typeName` | string | yes | Type name |

**Example prompt:** "Show the type hierarchy for AggregateRoot"

**Returns:** Base types (upward chain), implemented interfaces, and known derived types.

---

## get_dependency_graph

Visualize the call chain from a method, showing what it calls and what calls it.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `symbolName` | string | yes | Starting method name |
| `file` | string | no | File path to disambiguate |
| `line` | int | no | Line number to disambiguate |
| `depth` | int | no | Max depth to traverse (default: 2) |

**Example prompt:** "Show the dependency graph of HandleAsync in CreateOrderHandler"

**Returns:** Tree of method calls with depth indicators.
