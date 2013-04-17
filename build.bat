@echo off
powershell -NoProfile -ExecutionPolicy unrestricted -Command "& .\build\SetVersion.ps1 %*"
cmd /c build\SetEnvironment.bat