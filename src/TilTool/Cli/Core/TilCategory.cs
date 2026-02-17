namespace TilTool.Cli.Core;

public sealed record TilCategory(string Folder, IList<TilEntry> Entries);
