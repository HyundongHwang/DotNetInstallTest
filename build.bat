pause
rem deleting old installers ...
pause
del /S /Q scripts\*Installer.exe
del /S /Q *Installer.exe

pause
rem deploying PosSlTest ...
pause
rd /S /Q PosSlTest\dist
mkdir PosSlTest\dist
xcopy /Y "PosSlTest\Bin\Debug\*.xap" PosSlTest\dist\*
xcopy /Y "PosSlTest\Bin\Debug\*.bat" PosSlTest\dist\*
xcopy /Y "PosSlTest\Bin\Debug\*.png" PosSlTest\dist\*
xcopy /Y "PosSlTest\Bin\Debug\images\*" PosSlTest\dist\images\*

pause
rem deploying PosWpfTest ...
pause
rd /S /Q PosWpfTest\dist
mkdir PosWpfTest\dist
xcopy /Y /E "PosWpfTest\bin\Debug\*" PosWpfTest\dist\*

pause
rem building SlInstaller ...
pause
"nsis 2.46.5 Unicode\makensis.exe" scripts\SlInstaller.nsi

pause
rem building WpfInstaller ...
pause
"nsis 2.46.5 Unicode\makensis.exe" scripts\WpfInstaller.nsi

pause
rem deploying installers ...
pause
xcopy /Y scripts\*Installer.exe .\*