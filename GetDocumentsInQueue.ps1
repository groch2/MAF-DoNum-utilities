$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
$session.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36"
Invoke-WebRequest -UseBasicParsing -Uri "http://localhost:56326/api/documents/getdocumentsinqueue?redacteur=IDE" `
-WebSession $session `
-Headers @{
"Accept"="text/plain"
  "Accept-Encoding"="gzip, deflate, br"
  "Accept-Language"="en-US,en;q=0.9,de-DE;q=0.8,de;q=0.7,fr;q=0.6"
  "Cache-Control"="no-cache"
  "DNT"="1"
  "Origin"="http://cons-donum-intra.loc.maf.local"
  "Pragma"="no-cache"
  "Referer"="http://cons-donum-intra.loc.maf.local/"
  "Sec-Fetch-Dest"="empty"
  "Sec-Fetch-Mode"="cors"
  "Sec-Fetch-Site"="cross-site"
  "sec-ch-ua"="`"Chromium`";v=`"104`", `" Not A;Brand`";v=`"99`", `"Google Chrome`";v=`"104`""
  "sec-ch-ua-mobile"="?0"
  "sec-ch-ua-platform"="`"Windows`""
}