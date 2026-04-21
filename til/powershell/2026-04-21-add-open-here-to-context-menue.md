---
title: Add Open here to context menue
category: PowerShell
date: 2026-04-21
---

# Add Open here to context menue

As of today I never got those context menue entries created on installing PowerShell 7 working. I finally succeeded adding them manually.

Below registry entry opens an Administrator PowerShell console from windows context menue in current directory. 
```
Windows Registry Editor Version 5.00

[HKEY_CLASSES_ROOT\Directory\Background\shell\pwsh_here]
@="PowerShell 7: Open here as Administrator"
"Icon"="C:\\\\Program Files\\\\PowerShell\\\\7\\\\pwsh.exe"
"HasLUAShield"=""

[HKEY_CLASSES_ROOT\Directory\Background\shell\pwsh_here\command]
@="C:\\Program Files\\PowerShell\\7\\pwsh.exe -NoExit -RemoveWorkingDirectoryTrailingCharacter -WorkingDirectory \"%V!\" -Command \"$host.UI.RawUI.WindowTitle = 'PowerShell 7 (x64)'\""
```
