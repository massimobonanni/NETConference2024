param(
    [Parameter(Mandatory=$true)]
    [string]$endpoint,

    [Parameter(Mandatory=$false)]
    [int]$numberOfCalls=10
)

for ($i=0; $i -lt $numberOfCalls; $i++) {
    $response = Invoke-WebRequest -Uri $endpoint
    Write-Output "Request $($i): Status Code: $($response.StatusCode)"
}
