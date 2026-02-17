namespace TilTool.Cli.Core;

using System.Text;

public static class TilIndexRenderer
{
    private const string ReadmeStartMarker = "<!-- til-index:start -->";

    private const string ReadmeEndMarker   = "<!-- til-index:end -->";

    private static readonly string DefaultReadmeContent = $"""
                                                           # TIL: Today I Learned

                                                           {ReadmeStartMarker}
                                                           {ReadmeEndMarker}
                                                           """;

    public static void Render(TilIndex tilIndex, string readmeFilePath)
    {
        VerifyReadmeFile(readmeFilePath);
        var content = CreateIndex(tilIndex);
        UpdateReadmeFile(readmeFilePath, content);
    }

    private static void UpdateReadmeFile(string readmeFilePath, string content)
    {
        var updatedContent = new StringBuilder();
        var isInIndex = false;
        var readmeContent = File.ReadAllLines(readmeFilePath);
        
        foreach (var line in readmeContent)
        {
            if (line.StartsWith(ReadmeStartMarker))
            {
                updatedContent.AppendLine(line);
                updatedContent.AppendLine(content);
                isInIndex = true;
                continue;
            }

            if (line.StartsWith(ReadmeEndMarker))
            {
                updatedContent.AppendLine(line);
                isInIndex = false;
                continue;
            }

            if (isInIndex) continue;
            
            updatedContent.AppendLine(line);
        }
        
        File.WriteAllText(readmeFilePath, updatedContent.ToString(), new UTF8Encoding(false));
    }

    private static string CreateIndex(TilIndex tilIndex)
    {
        var content = new StringBuilder();

        foreach (var category in tilIndex.Categories)
        {
            content.AppendLine()
                   .AppendLine($"## {category.Folder}")
                   .AppendLine();

            foreach (var entry in category.Entries)
            {
                content.AppendLine($"- [{entry.Title}]({entry.RelativeFilePath}) ({entry.Date:yy-MMM-dd})");
            }
        }

        return content.ToString();
    }

    private static void VerifyReadmeFile(string readmeFilePath)
    {
        if (File.Exists(readmeFilePath)) return;
        File.WriteAllText(readmeFilePath, DefaultReadmeContent);
    }
}
