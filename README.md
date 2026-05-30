# WaveTrumpet

WaveTrumpet is a native Windows tray app prototype inspired by EarTrumpet, aimed at controlling Elgato Wave Link 2.0 audio devices.

This repository is currently a work-in-progress proof of concept. The current focus is the native app shell, tray behavior, flyout UI, and CI/build reliability. As the app matures, the PoC/WIP framing in this file should be removed.

## Current state

- Native WPF app on .NET Framework 4.6.2
- x86 target, matching the EarTrumpet-style direction we chose for the PoC
- Tray icon with a flyout window
- Sample device rows, slider UI, and placeholder secondary windows
- GitHub Actions build on push with downloadable artifacts

## Easy route: download the latest build artifact

If you do not want to build locally, you can download the latest successful CI artifact from the GitHub Actions run for this repository.

1. Open the repository's Actions tab.
2. Open the latest successful `WaveTrumpet CI` run.
3. Download the `WaveTrumpet-Release` artifact.
4. Extract it and run `WaveTrumpet.exe` from the released files.

This is the easiest path while the project is still in PoC/WIP shape.

## Development build instructions

### Prerequisites

- Windows
- Visual Studio 2022 or Visual Studio Build Tools with .NET Framework 4.6.2 targeting support
- NuGet restore support

### Build in Visual Studio

1. Open `WaveTrumpet.sln`
2. Select `Release | x86` or `Debug | x86`
3. Restore NuGet packages if prompted
4. Build the solution

Build output is written to:

- `Build/Debug/`
- `Build/Release/`

### Build in VS Code

The repository includes VS Code task and launch configuration files.

1. Open the repository root in VS Code
2. Run the default build task
3. Use the provided launch profile if your machine has the required desktop build tooling installed

## CI

GitHub Actions builds the solution on push and pull request to `main`.

Current CI behavior:

- Restores NuGet packages
- Builds `WaveTrumpet.sln` in `Release | x86`
- Uploads the build output as the `WaveTrumpet-Release` artifact
- Retains artifacts for 1 day

## Notes

- The current UI is a native PoC shell with sample data, not a full Wave Link integration yet.
- A deferred work list is tracked in `DEFERRED_REIMPLEMENTATION.md` for anything intentionally simplified during CI stabilization.
