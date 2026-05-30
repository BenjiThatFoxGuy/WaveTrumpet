# Deferred reimplementation

This file tracks anything temporarily removed or simplified during CI stabilization so it can be reimplemented intentionally.

## Removed or simplified for current PoC

- `GitVersionTask` package usage was removed from the build setup after CI restore failed because version `5.12.0` does not exist under that package name.
  - Follow-up: decide whether to restore versioning via `GitVersion.MsBuild` or keep static assembly versioning.
- Project icon resource wiring for `Assets/Icon-Dark.ico` was removed from the project file because the referenced asset did not exist.
  - Follow-up: add real branded tray/app icon assets and restore project icon settings.
- Secondary windows (`FullWindow`, `DialogWindow`, `SettingsWindow`) will be kept as minimal placeholders in the PoC.
  - Follow-up: flesh them out when those flows are implemented.
- Theme infrastructure will be implemented as a minimal PoC subset rather than a full EarTrumpet-equivalent theme system.
  - Follow-up: expand theme rules, acrylic behavior, and OS/high-contrast integration.
