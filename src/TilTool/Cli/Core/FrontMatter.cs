namespace TilTool.Cli.Core;

public sealed class FrontMatter
{
    public required string Title  { get; init; }

    public required DateOnly Date  { get; init; }
}
