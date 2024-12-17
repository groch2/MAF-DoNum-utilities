$xmlPath = Join-Path -Path $env:DEPLOYMENT_DESTINATION_FOLDER -ChildPath "Donum.Webhost.Intranet\web.config"
Write-Host "variable maxHttpRequestSizeInBytes:"
Write-Host $env:MAXHTTPREQUESTSIZEINBYTES

[xml]$xml = Get-Content -Path $xmlPath

$requestLimitsNode = $xml.configuration.location."system.webServer".security.requestFiltering.requestLimits
if ($requestLimitsNode) {
    $requestLimitsNode.maxAllowedContentLength = $env:MAXHTTPREQUESTSIZEINBYTES
    Write-Output "Attribut maxAllowedContentLength modifié avec succès."
} else {
    Write-Output "Le noeud requestLimits n'a pas été trouvé."
}
