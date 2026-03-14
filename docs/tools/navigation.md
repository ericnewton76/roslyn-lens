# Navigation Tools

## find_symbol

Locate type or method definitions by name.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `name` | string | yes | Symbol name to search for |
| `kind` | string | no | Filter by kind: `class`, `interface`, `method`, `property`, `enum` |

**Example prompt:** "Find the IClock interface"

**Returns:** File path, line number, kind, and containing namespace for each match.

---

## find_references

Find all usages of a symbol across the solution.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `symbolName` | string | yes | Symbol name to find references for |
| `file` | string | no | Restrict search to a specific file |
| `line` | int | no | Line number to disambiguate overloaded symbols |

**Example prompt:** "Find all usages of ICurrentTenant"

**Returns:** List of file paths and line numbers where the symbol is referenced.

---

## find_implementations

Find classes that implement an interface or derive from a base class.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `interfaceName` | string | yes | Interface or base class name |

**Example prompt:** "Find all implementations of IBlobStorageProvider"

**Returns:** Implementing type names with file paths and line numbers.

---

## find_callers

Find direct callers of a method.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `methodName` | string | yes | Method name |
| `className` | string | no | Containing class to disambiguate |

**Example prompt:** "Find callers of SaveChangesAsync in OrderRepository"

**Returns:** Calling method names with file paths and line numbers.

---

## find_overrides

Find overrides of virtual or abstract methods.

| Parameter | Type | Required | Description |
| --------- | ---- | -------- | ----------- |
| `methodName` | string | yes | Method name |
| `className` | string | no | Containing class to disambiguate |

**Example prompt:** "Find overrides of OnModelCreating"

**Returns:** Overriding method locations with file paths and line numbers.
