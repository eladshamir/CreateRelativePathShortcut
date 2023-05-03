# CreateRelativePathShortcut
This is a simple program that generates shortcuts [(MS-SHLLINK)](https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-shllink/16cb4ca1-9339-4d0c-a68d-bf1d6cc0f943) with relative paths

## Usage

```
CreateRelativePathShortcut.exe 
/path:<TARGET_PATH> /args:<CMD_LINE_ARGS> /iconpath:<ICON_PATH> /iconindex:<INT> /output:<OUTPUT_PATH> /readonly
```

The following command line arguments are required:
* path: The relative path of the target application
* output: The path to save the generated shortcut

The following command line arguments are options:
* args: Command line arguments
* iconpath: The path of the icon file
* iconindex: The index of the icon in the icon file
* readonly: If present, sets the shortcut file to read-only (recommended)


### Example:

```
CreateRelativePathShortcut.exe /path:"bin\start.exe" /args:"--quiet --force" /iconpath:"%SystemRoot%\System32\imageres.dll" /iconindex:1 /output:"Relative Path Shortcut.lnk /readonly"
```

## Keeping It Relative

When you launch an LNK file with a relative path, Windows automatically modifies the file to point to the absolute path, making the shortcuts created by this program good for only one time use.

As a workaround, use the `/readonly` flag to set the generated LNK file as read-only, stopping Windows from modifying it.

## Credits

This project leverages the following libraries:
* [PropertyStore](https://github.com/securifybv/PropertyStore) by Securify
* [ShellLink](https://github.com/securifybv/ShellLink) by Securify

