@echo off
echo === Ativando WSL e Virtual Machine Platform ===
dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart

echo.
echo === Baixando e instalando o Kernel do WSL 2 ===
set "TEMPFILE=%TEMP%\wsl2kernel.msi"
powershell -Command "Invoke-WebRequest -Uri https://aka.ms/wsl2kernel -OutFile %TEMPFILE%"
msiexec /i %TEMPFILE% /passive

echo.
echo === Definindo WSL 2 como padrao ===
wsl --set-default-version 2

echo.
echo === Finalizado! Reinicie o computador para concluir. ===
pause