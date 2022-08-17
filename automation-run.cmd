@echo off

echo This script doesn't build or run the application. 
echo If needed, run .\app-build and .\app-run

echo -----------------
echo Building Solution
echo -----------------
dotnet build -v q --nologo .\SpecFlowAPI.sln 

if %ERRORLEVEL% neq 0 goto ERROR
echo.
if not defined Spotify__Environment set /P Spotify__Environment="Set the Environment [local | dev | qa | uat]: "
if not defined TAG set /P TAG="Set the Category [smoke | get | post]: "

echo -----------------------
echo Runing Tests
echo Environment: %Spotify__Environment%
echo Category: %TAG%
echo -----------------------
dotnet test -v q --no-build --nologo SpecFlowAPI.sln --filter Category=%TAG% --logger html

if %ERRORLEVEL% neq 0 goto ERROR

goto END

:ERROR
echo !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
echo An unexpected error occurred, please review error details.
echo !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

goto END

:END
pause
start .\SpecFlowAPI\TestResults\