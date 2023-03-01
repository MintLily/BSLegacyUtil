# BSLegacyUtil
Converted from windows batch to C# | Beat Saber Legacy Group's Beat Saber Downgrade utility that is using [Steam's DepotDownloader](https://github.com/SteamRE/DepotDownloader)

## Quick Links
Issues, Bug Reports, Support? [Go here](https://github.com/BeatSaberLegacyGroup/BSLegacyUtil/issues) and create an issue.<br>
Need to Download Utility? [Go here](https://github.com/BeatSaberLegacyGroup/BSLegacyUtil/releases/latest) and download the latest version<br><br>
You'll need to install the following:<br>
To open the exe: [.NET Core Desktop Runtime v7.0.2+](https://link.bslegacy.com/dotnet7)

## Available Downgrades
| Major  | Patch -- | -------- | ------ | -----> |
|--------|----------|----------|--------|--------|
| 0.10.1 | 0.10.2   | 0.10.2p1 |
| 0.11.0 | 0.11.1   | 0.11.2   |
| 0.12.0 | 0.12.0p1 | 0.12.1   |
| 0.13.0 | 0.13.0p1 | 0.13.1   | 0.13.2 |
| 1.0.0  | 1.0.1    |
| 1.1.0  | 1.1.0p1  |
| 1.2.0  |
| 1.3.0  |
| 1.4.0  | 1.4.2    |
| 1.5.0  |
| 1.6.0  | 1.6.1    |
| 1.7.0  |
| 1.8.0  |
| 1.9.0  | 1.9.1    |
| 1.10.0 |
| 1.11.0 | 1.11.1   |
| 1.12.0 | 1.12.2   |
| 1.13.0 | 1.13.1   | 1.13.4   | 1.13.5 |
| 1.14.0 |
| 1.15.0 |
| 1.16.0 | 1.16.1   | 1.16.2   | 1.16.3 | 1.16.4 |
| 1.17.0 | 1.17.1   |
| 1.18.0 | 1.18.1   | 1.18.2   | 1.18.3 |
| 1.19.0 |
| 1.20.0 |
| 1.21.0 |
| 1.22.0 | 1.22.1   |
| 1.23.0 |
| 1.24.0 | 1.24.1   |
| 1.25.0 | 1.25.1   |
| 1.26.0 |
| 1.27.0 |
| 1.28.0 |

# Contributors / Credits
This project contains code from the following Users and Their projects: _(All rights and credit goes to them)_
* Steam - [DepotDownloader](https://github.com/SteamRE/DepotDownloader)
* RiskiVR & ComputerElite & DDAkebono - [BSLegacyLauncher](https://github.com/RiskiVR/BSLegacyLauncher)
* BSMG (nike4613) - [BSIPA](https://github.com/bsmg/BeatSaber-IPA-Reloaded)
* kasperstyre - [Pull Request #9](https://github.com/BeatSaberLegacyGroup/BSLegacyUtil/pull/9)

# Developers
Want to compile this yourself? You'll need to install the following:
- .NET 7.0.2+ SDK (https://dotnet.microsoft.com/download/dotnet/7.0)
- [JetBrains Rider](https://www.jetbrains.com/rider/) (2022.3+) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (17.4.3+)
- - *I use Rider to develop the current version*

# App Version Logic
```
1970.1.2r3 - 123 Release
Year.Month.Major[Revision] [build] [type]
v1970.1.2r3 build:123 type:Release|Beta|Alpha
```

**Current Version**
```
v2023.1.2r3 - 504 Release
SHA256 Checksums:
FD-ZIP: a95dde89e08d2db9681050538f2e2d18972995e5743304989061442b44d736de
FD-EXE: 6c0b18db7b3298a002eff2f0186fcdbb2ec567142853f8943a4a63197b4b9a81
SC-ZIP: cbab4172c3dd5458798dbef5ed83969425ae8d1c0443ed5222c6a37318a877fb
SC-EXE: 0bad0ab7a8dc4fe85b69659d6078a65d43b59bb34839add5c82893ed916b5e46
```
