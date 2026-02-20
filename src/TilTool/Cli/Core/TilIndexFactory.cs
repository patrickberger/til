namespace TilTool.Cli.Core;

using System.Text;

public static class TilIndexFactory
{
    private const string FrontMatterDelimiter = "---";

    public static TilIndex Create(string rootFolder, string tilFolderName)
    {
        var categories = new Dictionary<string, TilCategory>();
        var tilRootFolder = Path.Combine(rootFolder, tilFolderName);
        var filePaths = Directory.EnumerateFiles(tilRootFolder, "*.md", SearchOption.AllDirectories)
                                 .Where(p => !Path.GetFileName(p).Equals("README.md", StringComparison.OrdinalIgnoreCase))
                                 .ToList();

        foreach (var filePath in filePaths)
        {
            var relativePath = PathUtilities.ToPosix(Path.GetRelativePath(tilRootFolder, filePath));
            var parts = relativePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var categoryFolder = parts.Length >= 2 ? parts[0] : string.Empty;
            var frontMatter = ReadFrontMatter(filePath);
            var categoryName = string.IsNullOrEmpty(frontMatter.Category) ? categoryFolder : frontMatter.Category;

            if (!categories.ContainsKey(categoryName))
                categories.Add(categoryName, new TilCategory(categoryName, categoryFolder, new List<TilEntry>()));
            
            var relativeFilePath = PathUtilities.ToPosix(Path.GetRelativePath(rootFolder, filePath));
            var entry = new TilEntry(RelativeFilePath: relativeFilePath, Title: frontMatter.Title, Date: frontMatter.Date);
            categories[categoryName].Entries.Add(entry);
        }

        return new TilIndex(FileCount: filePaths.Count, Categories: categories.Values.ToList());
    }

    private static FrontMatter ReadFrontMatter(string filePath)
    {
        using var reader = new StreamReader(filePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
        
        var lines = new List<string>();
        var isInFrontMatter = false;
        for (var i = 0; i < 60 && !reader.EndOfStream; i++)
        {
            var line = reader.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.Equals(FrontMatterDelimiter, StringComparison.OrdinalIgnoreCase))
            {
                isInFrontMatter = !isInFrontMatter;
                continue;
            }

            if (!isInFrontMatter) continue;
            lines.Add(line);
        }

        var yaml = string.Join(Environment.NewLine, lines);
        return Yaml.Deserialize<FrontMatter>(yaml);
    }
}
