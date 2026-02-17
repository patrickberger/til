namespace TilIndexer.Commands;

using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;

using JetBrains.Annotations;

using TilTool.Cli.Core;

[Command("index", Description = "(Re-)Creates TOC.")]
[UsedImplicitly]
public sealed class IndexCommand : ICommand
{
    private const string DefaultTilFolder = "til";

    private const string ReadmePath = "readme.md";

    [CommandOption("root", 'r', Description = "The root directory of the til. Defaults to the directory above folder 'src'.", IsRequired = false)]
    public string RootFolder { get; init; } = @".\..\..\..";

    public ValueTask ExecuteAsync(IConsole console)
    {
        var rootFolder = Path.GetFullPath(RootFolder);
        var tilRootFolder = Path.Combine(rootFolder, DefaultTilFolder);
        if (!Directory.Exists(tilRootFolder)) throw new CommandException($"Directory '{tilRootFolder}' does not exist.");

        var tilIndex = TilIndexFactory.Create(rootFolder, DefaultTilFolder);
        console.Output.WriteLine($"Found {tilIndex.FileCount} files in {tilIndex.Categories.Count} categories.");

        var readmeFilePath = Path.Combine(rootFolder, ReadmePath);
        TilIndexRenderer.Render(tilIndex, readmeFilePath);

        return default;
    }
}
