@echo off
setlocal

REM Verifica se o Docker está rodando
echo Verificando se o Docker está em execução...
docker info >nul 2>&1
if errorlevel 1 (
    echo ERRO: O Docker não está em execução. Inicie o Docker Desktop e tente novamente.
    pause
    exit /b
)

echo Parando containers Docker em execução...
for /f "tokens=*" %%i in ('docker ps -q') do (
    docker stop %%i
)

REM Define ambiente globalmente para este script
set "DOTNET_ENVIRONMENT=Development"
echo DOTNET_ENVIRONMENT definido como: %DOTNET_ENVIRONMENT%

REM Sobe infraestrutura no Docker em background
docker-compose -f docker-compose.dev.yml up -d

REM Caminho relativo até a pasta src onde está a .sln
set "BASE_PATH=%~dp0..\Backend\src"
set "FRONTEND_PATH=%~dp0..\Frontend"

REM Restaura e compila o Producer e o Consumer
echo Restaurando pacotes .NET...
dotnet restore "%BASE_PATH%"

echo Compilando solution...
dotnet build "%BASE_PATH%" --no-restore

echo Iniciando API (Producer)...
start cmd /k "cd /d %BASE_PATH%\Producer\DeveloperStore.Sales.API && set DOTNET_ENVIRONMENT=%DOTNET_ENVIRONMENT% && dotnet run"

echo Iniciando Consumer...
start cmd /k "cd /d %BASE_PATH%\Consumer\DeveloperStore.Sales.Consumer.API && set DOTNET_ENVIRONMENT=%DOTNET_ENVIRONMENT% && dotnet run"

echo Iniciando Frontend (Angular)...
start cmd /k "cd /d %FRONTEND_PATH% && npm install --loglevel=error && npm start"

echo Todos os serviços foram iniciados em janelas separadas.
endlocal
pause
