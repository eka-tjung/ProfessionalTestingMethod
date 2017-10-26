# Start Internet Explorer
Start-Process iexplore.exe www.google.com

# Wait for 10 seconds
Start-Sleep -s 10

# Kill IE Process
Get-Process iexplore | Foreach-Object { $_.CloseMainWindow() }

# Kill IE Process #2
#Get-Process iexplore | Stop-Process