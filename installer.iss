[Setup]
AppName=Markdown Editor X
AppVersion=1.0
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

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "bin\Release\net8.0-windows10.0.19041.0\win-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Markdown Editor X"; Filename: "{app}\MarkdownEditorApp.exe"
Name: "{autodesktop}\Markdown Editor X"; Filename: "{app}\MarkdownEditorApp.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\MarkdownEditorApp.exe"; Description: "{cm:LaunchProgram,Markdown Editor X}"; Flags: nowait postinstall skipifsilent
