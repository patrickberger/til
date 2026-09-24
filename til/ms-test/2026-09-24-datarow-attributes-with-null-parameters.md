---
title: DataRow attributes with null parameters
category: MS Test
date: 2026-09-24
---

# DataRow attributes with null parameters

Using Visual Studio's test explorer below example does not recognize first `DataRow` attribute and does not execute according test.

```csharp
[TestClass]
public class TestClass
{
    [DataTestMethod]
    [DataRow(null)] // Null.
    [DataRow("")] // Empty.
    [DataRow(" ")] // Whitespace.
    public void Test(string parameter)
    {
        Assert.IsNotNull(parameter);
    }

}
```

Solution is to provide a display name explicitly.

```csharp
[TestClass]
public class TestClass
{
    [DataTestMethod]
    [DataRow(null, DisplayName = $"{nameof(Test)} (null)")] // Null.
    [DataRow("")] // Empty.
    [DataRow(" ")] // Whitespace.
    public void Test(string parameter)
    {
        Assert.IsNotNull(parameter);
    }

}
```

# References

- [DataTestMethod skips DataRows that provide different data arrays, only keeping last](https://github.com/microsoft/testfx/issues/1016#issuecomment-1183624178)
- [TestCase with DataRow attribute with null parameter ignored
](https://github.com/microsoft/testfx/issues/1028)