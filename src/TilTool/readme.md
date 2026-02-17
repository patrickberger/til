# TilTool

## Summary

This is the tool used to create and maintain this repository.

## Sample Usage

```bash
# Create a new TIL.
dotnet run -- add --category "git" --title "I learned to push today"
# Re-create table of contents.
dotnet run -- index
```

## Documentation

### Add a new TIL

This will create a new markdown file inside a category subfolder.

```bash
USAGE
  TilTool.Cli add --category <value> [options]

DESCRIPTION
  Adds a new til entry.

OPTIONS
* -c|--category     The category of the til. 
  -r|--root         The root directory of the til. Defaults to the directory above folder 'src'. Default: ".\..\..\..".
  -t|--title        The title of the til. Default: "new til".
  -h|--help         Shows help text. 
```

### Re-Create table of contents

This will replace current table of content in readme file with updated contents.

```bash
USAGE
  TilTool.Cli index [options]

DESCRIPTION
  (Re-)Creates TOC.

OPTIONS
  -r|--root         The root directory of the til. Defaults to the directory above folder 'src'. Default: ".\..\..\..".
  -h|--help         Shows help text. 
```