cls
echo %~dp0
SET batchDir=%~dp0
echo %batchDir%
MD %batchDir%NewDir
IF EXIST %batchDir%NewDir (
RD /s /q %batchDir%NewDir
)
MD %batchDir%NewDir
Copy %batchDir%ToCopy.txt %batchDir%NewDir\Copied.txt
dir > %batchDir%NewDir\NewFile.txt
REM RD /s /q %batchDir%NewDir
