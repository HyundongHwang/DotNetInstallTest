!include "Sections.nsh"
!include "CustomFunctions.nsh"

!define PRODUCT_NAME "PosSlTest"
!define PRODUCT_VERSION "0.9"
!define PRODUCT_NAME_VERSION "${PRODUCT_NAME} ${PRODUCT_VERSION}"

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${PRODUCT_NAME}Installer.exe"
InstallDir "c:\${PRODUCT_NAME}"
ShowInstDetails show
ShowUnInstDetails show
LoadLanguageFile "Korean.nlf"
Icon "Installer.ico"
UninstallIcon "Uninstaller.ico"
XPStyle on
LicenseData "License.rtf"

!define MSG_UNINATALL_COMPLETED "$(^Name)는(은) 완전히 제거되었습니다."
!define MSG_DO_U_WANT_UNINTALL "$(^Name)을(를) 제거하시겠습니까?"
!define SECTION_INSTALL_MAIN "${PRODUCT_NAME} 기본설치"
!define SECTION_PROGRAMGROUP_LINK_CREATE "프로그램그룹에 바로가기생성"
!define SECTION_DESKTOP_LINK_CREATE "데스크톱에 바로가기생성"
!define TIME_INSTALLER_SPLASH "3000"
!define TIME_UNINSTALLER_SPLASH "3000"
!define EXCUTABLE "run.bat"
!define DOTNET_VERSION "3.5"
!define DOTNET_INSTALLER "dotnetfx35.exe"
!define WINDOWS_INSTALLER "WindowsInstaller-KB893803-v2-x86.exe"
!define SILVERLIGHT_INSTALLER "Silverlight.exe"

Page license
Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles



Section "!Silverlight 설치" secId_dotNetInstall
	SectionIn RO
	File /oname=$TEMP\${SILVERLIGHT_INSTALLER} ${SILVERLIGHT_INSTALLER}

	${TickCountStart}
	MessageBox MB_OK "TickCount started"
 	
 	ExecWait "$TEMP\${SILVERLIGHT_INSTALLER} /q"

	${TickCountEnd} $0
	MessageBox MB_OK "Message showed: $0 ms"
SectionEnd

Section "!${SECTION_INSTALL_MAIN}"
	SectionIn RO
	SetOutPath "$INSTDIR"
	SetOverwrite on

	File /a /r ..\PosSlTest\dist\*
  
	WriteUninstaller "$INSTDIR\uninst.exe"
SectionEnd
	
Section "${SECTION_PROGRAMGROUP_LINK_CREATE}"
	CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
	CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\${PRODUCT_NAME_VERSION}.lnk" "$INSTDIR\${EXCUTABLE}"
	CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section "${SECTION_DESKTOP_LINK_CREATE}"
	CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\${EXCUTABLE}"
SectionEnd



Section Uninstall
	Delete "$DESKTOP\${PRODUCT_NAME}.lnk"  
	RMDir /r "$SMPROGRAMS\${PRODUCT_NAME}"
	RMDir /r "$INSTDIR"
	RMDir /r "$DOCUMENTS\${PRODUCT_NAME}"

	/* 
	* SetAutoClose true
	*/
SectionEnd



Function .onInit
	InitPluginsDir
	File /oname=$PLUGINSDIR\splash.bmp "InstallerSplash.bmp"
	File /oname=$PLUGINSDIR\splash.wav "InstallerSplash.wav"
	splash::show ${TIME_INSTALLER_SPLASH} $PLUGINSDIR\splash
	
	SectionSetText ${secId_dotNetInstall} ""
	
FunctionEnd

Function .onInstSuccess
	Exec $INSTDIR\${EXCUTABLE}
FunctionEnd
  

  
Function un.onUninstSuccess
	HideWindow
	MessageBox MB_ICONINFORMATION|MB_OK "${MSG_UNINATALL_COMPLETED}"
FunctionEnd

Function un.onInit
	InitPluginsDir
	File /oname=$PLUGINSDIR\splash.bmp "UninstallerSplash.bmp"
	File /oname=$PLUGINSDIR\splash.wav "UninstallerSplash.wav"
	splash::show ${TIME_UNINSTALLER_SPLASH} $PLUGINSDIR\splash
	
	MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "${MSG_DO_U_WANT_UNINTALL}" IDYES +2
	Abort
FunctionEnd