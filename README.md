# CreateRelativePathShortcut
This is a simple program that generates shortcuts (MS-SHLLINK) with relative paths

## Usage

```
CreateRelativePathShortcut.exe 
/path:<TARGET_PATH> /args:<CMD_LINE_ARGS> /iconpath:<ICON_PATH> /iconindex:<INT> /output:<OUTPUT_PATH>
```

The following command line arguments are required:
* path: The relative path of the target application
* output: The path to save the generated shortcut

The following command line arguments are options:
* args: Command line arguments
* iconpath: The path of the icon file
* iconindex: The index of the icon in the icon file


### Example:

`CreateRelativePathShortcut.exe /path:"bin\start.exe" /args:"--quiet --force" /iconpath:"%SystemRoot%\System32\imageres.dll" /iconindex:1 /output:"Relative Path Shortcut.lnk"`
