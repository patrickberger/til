namespace TilTool.Cli.Core;

using System.Text.RegularExpressions;

public static class PathUtilities
{
    public static string Slugify(string? source)
    {
        var slug = (source ?? string.Empty).Trim().ToLowerInvariant();
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");
        slug = Regex.Replace(slug, @"\-{2,}", "-").Trim('-');
        return string.IsNullOrWhiteSpace(slug) ? string.Empty : slug;
    }

    public static string ToPosix(string? source) => source?.Replace('\\', '/') ?? string.Empty;
}
