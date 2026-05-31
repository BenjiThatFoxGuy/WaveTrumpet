# Deferred reimplementation

This file tracks anything temporarily removed or simplified during CI stabilization so it can be reimplemented intentionally.

## Removed or simplified for current PoC

- `GitVersionTask` package usage was removed from the build setup after CI restore failed because version `5.12.0` does not exist under that package name.
  - Follow-up: decide whether to restore versioning via `GitVersion.MsBuild` or keep static assembly versioning.
- Project icon resource wiring for `Assets/Icon-Dark.ico` was removed from the project file because the referenced asset did not exist.
  - Follow-up: add real branded tray/app icon assets and restore project icon settings.
- Secondary windows (`FullWindow`, `DialogWindow`, `SettingsWindow`) will be kept as minimal placeholders in the PoC.
  - Follow-up: flesh them out when those flows are implemented.
- Theme infrastructure is no longer the earlier minimal subset; the flyout now uses an upstream-style acrylic/composition path focused on the Windows 10 target.
  - Follow-up: continue replacing the remaining placeholder theme/model pieces with direct EarTrumpet-derived behavior where practical.
- Current visual target is a Windows 10 acrylic-style flyout first.
  - Follow-up: add a separate Windows 11 visual path later if needed without regressing the Windows 10 LTSC focus.
