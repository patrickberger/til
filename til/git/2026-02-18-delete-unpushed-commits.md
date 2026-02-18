---
title: Delete unpushed commits
date: 2026-02-18
---

# Delete unpushed commits

Delete most recent commit keeping changes:

```
git reset --soft HEAD~1
```

Delete most recent commit destroying changes:

```
git reset --hard HEAD~1
```