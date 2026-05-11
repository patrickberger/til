namespace TilTool.Cli.Core;

public sealed class FrontMatter
{
    public required string Title { get; init; }

    public required string Category { get; init; } = string.Empty;

    public required DateOnly Date { get; init; }
}
