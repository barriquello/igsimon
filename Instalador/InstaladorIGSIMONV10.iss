[Setup]
AppId={{3AD69F53-FC19-4C3D-BF8D-C83C46B62B77}
AppName=IGSIMON
AppVersion=1.0
AppSupportURL=
AppUpdatesURL=
DefaultDirName={userdesktop}\IGSIMON
DefaultGroupName=Supervisorio
DisableProgramGroupPage=true
OutputBaseFilename=setup_IGSIMON
Compression=none
SolidCompression=true
InternalCompressLevel=none


[Languages]
Name: english; MessagesFile: compiler:Default.isl
Name: brazilianportuguese; MessagesFile: compiler:Languages\BrazilianPortuguese.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}
Name: inicializacao; Description: Iniciar o programa ao inicializar o computador; GroupDescription: {cm:AdditionalIcons}

[Files]
Source: .\Dependencias\dotNetFx40_Full_x86_x64.exe; DestDir: {app}
Source: ..\InterfaceDesktop\bin\Release\System.Data.SQLite.dll; DestDir: {app}
Source: .\Dependencias\vcredist_x86.exe; DestDir: {app}
Source: ..\InterfaceDesktop\bin\Release\InterfaceDesktop.exe; DestDir: {app}; Flags: ignoreversion
Source: ..\InterfaceDesktop\bin\Release\DocumentFormat.OpenXml.dll; DestDir: {app}
Source: .\Dependencias\visualbasicpowerpackssetup.exe; DestDir: {app}

[Icons]
Name: {group}\IGSIMON; Filename: {app}\InterfaceDesktop.exe
Name: {userdesktop}\IGSIMON; Filename: {app}\InterfaceDesktop.exe; Tasks: desktopicon
Name: {group}\{cm:UninstallProgram, Supervisorio}; Filename: {uninstallexe}
Name: {userstartup}\IGSIMON; Filename: {app}\InterfaceDesktop.exe; Tasks: " inicializacao"
[Run]
Filename: {app}\dotNetFx40_Full_x86_x64.exe; Parameters: /passive
Filename: {app}\vcredist_x86.exe; Parameters: /passive
Filename: {app}\visualbasicpowerpackssetup.exe; Parameters: /passive
