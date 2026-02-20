---
title: Data Driven Tests
category: MS Test
date: 2026-02-20
---

# Data Driven Tests

## Data via Attributes

The `[DataRow]` attribute could be used in place to provide simple data. `[DataTestMethod]` attribute should be used instead of `[TestMethod]`.

```csharp
[TestClass]
public class TestClass
{
    [DataTestMethod]
    [DataRow("one", 1)]
    [DataRow("two", 2)]
    [DataRow("three", 3)]
    public void Test(string parameter1, int parameter2)
    {
        // Test logic.
    }

}
```

## Data via Property or Method

The `[DynamicData]` attribute comes in handy if test data construction should be separated from according method. Data could be provided by either a method or a property. Attribute parameter `DynamicDataSourceType` must be adjusted accordingly.


```csharp
[TestClass]
public class TestClass
{
    public static IEnumerable<object[]> TestDataProperty =>
        new[]
        {
            new object[] { "one", 1 },
            new object[] { "two", 2 },
            new object[] { "three", 3 }
        };

    ...

    [DataTestMethod]
    [DynamicData(nameof(TestDataProperty), DynamicDataSourceType.Property)]
    public void PropertyBasedTest(string parameter1, int parameter2)
    {
        // Test logic.
    }
}
```

```csharp
[TestClass]
public class TestClass
{
    public static IEnumerable<object[]> TestDataMethod()
    {
        yield return new object[] { "one", 1 };
        yield return new object[] { "two", 2 };
        yield return new object[] { "three", 3 };
    }

    ...

    [DataTestMethod]
    [DynamicData(nameof(TestDataMethod), DynamicDataSourceType.Method)]
    public void PropertyBasedTest(string parameter1, int parameter2)
    {
        // Test logic.
    }
}
```

## Data via Custom Data Source 

```csharp
public class CustomDataSourceAttribute : Attribute, ITestDataSource
{
    public IEnumerable<object[]> GetData(MethodInfo methodInfo)
    {
        yield return new object[] { "one", 1 };
        yield return new object[] { "two", 2 };
        yield return new object[] { "three", 3 };
    }

    public string GetDisplayName(MethodInfo methodInfo, object[] data)
    {
        if (data is null) return null;
        return $"{methodInfo.Name} / {string.Join(", ", data)}";
    }
}

[TestClass]
public class TestClass
{
    [DataTestMethod]
    [CustomDataSource]
    public void PropertyBasedTest(string parameter1, int parameter2)
    {
        // Test logic.
    }
}
```

# References

- [MSTest v2: Data tests](https://web.archive.org/web/20260220070757/https://www.meziantou.net/mstest-v2-data-tests.htm)
- [Unit testing – Data-driven tests](https://web.archive.org/web/20260220073856/https://community.dataminer.services/unit-testing-data-driven-tests/)