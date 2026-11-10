---
title: Update all silently
category: WinGet
date: 2026-11-10
---

# Update all silently

Following command lets WinGet upgrade all packages having a newer version available without user interaction.

```bash
winget upgrade --all --silent --accept-package-agreements --accept-source-agreements
```

| Flag | Description |
| -- | -- |
| --all | Upgrades all installed packages that have a newer version available |
| --silent | Runs each installer silently, with no installer windows |
| --accept-package-agreements | Accepts package licence agreements so no prompt appears |
| --accept-source-agreements | Accepts the source agreement (for example the Microsoft Store source) |

# References

- [Winget Upgrade All: Update Every App with One Command](https://microsoftsoftwareswap.com/blogs/blog/winget-upgrade-all)