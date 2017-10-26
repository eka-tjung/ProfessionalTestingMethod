$numbers = 0
While ($numbers -lt 10) {
$numbers = $numbers + 1
If($numbers -gt 5){
Write-Host "$numbers is greater than 5"
}
Else {
Write-Host "$numbers is less than 5"
}
}
