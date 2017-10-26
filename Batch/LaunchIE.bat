@echo off
Title Start and Kill Internet Explorer
echo(
echo                     Launching Internet Explorer ...
Start "" "%ProgramFiles%\Internet Explorer\iexplore.exe" "www.google.com"
:: Sleep for 10 seconds, you can change the SleepTime variable
set SleepTime=10
Timeout /T %SleepTime% /NoBreak>NUL
Cls & Color 0C
echo(
echo              Killing Internet Explorer Please wait for a while ...
Taskkill /IM "iexplore.exe" /F



REM Start "" "%ProgramFiles%\Internet Explorer\iexplore.exe" "www.google.com"
REM Timeout /T 10
REM Taskkill /IM "iexplore.exe" /F