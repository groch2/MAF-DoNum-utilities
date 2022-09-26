<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var httpClient = new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/") };
var _document = new { documentId = "" };
var fileContent = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\DONUM\documents.json");
var json = JsonDocument.Parse(fileContent).RootElement;
var count = json.GetProperty("@odata.count").GetInt32();
var array = json.GetProperty("value");
for(var index = 0; index < count; index++) {
	var rawDocument = array[index].GetRawText();
	dynamic document = JsonSerializer.Deserialize(rawDocument, _document.GetType());
	string documentId = document.documentId;
	var requestContent = new StringContent(rawDocument, Encoding.UTF8, "application/json");
	var httpResponse = await httpClient.PatchAsync(documentId, requestContent);
	new { RequestUri = httpResponse.RequestMessage.RequestUri, IsSuccessStatusCode = httpResponse.IsSuccessStatusCode }.Dump();	
}
