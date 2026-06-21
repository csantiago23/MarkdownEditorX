[Setup]
AppName=Markdown Editor X
AppVersion=1.0.2
AppPublisher=Markdown Editor X Team
DefaultDirName={autopf}\Markdown Editor X
DefaultGroupName=Markdown Editor X
UninstallDisplayIcon={app}\MarkdownEditorApp.exe
OutputDir=C:\Projects\Installers
OutputBaseFilename=MarkdownEditorX_Setup
SetupIconFile=appicon.ico
Compression=lzma2
SolidCompression=yes
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
DisableWelcomePage=no
ChangesAssociations=yes

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "bin\Release\net8.0-windows10.0.19041.0\win10-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Markdown Editor X"; Filename: "{app}\MarkdownEditorApp.exe"
Name: "{autodesktop}\Markdown Editor X"; Filename: "{app}\MarkdownEditorApp.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\MarkdownEditorApp.exe"; Description: "{cm:LaunchProgram,Markdown Editor X}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKA; Subkey: "Software\Classes\.md\OpenWithProgids"; ValueType: string; ValueName: "MarkdownEditorX.AssocFile.md"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.md"; ValueType: string; ValueName: ""; ValueData: "Markdown Document"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.md\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\appicon.ico"
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.md\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\MarkdownEditorApp.exe"" ""%1"""

Root: HKA; Subkey: "Software\Classes\.txt\OpenWithProgids"; ValueType: string; ValueName: "MarkdownEditorX.AssocFile.txt"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.txt"; ValueType: string; ValueName: ""; ValueData: "Text Document"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.txt\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\appicon.ico"
Root: HKA; Subkey: "Software\Classes\MarkdownEditorX.AssocFile.txt\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\MarkdownEditorApp.exe"" ""%1"""

