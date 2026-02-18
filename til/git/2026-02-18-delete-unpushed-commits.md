---
title: Delete unpushed commits
date: 2026-02-18
---

# Delete unpushed commits

Delete most recent commit *keeping changes*:

```
git reset --soft HEAD~1
```

Delete most recent commit *destroying changes*:

```
git reset --hard HEAD~1
```

Delete every change not pushed yet, syncing local branch with remote:

```
git reset --hard origin/<branch>
```

# References

- [How do I delete unpushed git commits?](https://stackoverflow.com/a/54323316) (Stack Overflow)
- [git revert Documentation](https://git-scm.com/docs/git-revert)