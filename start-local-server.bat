@echo off
echo Starting localtunnel for SystemVisualizer...
echo Waiting for Docker container to be ready...
timeout /t 10 /nobreak

:retry
curl -s http://localhost:3000 >nul 2>&1
if errorlevel 1 (
    echo Waiting for server...
    timeout /t 5 /nobreak
    goto retry
)

echo Server is up! Starting tunnel...
npx localtunnel --port 3000 --subdomain systemvisualizer
