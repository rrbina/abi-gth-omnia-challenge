@echo off
setlocal

REM Define ambiente globalmente para este script
set "DOTNET_ENVIRONMENT=Development"
echo DOTNET_ENVIRONMENT definido como: %DOTNET_ENVIRONMENT%

REM Sobe infraestrutura no Docker em background
docker-compose -f docker-compose.dev.yml up -d

REM Caminho relativo até a pasta src onde está a .sln
set "BASE_PATH=%~dp0..\Backend\src"
set "FRONTEND_PATH=%~dp0..\Frontend"

echo Iniciando API (Producer)...
start cmd /k "cd /d %BASE_PATH%\Producer\DeveloperStore.Sales.API && set DOTNET_ENVIRONMENT=%DOTNET_ENVIRONMENT% && dotnet run"

echo Iniciando Consumer...
start cmd /k "cd /d %BASE_PATH%\Consumer\DeveloperStore.Sales.Consumer.API && set DOTNET_ENVIRONMENT=%DOTNET_ENVIRONMENT% && dotnet run"

echo Iniciando Frontend (Angular)...
start cmd /k "cd /d %FRONTEND_PATH% && npm install && npm start"

echo Todos os serviços foram iniciados em janelas separadas.
endlocal
pause