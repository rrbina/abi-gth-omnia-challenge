docker compose down -v
@echo off
echo ============================================
echo ?? Limpando containers em execução...
echo ============================================
docker stop $(docker ps -aq)

echo.
echo ============================================
echo ?? Removendo todos os containers...
echo ============================================
docker rm -f $(docker ps -aq)

echo.
echo ============================================
echo ?? Removendo todas as imagens...
echo ============================================
docker rmi -f $(docker images -aq)

echo.
echo ============================================
echo ?? Removendo todos os volumes...
echo ============================================
docker volume rm $(docker volume ls -q)

echo.
echo ============================================
echo ?? Removendo todas as redes...
echo ============================================
docker network rm $(docker network ls -q)

echo.
echo ============================================
echo ?? Limpando cache de build do Docker...
echo ============================================
docker builder prune -af

echo.
echo ? Docker limpo com sucesso!
pause
