namespace TilTool.Cli.Core;

public sealed record TilCategory(string Name, string Folder, IList<TilEntry> Entries);
